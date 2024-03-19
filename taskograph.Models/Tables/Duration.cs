using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace taskograph.Models.Tables
{
    public class Duration
    {
        public int Id { get; set; }
        public DateTime End { get; set; }
        //@Duration =  End - (2000-01-01 00:00:00)
        //eg: 15 min = (2000-01-01 00:15:00) - (2000-01-01 00:00:00)
        public ICollection<Entry> Entries { get; set; }
        public ICollection<RegularTarget> RegularTargets { get; set; }
    }
}
