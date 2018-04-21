using System;
using RokuDotNet.Client.Input;
using RokuDotNet.Client.Query;

namespace RokuDotNet.Client
{
    public interface IRokuDevice
    {
        IRokuDeviceInputApi InputApi { get; }

        Uri Location { get; }

        IRokuDeviceQueryApi QueryApi { get; }

        string SerialNumber { get; }
    }
}