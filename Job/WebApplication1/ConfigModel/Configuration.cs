using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace ConfigModel
{
   public class Configurations
    {

        public Setting Setting { get; set; }   //创建读取配置文件
        public ConnectionStrings ConnectionStrings { get; set; }   //创建读取配置文件

        public static Configurations Acquire { get; private set; }
        public static void CreateAcquire(IConfigurationRoot configurationRoot)  //创建链接
        {
            Acquire = new Configurations(configurationRoot);
        }
        public Configurations(IConfigurationRoot configurationRoot)   //链接读取配置文件
        {
            this.Setting = new Setting(configurationRoot.GetSection("Setting"));
            this.ConnectionStrings = new ConnectionStrings(configurationRoot.GetSection("ConnectionStrings"));
        }
    }
    /// <summary>
    /// 读取配置文件中的具体信息
    /// </summary>
    public class Setting
    {
        public string Host { get; }
        public string MQKey { get; set; }
        public Setting(IConfigurationSection section)
        {
            this.Host = section.GetSection("Host").Value;
            this.MQKey = section.GetSection("MQKey").Value;
        }
    }
    public class ConnectionStrings
    {
        public string RabbitMqHostName { get; }
        public string RabbitMqUserName { get; }
        public string RabbitMqPassword { get; }
        public string DapperWrite { get; set; }
        public string DapperRead { get; set; }
        public ConnectionStrings(IConfigurationSection section)
        {
            this.RabbitMqHostName = section.GetSection("RabbitMqHostName").Value;
            this.RabbitMqUserName = section.GetSection("RabbitMqUserName").Value;
            this.RabbitMqPassword = section.GetSection("RabbitMqPassword").Value;
            this.DapperWrite = section.GetSection("DapperWrite").Value;
            this.DapperRead = section.GetSection("DapperRead").Value;
        }
    }
}
