namespace Azure.Function.KeyVault.AES.Common.Helpers
{
    using System.Net;
    using Azure.Function.KeyVault.AES.Common.Interfaces;
    using Azure.Security.KeyVault.Secrets;

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
