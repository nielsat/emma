using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailPusher.Common.Enums
{
    public enum PublisherStatus
    {
        None=1,
        Subscribed=2,
        Confirmed=4,
        Error_SubscriptionNotFound = 8,
        Error_NotAllowedToSubscribe = 16,
        Error_DoesNotExist = 32,
        Error_ConfirmNotFound = 64
    }
}
