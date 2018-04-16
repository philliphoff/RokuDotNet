using System;

namespace RokuDotNet.Client
{
    public interface IRokuDevice
    {
        Uri Location { get; }

        string SerialNumber { get; }
    }
}