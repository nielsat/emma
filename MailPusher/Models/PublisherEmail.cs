using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MailPusher.Models
{
    public class PublisherEmail
    {
        public string PublisherId { get; set; }
        public string Domain { get; set; }
        public string SubscriberEmail { get; set; }
        public string PublisherName { get; set; }
    }
}