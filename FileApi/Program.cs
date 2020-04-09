using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace FileApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args)
                .Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureLogging((hostingContext, builder) =>
                    {
                        var outputTemplate =
                            "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] ({SourceContext}) {Message}{NewLine}{Exception}";
                        builder.AddFile("logs/FileApi-{HalfHour}.log",
                            outputTemplate: outputTemplate,
                            levelOverrides: new Dictionary<string, LogLevel>
                                {{"Microsoft", LogLevel.Critical}, {"System", LogLevel.Critical}},
                            fileSizeLimitBytes: 200_000_000);
                    }).UseStartup<Startup>().UseUrls("http://localhost:5002/");
                });
        }
    }
}