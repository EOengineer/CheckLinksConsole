using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace CheckLinksConsole
{
    public class Config
    {
        // constructor
        public Config(string[] args)
        {
            var inMemory = new Dictionary<string, string>
            {
                {"site", "https://g0t4.github.io/pluralsight-dotnet-core-xplat-apps" },
                {"output:folder", "reports1" }
            };
            var configBuilder = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemory)
                .SetBasePath(Directory.GetCurrentDirectory())
                // set base path sets the directory where
                // file configs will be stored, like checksettings.json
                .AddJsonFile("checksettings.json", true)
                .AddCommandLine(args)
                .AddEnvironmentVariables();

            var configuration = configBuilder.Build();
            ConfigurationRoot = configuration;
            Site = configuration["site"];
            Output = configuration.GetSection("output").Get<OutputSettings>();
        }

        public string Site { get; set; }
        public OutputSettings Output { get; set; }
        public IConfigurationRoot ConfigurationRoot { get; set; }
    }
}
