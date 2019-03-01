using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace ConfigModel
{
   public class Configurations
    {

        public Setting Setting { get; set; }   //创建读取配置文件

        public static Configurations Acquire { get; private set; }
        public static void CreateAcquire(IConfigurationRoot configurationRoot)  //创建链接
        {
            Acquire = new Configurations(configurationRoot);
        }
        public Configurations(IConfigurationRoot configurationRoot)   //链接读取配置文件
        {
            this.Setting = new Setting(configurationRoot.GetSection("Setting"));
        }
    }
    /// <summary>
    /// 读取配置文件中的具体信息
    /// </summary>
    public class Setting
    {
        public string Host { get; }
        public Setting(IConfigurationSection section)
        {
            this.Host = section.GetSection("Host").Value;
        }
    }
}
