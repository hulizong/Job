using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Common;

namespace Web.RabbitMQ
{
    public class ConnectionMQ
    {
        /// <summary>
        /// 创建MQ连接
        /// </summary>
        /// <returns></returns>
        public static IConnection Connection()
        {
            //创建连接工厂    
            ConnectionFactory factory = new ConnectionFactory
            {
                UserName = AppConfigurtaionServices.connectionStrings.RabbitMqUserName,//用户名
                Password = AppConfigurtaionServices.connectionStrings.RabbitMqPassword,//密码
                HostName = AppConfigurtaionServices.connectionStrings.RabbitMqHostName//rabbitmq ip
            };

            //创建连接
            var connection = factory.CreateConnection();
            return connection;
        }
    }
}
