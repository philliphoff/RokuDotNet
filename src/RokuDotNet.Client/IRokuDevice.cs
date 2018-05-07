using System;
using RokuDotNet.Client.Action;
using RokuDotNet.Client.Input;
using RokuDotNet.Client.Query;

namespace RokuDotNet.Client
{
    public interface IRokuDevice 
    {
        string Id { get; }

        IRokuDeviceInput Input { get; }

        IRokuDeviceQuery Query { get; }

        IRokuDeviceAction Action { get; }
    }
}