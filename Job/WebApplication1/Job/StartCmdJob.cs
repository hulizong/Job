using Common;
using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Job
{
    public class StartCmdJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            await Start();
        }
        public async Task Start()
        {
            string cmd = @"F:/ZHGCWeb/R20181116_ZHGC_Web";//@"CD F:/ZHGC1/R20181101_ZHGC";
            string cmd1 = @"F:";
            string cmd2 = @"git pull";
            string output = "";
            CmdHelper.RunCmd(cmd, cmd1, cmd2, out output);
            LogHelper.Info("拉取信息：：："+DateTime.Now.ToString()+"---" + output);
        }
    }
}
