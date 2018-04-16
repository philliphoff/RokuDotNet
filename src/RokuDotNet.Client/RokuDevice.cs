using System;

namespace RokuDotNet.Client
{
    public sealed class RokuDevice : IRokuDevice
    {
        public RokuDevice(Uri location, string serialNumber)
        {
            this.Location = location ?? throw new ArgumentNullException(nameof(location));
            this.SerialNumber = serialNumber ?? throw new ArgumentNullException(nameof(serialNumber));
        }

        #region IRokuDevice Members

        public Uri Location { get; }

        public string SerialNumber { get; }

        #endregion
    }
}