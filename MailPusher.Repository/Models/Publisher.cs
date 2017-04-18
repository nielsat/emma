using MailPusher.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailPusher.Repository.Models
{
    [Table("Publishers")]
    public class Publisher
    {
        [Key]
        public int ID { get; set; } 
        public string Name { get; set; }
        public string Domain { get; set; }
        public string Language { get; set; } //Language of the publisher (two letter language code)
        [ForeignKey("NACE")]
        public int NACEID { get; set; }
        public virtual NACE NACE {get;set;}
        public PublisherStatus Status { get; set; }
        public DateTime? LastReceivedEmail { get; set; }
        public DateTime Created { get; set; }
        [ForeignKey("Creator")]
        public string CreatorId { get; set; }
        public virtual AppUser Creator { get; set; }
        public DateTime Updated { get; set; }
        [ForeignKey("Updater")]
        public string UpdaterId { get; set; }
        public virtual AppUser Updater { get; set; }
        public DateTime? StatusChanged { get; set; }
        public string CountryCompanyCode { get; set; }
        public int AmountEmployees { get; set; }
    }
}
