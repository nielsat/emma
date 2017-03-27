using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MailPusher.Models
{
    public class Email
    {
        public int ID { get; set; }
        public int PublisherID { get; set; }
        public string SubjectLine { get; set; }
        public string ReceivedGMT { get; set; }
        public string SenderName { get; set; }
        public string SenderAddress { get; set; }
        public string Copy { get; set; }
    }
}