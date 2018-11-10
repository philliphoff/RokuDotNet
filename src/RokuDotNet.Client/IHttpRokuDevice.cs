using System;

namespace RokuDotNet.Client
{
    public interface IHttpRokuDevice : IRokuDevice
    {
        Uri Location { get; }
    }
}