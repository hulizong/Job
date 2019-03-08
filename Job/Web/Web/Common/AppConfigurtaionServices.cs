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

        }
        public static ConnectionStrings connectionStrings { get; }
        public  class ConnectionStrings
        {
            public  string DapperRead { get; }
            public string DapperWrite { get; }
            public string RedisConnMain { get; }
            public string RedisConnVice { get; }
            public string RabbitMqHostName { get; }
            public string RabbitMqUserName { get; }
            public string RabbitMqPassword { get; } 
            public ConnectionStrings(IConfiguration Configuration)
            { 
                this.DapperRead=  Configuration.GetConnectionString("DapperRead");
                this.DapperWrite = Configuration.GetConnectionString("DapperWrite");
                this.RedisConnMain = Configuration.GetConnectionString("RedisConnMain");
                this.RedisConnVice = Configuration.GetConnectionString("RedisConnVice");
                this.RabbitMqHostName = Configuration.GetConnectionString("RabbitMqHostName");
                this.RabbitMqUserName = Configuration.GetConnectionString("RabbitMqUserName");
                this.RabbitMqPassword = Configuration.GetConnectionString("RabbitMqPassword");
            }

        }

    }

  
}
