using PracticeV1.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeV1.Application.Services
{
    public interface IEmailService 
    {
        Task SendOrderConfirmationAsync(
         int orderId,
         string customerName,
         string customerEmail,
         decimal totalAmount);

    }
}
