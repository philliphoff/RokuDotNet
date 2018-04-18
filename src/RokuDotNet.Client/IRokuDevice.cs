using System;
using RokuDotNet.Client.Query;

namespace RokuDotNet.Client
{
    public interface IRokuDevice
    {
        Uri Location { get; }

        IRokuDeviceQueryApi QueryApi { get; }

        string SerialNumber { get; }
    }
}