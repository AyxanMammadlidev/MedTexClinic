using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Application.DTOs.Payment
{
    public record PaymentDto(long? Amount,string Currency,string CardNumber,string CVV,string ExpiryYear,string ExpiryMonth);
}
