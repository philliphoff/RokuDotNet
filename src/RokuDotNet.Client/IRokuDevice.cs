using System;
using RokuDotNet.Client.Apps;
using RokuDotNet.Client.Input;
using RokuDotNet.Client.Query;

namespace RokuDotNet.Client
{
    public interface IRokuDevice : IDisposable
    {
        string Id { get; }

        IRokuDeviceApps Apps {get; }

        IRokuDeviceInput Input { get; }

        IRokuDeviceQuery Query { get; }
    }
}