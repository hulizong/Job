using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Common
{
    public class Key
    {
        public static string Mq = AppConfigurtaionServices.setting.MQKey;
        public static string PushMQUserKey = Mq + "CeShi";
    }
}
