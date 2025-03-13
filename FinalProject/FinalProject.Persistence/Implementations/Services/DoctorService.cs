using AutoMapper;
using FinalProject.Application.Abstractions.Repositories;
using FinalProject.Application.Abstractions.Services;
using FinalProject.Application.DTOs.Doctor;
using FinalProject.Domain.Entities;
using FinalProject.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SendGrid.Helpers.Errors.Model;

namespace FinalProject.Persistence.Implementations.Services
{
    internal class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IMapper _mapper;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly ICloudinaryService _cloudinaryService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;
        private const string EmailSubject = "Welcome to MedTex Clinic!";

        public DoctorService(IDoctorRepository doctorRepository,
            IMapper mapper,
            IDepartmentRepository departmentRepository,
            ICloudinaryService cloudinaryService,
            UserManager<AppUser> userManager,
            IConfiguration configuration,IEmailService emailService
            )
        {
            _doctorRepository = doctorRepository;
            _mapper = mapper;
            _departmentRepository = departmentRepository;
            _cloudinaryService = cloudinaryService;
            _userManager = userManager;
            _configuration = configuration;
            _emailService = emailService;
        }

        public async Task CreateAsync(CreateDoctorDto doctorDto)
        {

            if (!await _departmentRepository.AnyAsync(d => d.Id == doctorDto.DepartmentId))
                throw new Exception("Department does not exists");
            if (doctorDto.Photo == null)
                throw new Exception("Please upload Image");
            (string imageUrl, string publicId) = await _cloudinaryService.UploadAsync(doctorDto.Photo);
            Doctor doctor = _mapper.Map<Doctor>(doctorDto);
            doctor.ImageUrl = imageUrl;
            doctor.ImagePublicId = publicId;
            doctor.IdentityCode = GenerateRandomString();
            doctor.CreatedAt = DateTime.Now;
            doctor.ModifiedAt = DateTime.Now;
            doctor.JoinDate = DateOnly.FromDateTime(DateTime.Now);
            await _doctorRepository.AddAsync(doctor);
            await _doctorRepository.SaveChangesAsync();

            var doctorUser = new AppUser
            {
                Name = doctorDto.Name,
                Surname = doctorDto.Surname,
                UserName = doctorDto.Email,
                Email = doctorDto.Email,
            };

            var result = await _userManager.CreateAsync(doctorUser, _configuration["DoctorDefault:Password"]);


            if (result.Succeeded)
                await _userManager.AddToRoleAsync(doctorUser, Roles.Doctor.ToString());

            else
               throw new Exception("Failed to create doctor user: " + string.Join(", ", result.Errors.Select(e => e.Description)));
            string emailMessage = GenerateEmailMessage(doctor);

            await _emailService.SendEmailAsync(doctor.Email, EmailSubject, emailMessage);

        }


        public async Task DeleteAsync(int id)
        {
            var doctor = await _doctorRepository.GetbyIdAsync(id);
            if (doctor is null)
                throw new NotFoundException("Doctor not found");
            if (!string.IsNullOrEmpty(doctor.ImagePublicId))
                await _cloudinaryService.DeleteAsync(doctor.ImagePublicId);

            _doctorRepository.Delete(doctor);
            await _doctorRepository.SaveChangesAsync();
        }

        public async Task<ICollection<DoctorItemDto>> GetAllAsync(int page, int take)
        {
            ICollection<Doctor> doctors = await _doctorRepository
                     .GetAll(null, null, false, false, skip: (page - 1) * take, take: take, "Comments")
                     .ToListAsync();
            return _mapper.Map<ICollection<DoctorItemDto>>(doctors);


        }

        public async Task<GetDoctorDto> GetByIdAsync(int id)
        {
            Doctor doctor = await _doctorRepository.GetbyIdAsync(id, "Comments");

            if (doctor is null)
                throw new Exception("Not found");


            GetDoctorDto doctorDto = _mapper.Map<GetDoctorDto>(doctor);



            return doctorDto;
        }

        public async Task UpdateAsync(int id, UpdateDoctorDto doctorDto)
        {
            if (!await _departmentRepository.AnyAsync(d => d.Id == doctorDto.DepartmentId))
                throw new Exception("Department does not exists");
            var doctor = await _doctorRepository.GetbyIdAsync(id);
            if (doctor is null)
                throw new NotFoundException("Doctor not found");

            if (!string.IsNullOrEmpty(doctor.ImagePublicId))//There is some doctors without image. This statement is Unnecessary at production enviroment
                await _cloudinaryService.DeleteAsync(doctor.ImagePublicId);

            (string imageUrl, string publicId) = await _cloudinaryService.UploadAsync(doctorDto.Photo);
            _mapper.Map(doctorDto, doctor);

            doctor.ImageUrl = imageUrl;
            doctor.ImagePublicId = publicId;
            doctor.ModifiedAt = DateTime.Now;
            await _doctorRepository.SaveChangesAsync();
        }




        private string GenerateEmailMessage(Doctor doctor)
        {
            return $@"
       Hello Dr. {doctor.Surname},<br><br>
       Congratulations! Your employment at MedTex Clinic has been successfully confirmed.<br><br>
       Here are your registration details:<br><br>
       - Identity Code: {doctor.IdentityCode}<br>
       - Full Name: {doctor.Name} {doctor.Surname}<br>
       - Start Date: {doctor.JoinDate}<br><br>
        Details of your Doctor Account:<br>
        userName:{doctor.Email}<br>
       Your temporary password: <strong>{_configuration["DoctorDefault:Password"]}</strong><br>
       (For security reasons, please change your password after logging in.)<br><br>
        
       We are delighted to have you on our team and look forward to working with you.<br><br>
       Best regards,<br>
       MedTex Team";
        }


        private string GenerateRandomString()
        {
            return Guid.NewGuid().ToString().Substring(0, 8).ToUpper();
               
        }

    }
}
