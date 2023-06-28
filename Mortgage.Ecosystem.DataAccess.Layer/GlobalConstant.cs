using Microsoft.Extensions.Hosting;
using Mortgage.Ecosystem.DataAccess.Layer.Conversion;

namespace Mortgage.Ecosystem.DataAccess.Layer
{
    public class GlobalConstant
    {
        public const int ZERO = 0;
        public const int ONE = 1;
        public const int TWO = 2;
        public const int THREE = 3;
        public const int FOUR = 4;
        public const int FIVE = 5;
        public const int SIX = 6;
        public const int SEVEN = 7;
        public const int EIGHT = 8;
        public const int NINE = 9;
        public const long NHF_NUMBER_START_RANGE = 1000000000;
        public const long NHF_NUMBER_END_RANGE = 9999999999;

        public const string USER_ROLE = "user";
        public const string ADMIN_ROLE = "admin";
        public const string EMPTY_STRING = "";

        public const string APP_NAME = "Mortgage Ecosystem";
        public const string COMPANY_NAME = "Fintrak Software Co. Limited";
        public const string RESERVED = "All rights reserved.";
        public const string CREDENTIAL_USERNAME = "bankonline@fmbn.gov.ng";
        public const string CREDENTIAL_PASSWORD = "puf72190";
        public const string MAIL_FROM = "bankonline@fmbn.gov.ng";
        public const string SMTP_HOST = "outlook.office365.com";

        //public const string MAIL_FROM = "development-team@mortgageecosystem.com";
        //public const string CREDENTIAL_USERNAME = "mortgage.ecosystem@gmail.com";
        //public const string CREDENTIAL_PASSWORD = "Password10$";
        ////public const string CREDENTIAL_PASSWORD = "dcfawdzkyvuvqiuz";
        //public const string SMTP_HOST = "smtp.gmail.com";

        public const int SMTP_PORT = 587;
        public const bool SMTP_SSL = true;

        // System Type
        public static PlatformID SystemType
        {
            get
            {
                return Environment.OSVersion.Platform;
            }
        }

        // System 32Or64
        public static int System32Or64
        {
            get
            {
                if (IntPtr.Size == 8)
                {
                    return 64; //64 bit
                }
                else if (IntPtr.Size == 4)
                {
                    return 32; //32 bit
                }
                else
                {
                    return 0;
                }
            }
        }

        // Whether the formal environment
        public static bool IsProduction
        {
            get
            {
                var isDevelopment = GlobalContext.HostingEnvironment?.IsProduction();
                return isDevelopment.ToBool(false);
            }
        }

        // Whether the development environment
        public static bool IsDevelopment
        {
            get
            {
                var isDevelopment = GlobalContext.HostingEnvironment?.IsDevelopment();
                return isDevelopment.ToBool(true);
            }
        }

        // Current timestamp
        public static string TimeStamp
        {
            get
            {
                return DateTime.Now.ToDateByUnix().ToTruncate(0).ToTrim();
            }
        }

        // Default Time
        public static DateTime DefaultTime
        {
            get
            {
                return DateTime.Parse("1970-01-01 00:00:00");
            }
        }

        // Get GUID
        public static string Guid
        {
            get
            {
                Guid guid = System.Guid.NewGuid();
                return guid.ToString();
            }
        }

        // Program running path
        public static string? GetRunPath
        {
            get
            {
                string? filePath = Path.GetDirectoryName(typeof(GlobalConstant).Assembly.Location);
                return filePath;
            }
        }
    }
}
