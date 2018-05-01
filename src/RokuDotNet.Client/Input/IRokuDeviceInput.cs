using System.Threading;
using System.Threading.Tasks;

namespace RokuDotNet.Client.Input
{
    public interface IRokuDeviceInput
    {
        Task KeyDownAsync(SpecialKeys key, CancellationToken cancellationToken = default(CancellationToken));
        Task KeyDownAsync(char key, CancellationToken cancellationToken = default(CancellationToken));

        Task KeyPressAsync(SpecialKeys key, CancellationToken cancellationToken = default(CancellationToken));

        Task KeyPressAsync(char key, CancellationToken cancellationToken = default(CancellationToken));

        Task KeyUpAsync(SpecialKeys key, CancellationToken cancellationToken = default(CancellationToken));
        Task KeyUpAsync(char key, CancellationToken cancellationToken = default(CancellationToken));
    }
}