namespace BusinessLogic.Interfaces
{
    public interface IMailService
    {
        //send message to email user
        Task SendMailAsync(string subject, string body, string toSend, string? fromSend = null);
    }
}
