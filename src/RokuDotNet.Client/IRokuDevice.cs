using System;
using System.Threading;
using System.Threading.Tasks;
using RokuDotNet.Client.Apps;
using RokuDotNet.Client.Input;

namespace RokuDotNet.Client
{
    public interface IRokuDevice : IDisposable
    {
        string Id { get; }

        IRokuDeviceApps Apps {get; }

        IRokuDeviceInput Input { get; }

        Task<DeviceInfo> GetDeviceInfoAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}