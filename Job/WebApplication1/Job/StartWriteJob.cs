using Common;
using Quartz;
using Quartz.Util;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Web.Common;
using Web.DBHelper;
using Web.Model;
using Web.RabbitMQ;

namespace Job
{
    public class StartWriteJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            await Start();
        }
        public async Task Start()
        {
          //  Common.LogHelper.Debug(DateTime.Now.ToString() + "测试打印");
            ReceiveMQ.GetMQ<Login>(Test, "CeShi");// Key.PushMQUserKey);

        }
        public static  bool Test(Login info)
        {
            try
            {
                var loginInfo = SqlDapperHelper.ReturnT<Login>("select * from [User] where Id=@Id", new { Id = info.ID });
                UserInfo userInfo = new UserInfo() {
                    Name=loginInfo.Name ,
                    UserId=loginInfo.ID,
                    Sex=loginInfo.Sex
                };
                SqlDapperHelper.Insert(userInfo);
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
                return false;
            }
            return true;
        }
    }
}
