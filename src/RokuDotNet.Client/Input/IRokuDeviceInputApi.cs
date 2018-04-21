using System.Threading;
using System.Threading.Tasks;

namespace RokuDotNet.Client.Input
{
    public interface IRokuDeviceInputApi
    {
        Task KeyDownAsync(string key, CancellationToken cancellationToken = default(CancellationToken));

        Task KeyPressAsync(string key, CancellationToken cancellationToken = default(CancellationToken));

        Task KeyUpAsync(string key, CancellationToken cancellationToken = default(CancellationToken));
    }
}