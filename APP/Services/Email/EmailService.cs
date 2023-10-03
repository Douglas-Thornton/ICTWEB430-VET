using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Configuration;

namespace APP.Services;

public class EmailService : IEmailService
{
    private readonly IConfiguration configuration;
    public EmailService(IConfiguration configuration)
    {
        this.configuration = configuration;
    }
    public async Task SendEmail(string senderName, string message, string recipientsEmail)
    {
        // Sender's email address and credentials
        string senderEmail = configuration["EmailSender"];
        string senderPassword = configuration["EmailPassword"];

        // Recipient's email address
        string subject = $"Play Date: {senderName}";
        string body = message;
        // Mail server (SMTP) and port
        string smtpServer = "smtp.gmail.com";

        if(!IsValidEmail(senderEmail)) { return; }
        if(!IsValidEmail(recipientsEmail)) {  return; }

        // Create the email message
        try
        {
            using MailMessage mailMessage = new(senderEmail, recipientsEmail, subject, body);
            // Set up the SMTP client
            using SmtpClient smtpClient = new(smtpServer, 587)
            {
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(senderEmail, senderPassword),
                EnableSsl = true
            };

            try
            {
                // Send the email asynchronously
                await smtpClient.SendMailAsync(mailMessage);
                Console.WriteLine("Email sent successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error sending email: " + ex.Message);
            }
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        
    }

    private bool IsValidEmail(string email)
    {
        try
        {
            var addr = new MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }
}
