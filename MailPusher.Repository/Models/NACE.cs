using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailPusher.Repository.Models
{
    [Table("NACE")]
    public class NACE
    {
        [Key]
        public int ID { get; set; }

        public int Order { get; set; }
        public int Level { get; set; }

        public string Code { get; set; }
        public string Description { get; set; }
        public string ISICRef { get; set; }
        public string Includes { get; set; }
        public string IncludesAlso { get; set; }
        public string Rulings { get; set; }
        public string Excludes { get; set; }
        public string ParentCode { get; set; }
        public int? ParentId { get; set; }
    }
}
