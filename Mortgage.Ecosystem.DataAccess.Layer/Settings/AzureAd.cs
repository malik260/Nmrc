namespace Mortgage.Ecosystem.DataAccess.Layer.Settings
{
    public class AzureAd
    {
        public string? Instance { get; set; }
        public string? Authority { get; set; }
        public string? Domain { get; set; }
        public string? TenantId { get; set; }
        public string? ClientId { get; set; }
        public string? ClientSecret { get; set; }
        public string? CallbackPath { get; set; }
        public string? SignedOutCallbackPath { get; set; }
        public string? RemoteSignOutPath { get; set; }
    }
}
