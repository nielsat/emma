using MailPusher.Scheduler.Schedulers;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailPusher.Scheduler
{
    public class MainScheduler
    {
        public static void Start() {
            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
            scheduler.Start();

            EmailProcessorScheduler.Schedule(scheduler);
        }
    }
}
