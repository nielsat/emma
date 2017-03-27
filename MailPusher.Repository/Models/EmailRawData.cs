using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailPusher.Repository.Models
{
    [Table("EmailRawData")]
    public class EmailRawData
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("Email")]
        public int EmailID { get; set; }
        public virtual Email Email { get; set; }
        public string HTML { get; set; }
        public string Text { get; set; }
        public string HTMLText { get; set; }
    }
}
