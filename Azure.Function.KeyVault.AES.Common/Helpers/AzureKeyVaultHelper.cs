using Azure.Function.KeyVault.AES.Common.Interfaces;
using Azure.Security.KeyVault.Secrets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Azure.Function.KeyVault.AES.Common.Helpers
{
    public class AzureKeyVaultHelper : IAzureKeyVaultHelper
    {
        private readonly SecretClient _secretClient;

        public AzureKeyVaultHelper(SecretClient secretClient)
        {
            _secretClient = secretClient;
        }

        public async Task<string> GetSecretValueAsync(string secretKeyName)
        {
            string secretValue = string.Empty;
            var response = await _secretClient.GetSecretAsync(secretKeyName).ConfigureAwait(false);

            if (response.GetRawResponse().Status == (int)HttpStatusCode.OK && response.Value != null)
            {
                secretValue = response.Value.Value;
            }
            return secretValue;
        }
    }
}
