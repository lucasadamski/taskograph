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
        [InverseProperty(nameof(RegularTarget.TargetDuration))]
        public ICollection<RegularTarget> TargetRegularTargets { get; set; }
        [InverseProperty(nameof(RegularTarget.PerTimeframeDuration))]
        public ICollection<RegularTarget> PerTimeframeRegularTargets { get; set; }


        public override string ToString()
        {
            TimeSpan span = TimeSpan.FromMinutes(Minutes);

            string formatted = string.Format("{0}{1}{2}{3}",
            span.Duration().Days > 0 ? string.Format("{0:0} day{1}, ", span.Days, span.Days == 1 ? string.Empty : "s") : string.Empty,
            span.Duration().Hours > 0 ? string.Format("{0:0} hour{1}, ", span.Hours, span.Hours == 1 ? string.Empty : "s") : string.Empty,
            span.Duration().Minutes > 0 ? string.Format("{0:0} minute{1}, ", span.Minutes, span.Minutes == 1 ? string.Empty : "s") : string.Empty);

            return formatted;
        }

        public static Duration operator +(Duration d1, Duration d2)=>  new Duration() { Minutes = d1.Minutes + d2.Minutes };
        
 
    }
}
