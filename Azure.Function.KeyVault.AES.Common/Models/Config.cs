using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azure.Function.KeyVault.AES.Common.Models
{
    public static class Config
    {
        static Config()
        {
            EncryptionKeyName = Environment.GetEnvironmentVariable(nameof(EncryptionKeyName));
            EncryptionIVName = Environment.GetEnvironmentVariable(nameof(EncryptionIVName));
            AzureKeyVaultBaseUri = Environment.GetEnvironmentVariable(nameof(AzureKeyVaultBaseUri));
            SearchTimeRangeBefore = Environment.GetEnvironmentVariable(nameof(SearchTimeRangeBefore));
            SearchTimeRangeAfter = Environment.GetEnvironmentVariable(nameof(SearchTimeRangeAfter));
            LoadSearchApiUri = Environment.GetEnvironmentVariable(nameof(LoadSearchApiUri));
            IdentityServerUri = Environment.GetEnvironmentVariable(nameof(IdentityServerUri));
            ClientScope = Environment.GetEnvironmentVariable(nameof(ClientScope));
            ClientId = Environment.GetEnvironmentVariable(nameof(ClientId));
            ClientSecret = Environment.GetEnvironmentVariable(nameof(ClientSecret));
            ValidateIssuerName = Environment.GetEnvironmentVariable(nameof(ValidateIssuerName));
            RequireHttpsMetadata = Environment.GetEnvironmentVariable(nameof(RequireHttpsMetadata));
            GrantType = Environment.GetEnvironmentVariable(nameof(GrantType));
        }

        public static string EncryptionKeyName { get; set; }
        public static string EncryptionIVName { get; set; }
        public static string AzureKeyVaultBaseUri { get; set; }
        public static string SearchTimeRangeBefore { get; set; }
        public static string SearchTimeRangeAfter { get; set; }
        public static string LoadSearchApiUri { get; set; }
        public static string IdentityServerUri { get; set; }
        public static string ClientScope { get; set; }
        public static string ClientId { get; set; }
        public static string ClientSecret { get; set; }
        public static string ValidateIssuerName { get; set; }
        public static string RequireHttpsMetadata { get; set; }
        public static string GrantType { get; set; }
    }
}
