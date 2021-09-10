using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace MyMvc.Models
{
    public class AppConfigurtaionHelper
    {
        public static IConfiguration Configuration { get; set; }

        static AppConfigurtaionHelper()
        {      
            Configuration = new ConfigurationBuilder()
            .Add(new JsonConfigurationSource { Path = "appsettings.json", ReloadOnChange = true })
            .Build();
        }
    }
}
