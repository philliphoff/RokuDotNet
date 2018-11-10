using System;
using RokuDotNet.Client.Input;
using RokuDotNet.Client.Query;

namespace RokuDotNet.Client
{
    public interface IRokuDevice : IDisposable
    {
        string Id { get; }

        IRokuDeviceInput Input { get; }

        IRokuDeviceQuery Query { get; }
    }
}