using System;
using RokuDotNet.Client.Input;
using RokuDotNet.Client.Launch;
using RokuDotNet.Client.Query;

namespace RokuDotNet.Client
{
    public interface IRokuDevice : IDisposable
    {
        string Id { get; }

        IRokuDeviceInput Input { get; }

        IRokuDeviceLaunch Launch {get; }

        IRokuDeviceQuery Query { get; }
    }
}