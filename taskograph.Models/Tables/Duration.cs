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
        public int? Minutes { get; set; }
        public int? Hours { get; set; }
        public int? Days { get; set; }
        public int? Weeks { get; set; }
        public int? Months { get; set; }
        public ICollection<Entry> Entries { get; set; }
        [InverseProperty(nameof(RegularTarget.TargetDuration))]
        public ICollection<RegularTarget> TargetRegularTargets { get; set; }
        [InverseProperty(nameof(RegularTarget.PerTimeframeDuration))]
        public ICollection<RegularTarget> PerTimeframeRegularTargets { get; set; }

        public string PrepareText()
        {
            if (Months != null) 
                return $"{Months} months";
            else if (Weeks != null) 
                return $"{Weeks} weeks";
            else if (Days != null) 
                return $"{Days}d";
            else if (Hours != null && Minutes != null)
                return String.Format("{0:00}:{1:00}", Hours, Minutes);
            else if (Hours != null && Minutes == null) 
                return String.Format("{0:00}:00", Hours);
            else if (Hours == null && Minutes != null) 
                return String.Format("00:{0:00}", Minutes);
            else 
                return "Empty";
        }
    }
}
