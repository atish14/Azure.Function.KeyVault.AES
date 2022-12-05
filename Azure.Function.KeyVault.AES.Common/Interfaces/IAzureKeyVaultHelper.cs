using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azure.Function.KeyVault.AES.Common.Interfaces
{
    public interface IAzureKeyVaultHelper
    {
        Task<string> GetSecretValueAsync(string secretKeyName);
    }
}
