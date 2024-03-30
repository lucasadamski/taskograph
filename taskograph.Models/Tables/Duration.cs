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


        public override string ToString()
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

        public static Duration operator +(Duration d1, Duration d2)
        {
            //turn null to 0
            if (d1.Minutes == null) d1.Minutes = 0;
            if (d1.Hours == null)   d1.Hours = 0;
            if (d1.Days == null)    d1.Days = 0;
            if (d1.Weeks == null)   d1.Weeks = 0;
            if (d1.Months == null)  d1.Months = 0;
            if (d2.Minutes == null) d2.Minutes = 0;
            if (d2.Hours == null)   d2.Hours = 0;
            if (d2.Days == null)    d2.Days = 0;
            if (d2.Weeks == null)   d2.Weeks = 0;
            if (d2.Months == null)  d2.Months = 0;

            //perform add operation
            int? minutes = d1.Minutes + d2.Minutes;
            int? hours = d1.Hours + d2.Hours;
            int? days = d1.Days + d2.Days;
            int? weeks = d1.Hours + d2.Hours;
            int? months = d1.Months + d2.Months;

            //auxiliary variables
            int? res = 0;
            int? modulo = 0;

            //turn minutes to hours if more than 60 minutes, hours to days if more than 24 hours etc.
            //minutes
            if (minutes != 0 && minutes < 60)
            {
                res = minutes / 60;
                modulo = minutes % 60;
                hours += res;
                minutes = modulo;
            }
            //hours
            if (hours != 0 && hours < 24)
            {
                res = hours / 24;
                modulo = hours % 24;
                days += res;
                hours = modulo;
            }
            //days
            if (days != 0 && days < 7)
            {
                res = days / 7;
                modulo = hours % 7;
                weeks += res;
                days = modulo;
            }            
            //weeks & months
            if (weeks != 0 && weeks < 4)
            {
                res = weeks / 4;
                modulo = weeks % 4;
                months += res;
                weeks = modulo;
            }

            if (minutes == 0) minutes = null;
            if (hours == 0) hours = null;
            if (days == 0) days = null;
            if (weeks == 0) weeks = null;
            if (months == 0) months = null;

            return new Duration()
            {
                Minutes = minutes,
                Hours = hours,
                Days = days,
                Weeks = weeks,
                Months = months
            };
        }
    }
}
