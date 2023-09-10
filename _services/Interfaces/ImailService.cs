namespace SocialClint._services.Interfaces
{
    public interface ImailService
    {
        Task SentEmailAsync(string MailTo, string subject, string body);

        //Task SentEmailAsync(string MailTo, string subject, string body, IList<IFormFile> attachment = null);

    }
}
