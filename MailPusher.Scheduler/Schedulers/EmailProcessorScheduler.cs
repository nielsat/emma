using MailPusher.Common.Helpers;
using MailPusher.Scheduler.Jobs;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailPusher.Scheduler.Schedulers
{
    public class EmailProcessorScheduler
    {
        public static void Schedule(IScheduler scheduler)
        {
            IJobDetail job = JobBuilder.Create<EmailProcessorJob>().Build();

            ITrigger trigger = TriggerBuilder.Create()
            .WithIdentity("EmailProcessor", "EmailProcessorGroup")
            .StartNow()
            .WithSimpleSchedule(x => x
            .WithIntervalInSeconds(Convert.ToInt32(AppSettingsHelper.GetValueFromAppSettings(Common.Enums.AppSettingsKey.emailParsingSchedulerIntervalInSeconds)))
            .RepeatForever())
            .Build();

            scheduler.ScheduleJob(job, trigger);
        }
    }
}
