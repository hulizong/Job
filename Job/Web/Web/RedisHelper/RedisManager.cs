using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Common;
using Web.RedisHelper;
namespace Web.RedisHelper
{
    public class RedisManager
    {
        static RedisManager()
        {
            if (!string.IsNullOrEmpty(AppConfigurtaionServices.connectionStrings.Redis))
                redisHelp = new RedisHelp(AppConfigurtaionServices.connectionStrings.Redis);
        }
        public static RedisHelp redisHelp { get; set; }
    }
}
