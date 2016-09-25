using System;
using Quartz;
using Quartz.Impl;

namespace VIPAC.Common.Quartz
{
    public class QuartzJobCore
    {
        private readonly IScheduler _sched;

        public QuartzJobCore()
        {
            ISchedulerFactory sf = new StdSchedulerFactory();
            _sched = sf.GetScheduler();
        }

        public void AddCronTrigger<T>(string cron, string triggerName, string triggerGroup, string jobName, string jobGroup) where T : IJob
        {
            IJobDetail job = JobBuilder.Create<T>().WithIdentity(jobName, jobGroup).Build();

            var trigger = (ICronTrigger)TriggerBuilder.Create()
                .WithIdentity(triggerName, triggerGroup)
                .WithCronSchedule(cron)
                .Build();

            _sched.ScheduleJob(job, trigger);
        }

        public void AddCronTrigger<T>(string cron, string triggerName, string triggerGroup, string jobName, string jobGroup,
            DateTime fechafin) where T : IJob
        {
            IJobDetail job = JobBuilder.Create<T>().WithIdentity(jobName, jobGroup).Build();

            DateTimeOffset endDate = DateBuilder.DateOf(23, 59, 59, fechafin.Day, fechafin.Month, fechafin.Year);

            var trigger = (ICronTrigger)TriggerBuilder.Create()
                .WithIdentity(triggerName, triggerGroup)
                .WithCronSchedule(cron)
                .EndAt(endDate)
                .Build();

            _sched.ScheduleJob(job, trigger);
        }

        public IJobDetail GetJob(string jobName, string jobGroup)
        {
            return _sched.GetJobDetail(new JobKey(jobName, jobGroup));
        }

        public void DeleteJob(string jobName, string jobGroup)
        {
            _sched.DeleteJob(new JobKey(jobName, jobGroup));
        }

        public void Start()
        {
            _sched.Start();
        }

        public void Stop()
        {
            _sched.Shutdown(true);
        }
    }
}