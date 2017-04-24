using MailPusher.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailPusher.Common.Helpers
{
    public static class DateRangeHelper
    {
        public static DateRange GetLastWeek() {
            DateTime today = DateTime.UtcNow;
            DateTime mondayOfLastWeek = today.AddDays(-(int)today.DayOfWeek - 6);
            DateTime sundayOfLastWeek = mondayOfLastWeek.AddDays(6);
            return new DateRange()
            {
                Start=mondayOfLastWeek,
                End=sundayOfLastWeek
            };
        }
        public static DateRange GetCurrentWeek() {
            DateTime today = DateTime.UtcNow;
            DateTime mondayOfCurrentWeek = today.AddDays(-(int)today.DayOfWeek + 1);
            DateTime sundayOfCurrentWeek = mondayOfCurrentWeek.AddDays(6);
            return new DateRange()
            {
                Start = mondayOfCurrentWeek,
                End = sundayOfCurrentWeek
            };
        }
        public static DateRange GetLastMonth() {
            DateTime today = DateTime.UtcNow;
            var month = new DateTime(today.Year, today.Month, 1);
            return new DateRange()
            {
                Start = month.AddMonths(-1),
                End = month.AddDays(-1)
            };
        }
        public static DateRange GetCurrentMonth() {
            DateTime today = DateTime.UtcNow;
            var month = new DateTime(today.Year, today.Month, 1);
            return new DateRange()
            {
                Start = month,
                End = month.AddMonths(1).AddDays(-1)
            };
        }
    }
}
