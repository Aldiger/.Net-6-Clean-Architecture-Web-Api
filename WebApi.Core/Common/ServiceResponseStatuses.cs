using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Core.Common
{
    [Serializable]
    [DataContract]
    public enum ServiceResponseStatuses
    {
        [EnumMember]
        Error,
        [EnumMember]
        Success,
        [EnumMember]
        Warning
    }
}
