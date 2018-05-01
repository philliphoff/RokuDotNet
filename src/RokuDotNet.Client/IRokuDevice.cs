using System;
using RokuDotNet.Client.Input;
using RokuDotNet.Client.Query;

namespace RokuDotNet.Client
{
    public interface IRokuDevice
    {
        IRokuDeviceInput Input { get; }

        Uri Location { get; }

        IRokuDeviceQuery Query { get; }

        string SerialNumber { get; }
    }
}