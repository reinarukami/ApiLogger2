using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ApiLogger.Models.Default
{
    public static class DefaultConfiguration
    {

        private static IConfiguration Configuration()
        {
            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            var configurations = configurationBuilder.Build();

            return configurations;
        }

        public static IConfiguration GetContractConfig()
        {
          var ContractConfiguration = Configuration().GetSection("Configuration");
          return ContractConfiguration;
        }
    }
}
