using FinalProject.Application.Abstractions.Services;
using FinalProject.Application.DTOs.Payment;
using Stripe.Forwarding;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalProject.Domain.Entities;
using FinalProject.Application.Abstractions.Repositories;
using Microsoft.Extensions.Configuration;

namespace FinalProject.Infrastructure.Implementations.Services
{
    internal class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IConfiguration _configuration;

        public PaymentService(IPaymentRepository paymentRepository
            ,IConfiguration configuration)
        {
            _paymentRepository = paymentRepository;
            _configuration = configuration;
            StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];
        }
        public async Task<string> CreatePaymentIntent(PaymentDto paymentRequest)
        {
            var options = new PaymentIntentCreateOptions
            {
                Amount = paymentRequest.Amount,
                Currency = paymentRequest.Currency,
                PaymentMethodTypes = new List<string> { "card" },

            };

     
            
            var service = new PaymentIntentService();
            var paymentIntent = await service.CreateAsync(options);
            Payment payment = new()
            {
                Amount = paymentRequest?.Amount,
                Currency = paymentRequest?.Currency,
                CreatedAt = DateTime.Now,
            };

            await _paymentRepository.AddAsync(payment);
            await _paymentRepository.SaveChangesAsync();
            return paymentIntent.ClientSecret;
        }
    }
}
