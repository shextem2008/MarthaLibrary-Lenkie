using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Exceptions
{
    public class ErrorConstants
    {
        public const string NULL_ENTRY_REJECTED = "LI001";
        public const string AUTHORIZATION_ERROR = "LI002";


        public const string ROLE_NOT_EXIST = "ROL001";
        public const string USER_ACCOUNT_EXISTS = "USR001";
        public const string USER_ACCOUNT_NOT_EXIST = "USR002";
        public const string USER_ACCOUNT_LOCKED = "USR005";
        public const string USER_ACCOUNT_REGISTRATION_FAILED = "USR003";
        public const string USER_ACCOUNT_PASSWORD_INVALID = "USR004";
        public const string USER_ACCOUNT_WRONG_OTP = "USR005";

        // AssignedMenuToRole
        public const string ASSIGNEMENUROLE_EXIST = "AMR001";
        public const string ASSIGNEMENUROLE_NOT_EXIST = "AMR002";
        public const string DELETEPARENTMENU_EXIST_SUBMENU = "AMR003";
        public const string MENU_ALREADY_ASSIGNEDTO_ROLE = "AMR004";



        // For Librarry:
        public const string LBOOKINFO_EXIST = "LBO005";
        public const string LBOOKINFO_NOT_EXIST = "LBO006";

        // For Tenant:
        public const string TENANTINFO_EXIST = "TEN005";
        public const string TENANTINFO_NOT_EXIST = "TEN006";

        // For Product:
        public const string PRODUCTINFO_EXIST = "PRO005";
        public const string PRODUCTINFO_NOT_EXIST = "PRO006";

        // For Wallet:
        public const string WalletINFO_EXIST = "WLO005";
        public const string WalletINFO_NOT_EXIST = "WLO006";

        // For Emloyee:
        public const string EMPLOYEE_EXIST = "EMP005";
        public const string EMPLOYEE_NOT_EXIST = "EMP006";

        // For Client:
        public const string CLIENT_EXIST = "CLI005";
        public const string CLIENT_NOT_EXIST = "CLI006";
        // For Client:
        public const string VENDOR_EXIST = "VEN005";
        public const string VENDOR_NOT_EXIST = "VEN006";

    }
}
