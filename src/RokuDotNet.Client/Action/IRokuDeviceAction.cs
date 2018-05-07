using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Threading.Tasks;

namespace RokuDotNet.Client.Action
{
    public interface IRokuDeviceAction
    {
        Task Launch(string appId, NameValueCollection nvc);
    }
}
