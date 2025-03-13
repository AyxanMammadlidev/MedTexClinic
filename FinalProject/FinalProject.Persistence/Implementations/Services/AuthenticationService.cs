using AutoMapper;
using FinalProject.Application.Abstractions.Services;
using FinalProject.Application.Abstractions.Token;
using FinalProject.Application.DTOs.Account;
using FinalProject.Application.DTOs.Tokens;
using FinalProject.Domain.Entities;
using FinalProject.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using System.Text;

namespace FinalProject.Persistence.Implementations.Services
{
    internal class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ITokenHandler _tokenHandler;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;
        private string? resetCode;
        private DateTime expireTime;


        public AuthenticationService(UserManager<AppUser> userManager,
            IMapper mapper,
            ITokenHandler tokenhandler,
            SignInManager<AppUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            IEmailService emailService,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _mapper = mapper;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _tokenHandler = tokenhandler;
            _configuration = configuration;
            _emailService = emailService;

        }

        public async Task<TokenResponseDto> LoginAsync(LoginDto loginDto)
        {

            var user = await _userManager.Users.FirstOrDefaultAsync(u => loginDto.EmailOrUsername == u.UserName || loginDto.EmailOrUsername == u.Email);
            if (user is null)
                throw new Exception("Username ,email or Password incorrect");

            bool result = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!result)
            {
                user.AccessFailedCount++;
                throw new Exception("Username ,email or Password incorrect");
            }
            var claims = await _tokenHandler.CreateClaimsAsync(user);
            TokenResponseDto tokenDto = await _tokenHandler.CreateAccessToken(user, 15, claims);
            user.RefreshToken = tokenDto.RefreshToken;
            user.RefreshTokenExpireTime = tokenDto.RefreshTokenExpireTime;
            await _userManager.UpdateAsync(user);
            return tokenDto;

        }

        public async Task RegisterAsync(RegisterDto userDto)
        {
            if (await _userManager.Users.AnyAsync(u => u.UserName == userDto.UserName || u.Email == userDto.Email))
            {
                throw new Exception("this user already exists");
            }
            if (!userDto.IsAgree)
            {
                throw new Exception("You must agree to the terms and conditions to register");
            }

            AppUser user = _mapper.Map<AppUser>(userDto);

            var result = await _userManager.CreateAsync(user, userDto.Password);
            if (!result.Succeeded)
            {
                StringBuilder builder = new();
                foreach (var error in result.Errors)
                {
                    builder.AppendLine(error.Description);
                }

                throw new Exception(builder.ToString());
            }

            await _userManager.AddToRoleAsync(user, Roles.Patient.ToString());


        }

        public async Task Logout(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
                throw new Exception("User not found");

            user.RefreshToken = null;
            user.RefreshTokenExpireTime = DateTime.UtcNow;

            await _userManager.UpdateAsync(user);
        }

        public async Task ForgotPasswordAsync(ForgotPasswordDto forgotPasswordDto)
        {
            AppUser? user = await _userManager.FindByEmailAsync(forgotPasswordDto.Email);
            if (user is null)
                throw new Exception("User not found");

            string resetCode = Guid.NewGuid().ToString().Substring(0, 6).ToUpper();
            DateTime expireTime = DateTime.UtcNow.AddMinutes(5);


            user.ResetCode = resetCode;
            user.ResetCodeExpireTime = expireTime;
            await _userManager.UpdateAsync(user);

            await _emailService.SendEmailAsync(

                to: forgotPasswordDto.Email,
                subject: "Password Reset",
                body: $"Please enter the code to reset your password:\n\n{resetCode}"
            );
        }

        public async Task ResetPasswordAsync(ResetPasswordDto resetPasswordDto)
        {
            AppUser? user = await _userManager.Users.FirstOrDefaultAsync(u => u.ResetCode == resetPasswordDto.ResetCode && u.ResetCodeExpireTime > DateTime.UtcNow);
            if (user is null)
                throw new Exception("Reset Code is incorrect or expired");


            string token = await _userManager.GeneratePasswordResetTokenAsync(user);



            var result = await _userManager.ResetPasswordAsync(user, token, resetPasswordDto.NewPassword);

            if (!result.Succeeded)
            {
                StringBuilder errorBuilder = new();
                foreach (var error in result.Errors)
                {
                    errorBuilder.AppendLine(error.Description);
                }
                throw new Exception(errorBuilder.ToString());
            }

            user.ResetCode = null;
            user.ResetCodeExpireTime = null;
            await _userManager.UpdateAsync(user);

        }

        public async Task<TokenResponseDto> LoginWithRefreshToken(string refreshToken)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
            if (user is null)
                throw new Exception("Not found");
            if (user.RefreshTokenExpireTime < DateTime.Now)
                throw new Exception("RefreshToken Expired");
            List<Claim> claims = await _tokenHandler.CreateClaimsAsync(user);
            TokenResponseDto tokenDto = await _tokenHandler.CreateAccessToken(user, 15, claims);

            user.RefreshToken = user.RefreshToken;
            user.RefreshTokenExpireTime = user.RefreshTokenExpireTime;

            await _userManager.UpdateAsync(user);
            return tokenDto;


        }
    }
}
