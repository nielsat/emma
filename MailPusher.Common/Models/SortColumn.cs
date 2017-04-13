using MailPusher.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailPusher.Common.Models
{
    public class SortColumn
    {
        public SortingOrder SortingOrder { get; set; }
        public SortColumnName SortColumnName { get; set; }
    }
}
