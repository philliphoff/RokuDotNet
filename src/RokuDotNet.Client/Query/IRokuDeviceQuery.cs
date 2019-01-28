using System.Threading;
using System.Threading.Tasks;

namespace RokuDotNet.Client.Query
{
    public interface IRokuDeviceQuery
    {    
        Task<DeviceInfo> GetDeviceInfoAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}