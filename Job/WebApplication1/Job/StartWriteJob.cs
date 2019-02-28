using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

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
            Common.LogHelper.Debug(DateTime.Now.ToString()+ "测试打印");
        }
    }
}
