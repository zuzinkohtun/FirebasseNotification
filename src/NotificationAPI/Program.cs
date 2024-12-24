using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NotificationAPI.Configs;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace NotificationAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IHost host = CreateHostBuilder(args).Build();
            IWebHostEnvironment environment = host.Services.GetRequiredService<IWebHostEnvironment>();
            var env = environment.EnvironmentName;
            var configuration = new ConfigurationBuilder()
                  .AddJsonFile($"appsettings.{env}.json", optional: true, reloadOnChange: true)
                  .Build();

            var firebaseCredential = new FirebaseCredential();
            configuration.GetSection(nameof(FirebaseCredential)).Bind(firebaseCredential);

            FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromJson(JsonConvert.SerializeObject(firebaseCredential)),
            });

            host.Run();

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
