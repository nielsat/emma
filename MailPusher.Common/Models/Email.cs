using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailPusher.Common.Models
{
    public class Email
    {
        public Email()
        {
            EmailHeaders = new List<EmailHeader>();
        }

        public List<EmailHeader> EmailHeaders { get; set; }

        public string HTML { get; set; }
        public string Text { get; set; }
        public string HTMLText { get; set; }

        public int PublisherID { get; set; }
        public DateTime ReceivedGMT { get; set; }
        public string SubjectLine { get; set; }
        public string SenderName { get; set; }
        public string SenderAddress { get; set; }
        public string Copy { get; set; }
    }
}
