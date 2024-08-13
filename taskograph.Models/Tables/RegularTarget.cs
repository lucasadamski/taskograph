using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace taskograph.Models.Tables
{
    //Regular Target is basically a task that we want to perform on a regular basis. For example
    //we want to perform Task "Running" for 30 minutes every week. 30 minutes is TimeDedicatedToPerformTarget 
    //and week is RegularTimeIntervalToAchieveTarget. Both members relate to the same table Duration, but the 
    //purpose of that time duration is different.
    public class RegularTarget
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public long TimeDedicatedToPerformTarget { get; set; }  //eg 30 minutes         TimeDedicatedToPerformTarget
        public long RegularTimeIntervalToAchieveTarget { get; set; } //per every 1 week RegularTimeIntervalToAchieveTarget
        public DateTime Created { get; set; }
        public DateTime? LastUpdated { get; set; }
        public DateTime? Deleted { get; set; }

        public Task Task { get; set; }
    }
}
