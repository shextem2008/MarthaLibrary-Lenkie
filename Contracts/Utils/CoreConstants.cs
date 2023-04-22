using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Utils
{
    public abstract class CoreConstants
    {
        public const string DefaultAccount = "superadmin@ts.com";
        public const string DefaultAccountName = "superadmin";
        public const string DateFormat = "dd MMMM, yyyy";
        public const string TimeFormat = "hh:mm tt";
        public const string SystemDateFormat = "dd/MM/yyyy";
        public const string? TenantName = "Default";
        public const string TenantEmail = "default@ts.com";
        public const string? DepartmentName = "Default";

        public class Roles
        {
            public const string SuperAdmin = "SuperAdministrator";
            public const string Admin = "Administrator";
            public const string Employee = "Employee";
            public const string Customer = "Customer";
            public const string Vendor = "Vendor";
            public const string Bank = "Bank";
        }

        public class Url
        {
            public const string PasswordResetEmail = "password-resetcode-email.html";
            public const string AccountActivationEmail = "account-email.html";
            public const string BookingSuccessEmail = "confirm-email.html";
            public const string BookingAndReturnSuccessEmail = "confirm-return-email.html";
            public const string ActivationCodeEmail = "activation-code-email.html";
            public const string BookingUnSuccessEmail = "failed-email.html";
            public const string RescheduleSuccessEmail = "reschedule-success.html";
            public const string AdminHireBookingEmail = "hirebooking-admin.html";
            public const string CustomerHireBookingEmail = "hirebooking.html";
            public const string getImages = "Images";
        }


        public class WebConstants
        {
            public const int DefaultPageSize = int.MaxValue;
        }
    }
}
