using MailPusher.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MailPusher.Models
{
    public class Publisher
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Domain { get; set; }
        public string Language { get; set; } //Language of the publisher (two letter language code)
        public int NACEID { get; set; }
        public PublisherStatus Status { get; set; }
        public string Category { get; set; }
        public string State {
            get {
                return Status.ToString();
            }
        }
        public string UpdaterId { get; set; }
        public string CreatorId { get; set; }

        public int ReceivedEmails { get; set; }
        public string LastReceivedEmail { get; set; }
        public string FormatedStatus { get; set; }
        public string NACEDescription { get; set; }
    }
}