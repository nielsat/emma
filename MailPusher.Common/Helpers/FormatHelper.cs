using MailPusher.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MailPusher.Common.Helpers
{
    public static class FormatHelper
    {
        public static string ConvertDateToString(DateTime date)
        {
            DateTime utcDate = date.ToUniversalTime();
            string prefix = string.Empty;
            int dayDiff = (DateTime.UtcNow - utcDate).Days;
            switch (dayDiff) {
                case 0:
                    prefix = "Today";
                    break;
                case 1:
                    prefix = "Yesterday";
                    break;
                default:
                    prefix = utcDate.DayOfWeek.ToString();
                    break;
            }
            return string.Format("{0} {1:yyyy.MM.dd} at {1:hh:mm} GMT", prefix, utcDate);
        }

        public static string ConvertDateToShortFullDateString(DateTime date)
        {
            DateTime utcDate = date.ToUniversalTime();
            string prefix = string.Empty;
            return string.Format("{0} {1:yyyy.MM.dd} at {1:hh:mm} GMT", System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.AbbreviatedDayNames[(int)utcDate.DayOfWeek], utcDate);
        }

        public static string ConvertDateToShortString(DateTime date)
        {
            DateTime utcDate = date.ToUniversalTime();
            return string.Format("{0:yyyy.MM.dd}", utcDate);
        }

        public static string GetFormtedStatus(PublisherStatus status, DateTime? changed)
        {
            string datePart = string.Empty;
            if (changed.HasValue)
            {
                datePart = string.Format("({0:yyyy-MM-dd})", changed.Value.ToUniversalTime());
            }
            return string.Format("{0} {1}", status.ToString(), datePart);
        }

        public static double GetJavascriptDate(DateTime date)
        {
            return date.ToUniversalTime()
               .Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc))
               .TotalMilliseconds;
        }
    }
}
