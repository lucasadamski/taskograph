using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace taskograph.Models.Tables
{
    public class RegularTarget
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public int TargetDurationId { get; set; }  //eg 15 min
        public int PerTimeframeDurationId { get; set; } //per every 1 week
        public int DateId { get; set; }

        //add relations

    }
}
