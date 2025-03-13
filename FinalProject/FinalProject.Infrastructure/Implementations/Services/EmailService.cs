using FinalProject.Application.Abstractions.Services;
using FinalProject.Domain.Entities;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;

namespace FinalProject.Infrastructure.Implementations.Services
{
    internal class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            try
            {
                using (var client = new SmtpClient(
                    _configuration["SmtpSettings:Server"],
                    Convert.ToInt32(_configuration["SmtpSettings:Port"])
                ))
                {
                    client.Credentials = new NetworkCredential(
                        _configuration["SmtpSettings:Username"],
                        _configuration["SmtpSettings:Password"]
                    );
                    client.EnableSsl = Convert.ToBoolean(_configuration["SmtpSettings:EnableSsl"]);

                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress(_configuration["SmtpSettings:Username"]),
                        Subject = subject,
                        Body = body,
                        IsBodyHtml = true
                    };

                    mailMessage.To.Add(to);

                    await client.SendMailAsync(mailMessage);
                    Console.WriteLine("mail sent sucsessfully");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"error: {ex.Message}");
            }
        }



       

    }
}

