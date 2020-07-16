using ArtigoAzureServiceBus.API.InputModels;
using System.Threading.Tasks;

namespace ArtigoAzureServiceBus.API.Services
{
    public interface INotificationService
    {
        Task Send(UserFollowingInputModel userFollowing);
    }
}
