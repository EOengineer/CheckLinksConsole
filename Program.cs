using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using Microsoft.Extensions.Configuration;

namespace CheckLinksConsole
{
    class Program
    {
        
        static void Main(string[] args)
        {
            var config = new Config(args);
            Directory.CreateDirectory(config.Output.GetReportDirectory()); // could also do configuration.GetSection("output").Get<OutputSettings>(); and this would also skipping creating the new outputsettings object
            Console.WriteLine($"Saving file to {config.Output.GetReportFilePath()}");            
            
            var client = new HttpClient();
            var body = client.GetStringAsync(config.Site);
            Console.WriteLine(body.Result);

            Console.WriteLine();
            Console.WriteLine("Links");
            var links = LinkChecker.GetLinks(body.Result);
            links.ToList().ForEach(Console.WriteLine);
            
            // write out links
            // File.WriteAllLines(outputPath, links);
            var checkedLinks = LinkChecker.CheckLinks(links);
            using (var file = File.CreateText(config.Output.GetReportFilePath()))
            // using is like a try block, allows the system to drop the connection if something does wrong
            foreach (var link in checkedLinks.OrderBy(l => l.IsMissing))
            {
                var status = link.IsMissing ? "missing" : "ok";

                file.WriteLine($"{status} - {link.Link}");
            }
        }
    }
}
