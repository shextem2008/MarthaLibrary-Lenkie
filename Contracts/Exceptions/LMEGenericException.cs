using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Exceptions
{
    [Serializable]
    public class LMEGenericException : Exception
    {
        public string ErrorCode { get; set; }

        public LMEGenericException(string message) : base(message)
        { }

        public LMEGenericException(string message, string errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}
