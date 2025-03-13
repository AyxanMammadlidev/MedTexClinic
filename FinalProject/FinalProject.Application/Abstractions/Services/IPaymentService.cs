
using FinalProject.Application.DTOs.Payment;
using FinalProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Application.Abstractions.Services
{
    public interface IPaymentService
    {
        Task<string> CreatePaymentIntent(PaymentDto paymentRequest);
    }
}
