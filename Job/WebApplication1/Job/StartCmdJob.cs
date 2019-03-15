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
            string cmd = @"CD F:/ZHGC1/R20181101_ZHGC";
            string cmd1 = @"F:";
            string cmd2 = @"git pull";
            string output = "";
            CmdHelper.RunCmd(cmd, cmd1, cmd2, out output);
            LogHelper.Info(DateTime.Now.ToString() + output);
        }
    }
}
