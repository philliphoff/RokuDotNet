using System;
using RokuDotNet.Client.Input;
using RokuDotNet.Client.Query;

namespace RokuDotNet.Client
{
    public interface IRokuDevice : IRokuDeviceInput, IRokuDeviceQuery
    {
        string Id { get; }

        IRokuDeviceInput Input { get; }

        IRokuDeviceQuery Query { get; }
    }
}