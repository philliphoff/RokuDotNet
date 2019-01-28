using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RokuDotNet.Client.Input
{
    public interface IRokuDeviceInput
    {
        Task KeyDownAsync(PressedKey key, CancellationToken cancellationToken = default(CancellationToken));

        Task KeyPressAsync(PressedKey key, CancellationToken cancellationToken = default(CancellationToken));

        Task KeyPressAsync(IEnumerable<PressedKey> keys, CancellationToken cancellationToken = default(CancellationToken));

        Task KeyUpAsync(PressedKey key, CancellationToken cancellationToken = default(CancellationToken));
    }
}