using Undersoft.SDK.Series;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System;
using System.Linq;

namespace Undersoft.SDK.Service.Server
{
    public static class GrpcDataServerRegistry
    {
        public static ISeries<Type> ServiceContracts = new Registry<Type>();
    }
}
