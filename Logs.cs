using System;
using System.Collections.Generic;
using System.Text;

namespace CheckLinksConsole
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;

    // made static so there is only 1 instance of it
    // which will be shared throughout the app
    public static class Logs
    {
        // this is a publicly available field accessible from anywhere in the app
        public static LoggerFactory Factory = new LoggerFactory();

        // was a constructor, but changed to an initialization method
        public static void Init(IConfiguration configuration)
        {
            // Factory.AddConsole(LogLevel.Trace, includeScopes: true);
            Factory.AddConsole(configuration.GetSection("Logging"));
            Factory.AddFile("logs/checklinks-{Date}.json",
                isJson: true,
                minimumLevel: LogLevel.Trace);
        }
    }
}