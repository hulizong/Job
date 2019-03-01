using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace ConfigModel
{
   public class Configurations
    {

        public Setting Setting { get; set; }

        public static Configurations Acquire { get; private set; }
        public static void CreateAcquire(IConfigurationRoot configurationRoot)
        {
            Acquire = new Configurations(configurationRoot);
        }
        public Configurations(IConfigurationRoot configurationRoot)
        {
            this.Setting = new Setting(configurationRoot.GetSection("Setting"));
        }
    }
    public class Setting
    {
        public string Host { get; }
        public Setting(IConfigurationSection section)
        {
            this.Host = section.GetSection("Host").Value;
        }
    }
}
