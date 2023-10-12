namespace APP.Services;

public interface IEmailService
{
    public Task SendEmail(string senderName, string message, string recipientsEmail);
}
