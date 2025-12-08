using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;
using PracticeV1.Application.Services;
using PracticeV1.Domain.Entity;
using PracticeV1.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;


namespace PracticeV1.Infrastructure.Service
{
    public class EmailService : IEmailService
    {
        public readonly IConfiguration _configuration;
        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task SendOrderConfirmationAsync(int orderId,
         string customerName,
         string customerEmail,
         decimal totalAmount)
        {
            var setting = _configuration.GetSection("Smtp");
            var message = new MailMessage
            {
                From = new MailAddress(setting["FromEmail"], setting["FromName"]),
                Subject = $"Đơn hàng #{orderId} đã được xác nhận!",
                Body = $@"
                <h2>Xin chào {customerName},</h2>
                <p>Cảm ơn bạn đã mua sắm tại <strong>MyShop</strong>!</p>
                <p><strong>Mã đơn hàng:</strong> #{orderId} </p>
                <p><strong>Tổng tiền:</strong> {totalAmount:#,##0} VNĐ</p>
                <p><strong>Thời gian:</strong> {DateTime.Now:dd/MM/yyyy HH:mm}</p>
                <hr>
                <p>Đơn hàng đang được xử lý và sẽ giao trong 2-3 ngày.</p>
                <p>Trân trọng,<br><strong>MyShop Team</strong></p>",
                IsBodyHtml = true
            };
            message.To.Add(new MailAddress(customerEmail, customerName));
            using var client = new SmtpClient(setting["Host"], int.Parse(setting["Port"]))
            {
                Credentials = new NetworkCredential(setting["Username"], setting["Password"]),
                EnableSsl = true
            };

            try
            {
                await client.SendMailAsync(message);
                Console.WriteLine($"Email đã gửi tới {customerEmail}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Gửi email thất bại: {ex.Message}");
            }



        }
    }
}
