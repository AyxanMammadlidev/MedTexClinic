using FinalProject.Application.Abstractions.Services;
using FinalProject.Application.DTOs.Payment;
using FinalProject.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost("create-payment-intent")]
        public async Task<IActionResult> CreatePaymentIntent([FromBody] PaymentDto request)
        {
            var clientSecret = await _paymentService.CreatePaymentIntent(request);
            return Ok(new { clientSecret });
        }
    }
}
