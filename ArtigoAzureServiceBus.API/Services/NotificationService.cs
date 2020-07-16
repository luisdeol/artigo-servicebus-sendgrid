using ArtigoAzureServiceBus.API.InputModels;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace ArtigoAzureServiceBus.API.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IConfiguration _configuration;
        private readonly ISendGridClient _sendGridClient;
        public NotificationService(ISendGridClient sendGridClient, IConfiguration configuration)
        {
            _configuration = configuration;
            _sendGridClient = sendGridClient;
        }

        public async Task Send(UserFollowingInputModel userFollowingInputModel)
        {
            var defaultSubject = _configuration.GetSection("Notification:DefaultSubject").Value;
            var from = new EmailAddress(_configuration.GetSection("Notification:DefaultFrom").Value, _configuration.GetSection("Notification:DefaultFromName").Value);
            var to = new EmailAddress(userFollowingInputModel.Email, "ToName");
            var body = $"User {userFollowingInputModel.IdUserFollower} followed you.";

            var message = new SendGridMessage
            {
                From = from,
                Subject = defaultSubject
            };

            message.AddContent(MimeType.Html, body);
            message.AddTo(to);

            await _sendGridClient.SendEmailAsync(message);
        }
    }
}
