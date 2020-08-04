using System.Threading;
using System.Threading.Tasks;

namespace MGAnonymized.Web.Infrastructure
{
    public interface IStartupTask
    {
        Task ExecuteAsync(CancellationToken cancellationToken = default);
    }
}
