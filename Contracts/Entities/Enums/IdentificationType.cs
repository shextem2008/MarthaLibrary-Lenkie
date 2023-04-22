using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Entities
{
    public enum IdentificationType
    {
        NationalID = 1,
        Passport = 2,
        DriverLicense = 3,
        Voterscard  = 4,
        SocialSecurity = 5,
        Default = 6
    }
}
