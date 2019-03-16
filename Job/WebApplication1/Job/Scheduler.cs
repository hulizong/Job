using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Threading.Tasks;

namespace Job
{
    public class Scheduler
    {

        private IScheduler scheduler;
        public async Task Start()
        {
            await JobStart();
        }
        public async Task JobStart()
        {
            NameValueCollection props = new NameValueCollection { { "quartz.serializer.type", "binary" } };
            StdSchedulerFactory factory = new StdSchedulerFactory(props);
            scheduler = await factory.GetScheduler();  //从工厂中获取一个调度器实例化

            //开始启动调度器
            await scheduler.Start();

            // await CreateJob<StartWriteJob>("测试","第一组", "0/30 * * * * ? ");
            //执行cmd命令
            await CreateJob<StartCmdJob>("_StartCmdJob", "_StartCmdJob", "0 0 0/1 * * ?  ");
        }

        /// <summary>
        /// 停止调度器
        /// </summary>
        public void Stop()
        {
            scheduler.Shutdown();
            scheduler = null;//清除
        }

        private async Task CreateJob<T>(string name,string group ,string cronTime) where T : IJob
        {
            //创建一个作业
            var job = JobBuilder.Create<T>()
                .WithIdentity("name" + name, "gtoup" + group)
                .Build();
            //创建一个触发器
            var tigger = (ICronTrigger)TriggerBuilder.Create()
                .WithIdentity("name" + name, "group" + group)
                .StartNow()
                .WithCronSchedule(cronTime)
                .Build();
            //把作业和触发器放入调度器中
            await scheduler.ScheduleJob(job,tigger);
        }
    }
}
