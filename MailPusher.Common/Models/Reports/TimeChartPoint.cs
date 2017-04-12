using MailPusher.Common.Helpers;
using System;

namespace MailPusher.Common.Models.Reports
{
    public class TimeChartPoint
    {
        public DateTime Date { get; set; }
        public double JavaScriptDate { get { return FormatHelper.GetJavascriptDate(Date); } }
        public int Value { get; set; }
        public string StringDate { get { return FormatHelper.ConvertDateToShortString(Date); } }
    }
}
