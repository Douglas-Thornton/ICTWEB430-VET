namespace APP.Services;

public interface IEmailService
{
    public Task SendEmail(string senderName, string emailSubject, string message, string recipientsEmail);
}
