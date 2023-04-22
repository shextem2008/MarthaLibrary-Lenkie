using Contracts.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Dto
{
    public class AuthenticateResponse
    {
        public string UserName { get; set; }
        public string JwtToken { get; set; }
        public int ExpiresIn { get; set; }
    }
}
