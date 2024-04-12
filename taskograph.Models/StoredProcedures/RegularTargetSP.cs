using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace taskograph.Models.StoredProcedures
{
    public class RegularTargetSP
    {
        public int Id { get; set; }
        public string TaskName { get; set; }
        public long TargetDuration { get; set; }
        public long PerTimeframeDuration { get; set; }
        public DateTime Created { get; set; }
        public DateTime? LastUpdated { get; set; }
        public DateTime? Deleted { get; set; }

    }
}
