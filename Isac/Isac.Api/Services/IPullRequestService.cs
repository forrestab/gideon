using Isac.WebHooks.Receivers.BitbucketServer.Models.Notifications;
using System.Threading.Tasks;

namespace Isac.Api.Services
{
    public interface IPullRequestService
    {
        Task OnOpenedHandlerAsync(PullRequestOpenedNotification notification);
    }
}
