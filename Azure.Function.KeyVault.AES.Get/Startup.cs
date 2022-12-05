using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Function.KeyVault.AES.Common.Models;
using Azure.Function.KeyVault.AES.Common.Interfaces;
using Azure.Function.KeyVault.AES.Common.Helpers;

[assembly: FunctionsStartup(typeof(Azure.Function.KeyVault.AES.Encryption.Startup))]
namespace Azure.Function.KeyVault.AES.Encryption
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddScoped(provider => new SecretClient(new Uri(Config.AzureKeyVaultBaseUri), new DefaultAzureCredential()));
            builder.Services.AddScoped<IAzureKeyVaultHelper, AzureKeyVaultHelper>();
        }
    }
}