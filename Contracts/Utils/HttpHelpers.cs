using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Utils
{
    public static class HttpHelpers
    {
        public static string GetStatusCodeValue(this HttpStatusCode code)
        {
            return ((int)code).ToString();
        }
    }
}
