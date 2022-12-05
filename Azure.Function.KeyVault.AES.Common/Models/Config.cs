namespace Azure.Function.KeyVault.AES.Common.Models
{
    public static class Config
    {
        static Config()
        {
            EncryptionKeyName = Environment.GetEnvironmentVariable(nameof(EncryptionKeyName));
            EncryptionIVName = Environment.GetEnvironmentVariable(nameof(EncryptionIVName));
            AzureKeyVaultBaseUri = Environment.GetEnvironmentVariable(nameof(AzureKeyVaultBaseUri));
        }

        public static string? EncryptionKeyName { get; set; }
        public static string? EncryptionIVName { get; set; }
        public static string? AzureKeyVaultBaseUri { get; set; }
    }
}
