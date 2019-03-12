using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Common
{
    public class AppConfigurtaionServices
    {
        public static IConfiguration Configuration { get; set; }
        static AppConfigurtaionServices()
        {
            //ReloadOnChange = true 当appsettings.json被修改时重新加载            
            Configuration = new ConfigurationBuilder()
            .Add(new JsonConfigurationSource { Path = "appsettings.json", ReloadOnChange = true })
            .Build();
            connectionStrings= new ConnectionStrings(Configuration);
            setting = new Setting(Configuration);
        }
        public static ConnectionStrings connectionStrings { get; }
        public  class ConnectionStrings
        {
            public  string DapperRead { get; }
            public string DapperWrite { get; }
            public string Redis { get; } 
            public string RabbitMqHostName { get; }
            public string RabbitMqUserName { get; }
            public string RabbitMqPassword { get; } 
            public ConnectionStrings(IConfiguration Configuration)
            { 
                this.DapperRead=  Configuration.GetConnectionString("DapperRead");
                this.DapperWrite = Configuration.GetConnectionString("DapperWrite");
                this.Redis = Configuration.GetConnectionString("Redis"); 
                this.RabbitMqHostName = Configuration.GetConnectionString("RabbitMqHostName");
                this.RabbitMqUserName = Configuration.GetConnectionString("RabbitMqUserName");
                this.RabbitMqPassword = Configuration.GetConnectionString("RabbitMqPassword");
            }

        }

        public static Setting setting { get; }
        public class Setting
        {
           
            public string Host { get; }
            public string ApiHost { get; }
            public string MQKey { get; set; }
            public Setting(IConfiguration Configuration)
            {
                 
                this.Host = Configuration.GetConnectionString("Host");
                this.ApiHost = Configuration.GetConnectionString("ApiHost");
                this.MQKey = Configuration.GetConnectionString("MQKey");
            }


        }
    }

  
}
