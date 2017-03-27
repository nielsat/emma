using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailPusher.Repository.Models
{
    [Table("EmailRawHeaders")]
    public class EmailRawHeader
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey("Email")]
        public int EmailID { get; set; }
        public virtual Email Email { get; set; }
        public string HeaderName { get; set; }
        public string HeaderValue { get; set; }
    }
}
