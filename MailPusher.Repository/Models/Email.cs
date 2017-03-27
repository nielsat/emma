using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailPusher.Repository.Models
{
    [Table("Emails")]
    public class Email
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("Publisher")]
        public int PublisherID { get; set; }
        public virtual Publisher Publisher { get; set; }
        public DateTime ReceivedGMT { get; set; }
        public string SubjectLine { get; set; }
        public string SenderName { get; set; }
        public string SenderAddress { get; set; }
        public string Copy { get; set; }
    }
}
