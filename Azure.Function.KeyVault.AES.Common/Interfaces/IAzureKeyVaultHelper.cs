namespace Azure.Function.KeyVault.AES.Common.Interfaces
{
    public interface IAzureKeyVaultHelper
    {
        Task<string> GetSecretValueAsync(string secretKeyName);
    }
}
