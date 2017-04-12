using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailPusher.Common.Models.Reports
{
    public class TimeChartLine
    {
        public string Title { get; set; }
        public string SeriesAxis { get; set; }
        public string AxesLabel { get; set; }
        public List<TimeChartPoint> Points { get; set; }
    }
}
