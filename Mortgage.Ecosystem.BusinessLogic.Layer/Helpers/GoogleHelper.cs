using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Mortgage.Ecosystem.DataAccess.Layer.Configurations;
using Mortgage.Ecosystem.DataAccess.Layer.Exceptions;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Helpers
{
    internal class GoogleHelper
    {
        private readonly GoogleConfig _google;

        public GoogleHelper(GoogleConfig google)
        {
            _google = google ?? throw new ConfigurationException(@"The Google Workspace configuration has not been added.");
        }

        public BaseClientService.Initializer GetInitializer(string? accountName = null, params string[] scopes)
        {
            var credPath = _google.CredentialPath;

            if (string.IsNullOrWhiteSpace(accountName))
            {
                accountName = _google.DefaultAccountName;
            }

            var originCredential =
                (ServiceAccountCredential)GoogleCredential.FromFile(credPath)
                    .UnderlyingCredential;

            var initializer = new ServiceAccountCredential.Initializer(originCredential.Id)
            {
                User = accountName,
                Key = originCredential.Key,
                Scopes = scopes
            };

            var credential = new ServiceAccountCredential(initializer);

            return new BaseClientService.Initializer
            {
                ApplicationName = "Portal",
                HttpClientInitializer = credential
            };
        }
    }
}
