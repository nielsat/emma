using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailPusher.Common.Models
{
    public class Metrics
    {
        public int TotalSubscriptions { get; set; }
        public int TotalConfirmedSubscriptions { get; set; }
        public int ConfirmedSubscriptionsLastWeek { get; set; }
        public int ConfirmedSubscriptionsThisWeek { get; set; }
        public int ConfirmedSubscriptionsLastMonth { get; set; }
        public int ConfirmedSubscriptionsThisMonth { get; set; }
        public int SubscriptionsBeforeTarget
        {
            get
            {
                return 3000 - TotalSubscriptions;
            }
        }
    }
}
