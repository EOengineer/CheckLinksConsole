using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using Microsoft.Extensions.Configuration;

namespace CheckLinksConsole
{
    using Microsoft.Extensions.Logging;
    class Program
    {
        
        static void Main(string[] args)
        {
            // it is a convention to pass the applications fully qualified class name
            // as an overload into the logger to aid in more descriptive logging since it includes
            // the namespace and everything
            var config = new Config(args);
            Logs.Init(config.ConfigurationRoot);
            var logger = Logs.Factory.CreateLogger<Program>();
            
            Directory.CreateDirectory(config.Output.GetReportDirectory()); // could also do configuration.GetSection("output").Get<OutputSettings>(); and this would also skipping creating the new outputsettings object
            
            logger.LogInformation($"Saving file to {config.Output.GetReportFilePath()}");           
            var client = new HttpClient();
            var body = client.GetStringAsync(config.Site);
            logger.LogDebug(body.Result);

            var links = LinkChecker.GetLinks(config.Site, body.Result);            
            // write out links
            // File.WriteAllLines(outputPath, links);
            var checkedLinks = LinkChecker.CheckLinks(links);
            using (var file = File.CreateText(config.Output.GetReportFilePath()))
            using (var linksDb = new LinksDb())
            // using is like a try block, allows the system to drop the connection if something does wrong
            {
                foreach (var link in checkedLinks.OrderBy(l => l.IsMissing))
                {
                    var status = link.IsMissing ? "missing" : "ok";
                    file.WriteLine($"{status} - {link.Link}");
                    linksDb.Links.Add(link);
                }
                linksDb.SaveChanges();
            }
        }
    }
}
