using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MailPusher.Models
{
    public class NACE
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public string ParentCode { get; set; }
        public string Excludes { get; set; }
        public string Includes { get; set; }
        public string Rulings { get; set; }
        public string Code { get; set; }

    }
}