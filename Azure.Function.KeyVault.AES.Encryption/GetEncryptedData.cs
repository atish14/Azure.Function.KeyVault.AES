using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Security.Cryptography;
using Azure.Function.KeyVault.AES.Common.Interfaces;
using Azure.Function.KeyVault.AES.Common.Models;

namespace Azure.Function.KeyVault.AES.Encryption
{
    public class GetEncryptedData
    {
        private readonly IAzureKeyVaultHelper _azureKeyVaultHelper;

        public GetEncryptedData(IAzureKeyVaultHelper azureKeyVaultHelper)
        {
            _azureKeyVaultHelper = azureKeyVaultHelper;
        }

        [FunctionName("GetEncryptedData")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "fetch")] HttpRequest request, ILogger log)
        {
            if (request.Query == null || request.Query.Count <= 0)
            {
                return new BadRequestObjectResult("Invalid Request");
            }
            log.LogInformation($"GetEncryptedData function executing with data:{request.Query["data"]}");

            string plainText = request.Query["data"];

            log.LogInformation($"Fetching keySecretValue from keyvault for key: {Config.EncryptionKeyName}");
            string keySecretValue = await _azureKeyVaultHelper.GetSecretValueAsync(Config.EncryptionKeyName).ConfigureAwait(false);
            if (string.IsNullOrWhiteSpace(keySecretValue))
            {
                return new NotFoundObjectResult($"Key not found - {nameof(Config.EncryptionKeyName)}");
            }
            byte[] encryptionKey = Convert.FromBase64String(keySecretValue);

            log.LogInformation($"Fetching vectorSecretValue from keyvault for key: {Config.EncryptionIVName}");
            string vectorSecretValue = await _azureKeyVaultHelper.GetSecretValueAsync(Config.EncryptionIVName).ConfigureAwait(false);
            if (string.IsNullOrWhiteSpace(keySecretValue))
            {
                return new NotFoundObjectResult($"Key not found - {nameof(Config.EncryptionIVName)}");
            }
            byte[] encryptionVector = Convert.FromBase64String(vectorSecretValue);

            log.LogInformation($"Encrypting string: {plainText}");
            string encryptedText = EncryptData(plainText, encryptionKey, encryptionVector);

            return new OkObjectResult(encryptedText);
        }

        private static string EncryptData(string plainText, byte[] key, byte[] initalizationVector)
        {
            byte[] encryptedText;

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = key;
                aesAlg.IV = initalizationVector;

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using MemoryStream msEncrypt = new();
                using CryptoStream csEncrypt = new(msEncrypt, encryptor, CryptoStreamMode.Write);
                using (StreamWriter swEncrypt = new(csEncrypt))
                {
                    swEncrypt.Write(plainText);
                }
                encryptedText = msEncrypt.ToArray();
            }
            return Convert.ToBase64String(encryptedText);
        }
    }
}