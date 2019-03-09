using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StackExchange.Redis;
using Web.Common;

namespace Web.RedisHelper
{
    public class RedisHelp
    {
        private ConnectionMultiplexer redis { get; set; }
        private IDatabase db { get; set; }
        public RedisHelp(string connection)
        {
            redis = ConnectionMultiplexer.Connect(connection);//AppConfigurtaionServices.connectionStrings.Redis);
            db = redis.GetDatabase();
        }

        /// 
        /// 增加/修改
        /// 
        /// 
        /// 
        /// 
        public bool SetValue(string key, string value)
        {
            return db.StringSet(key, value);
        }

        /// 
        /// 查询
        /// 
        /// 
        /// 
        public string GetValue(string key)
        {
            return db.StringGet(key);
        }

        /// 
        /// 删除
        /// 
        /// 
        /// 
        public bool DeleteKey(string key)
        {
            return db.KeyDelete(key);
        }
    }
}
