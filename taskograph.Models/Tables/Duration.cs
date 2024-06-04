using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace taskograph.Models.Tables
{
    public class Duration
    {
        public int Id { get; set; }
        public long Minutes { get; set; }
        public ICollection<Entry> Entries { get; set; }
        [InverseProperty(nameof(RegularTarget.TimeDedicatedToPerformTarget))]
        public ICollection<RegularTarget> TargetRegularTargets { get; set; }
        [InverseProperty(nameof(RegularTarget.RegularTimeIntervalToAchieveTarget))]
        public ICollection<RegularTarget> PerTimeframeRegularTargets { get; set; }


        public override string ToString()                   //TODO implement Months, and Years
        {
            if (Minutes == null || Minutes == 0) return "";
            TimeSpan span = TimeSpan.FromMinutes(Minutes);
            string formatted = "";
            if (span.Duration().TotalDays > 360)
            {
                int years = ((int)span.Duration().TotalDays) / 360;
                int days = ((int)span.Duration().TotalDays) % 360;
                formatted = string.Format("{0}{1}",
                    string.Format("{0:0}year{1}, ", years, years == 1 ? string.Empty : "s"),
                    days > 0 ? string.Format("{0:0}day{1}", days, days == 1 ? string.Empty : "s") : string.Empty);
            }
            if (span.Duration().TotalDays > 28)
            {
                int months = ((int)span.Duration().TotalDays) / 28;
                int days = ((int)span.Duration().TotalDays) % 28;
                formatted = string.Format("{0}{1}",
                    string.Format("{0:0}month{1}, ", months, months == 1 ? string.Empty : "s"),
                    days > 0 ? string.Format("{0:0}day{1}", days, days == 1 ? string.Empty : "s") : string.Empty);
            }
            else if (span.Duration().TotalDays > 7)
            {
                int weeks = ((int)span.Duration().TotalDays) / 7;
                int days = ((int)span.Duration().TotalDays) % 7;
                formatted = string.Format("{0}{1}",
                    string.Format("{0:0}week{1}, ", weeks, weeks == 1 ? string.Empty : "s"),
                    days > 0 ? string.Format("{0:0}day{1}", days, days == 1 ? string.Empty : "s") : string.Empty);
            }
            else
            {
                formatted = string.Format("{0}{1}{2}",
                 span.Duration().Days > 0 ? string.Format("{0:0}day{1}", span.Days, span.Days == 1 ? string.Empty : "s") : string.Empty,
                 span.Duration().Hours > 0 ? string.Format("{0:0}hr", span.Hours) : string.Empty,
                 span.Duration().Minutes > 0 ? string.Format("{0:0}min", span.Minutes) : string.Empty);
            }


            if (string.IsNullOrEmpty(formatted)) return "";
            return formatted;
        }

        public static Duration operator +(Duration d1, Duration d2)=>  new Duration() { Minutes = (d1?.Minutes ?? 0) + (d2?.Minutes ?? 0) };
        
 
    }
}
