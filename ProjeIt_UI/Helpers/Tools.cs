using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using System;

namespace ProjeIt_UI.Helpers
{
    public class Tools : ComponentBase
    {
        private static IConfigurationRoot _configuration;
        public static string GetApiUrl
        {
            get
            {
                _configuration ??= new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();
                return _configuration.GetSection("BaseUrl:Value").Value;
            }

        }
    }
}