using Isac.Integrations.Atlassian.Bitbucket.Models.Notifications;
using System.Threading.Tasks;

namespace Isac.WebHooks.Services
{
    public interface IPullRequestWebHooksService
    {
        Task OnOpenedAsync(PullRequestOpenedNotification notification);
    }
}
