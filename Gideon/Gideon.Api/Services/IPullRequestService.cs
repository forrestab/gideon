using Gideon.WebHooks.Receivers.BitbucketServer.Models.Notifications;
using System.Threading.Tasks;

namespace Gideon.Api.Services
{
    public interface IPullRequestService
    {
        Task OnOpenedHandlerAsync(PullRequestOpenedNotification notification);
    }
}
