using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
        public DateTime Created { get; set; }
        public DateTime? LastUpdated { get; set; }
        public DateTime? Deleted { get; set; }


        public Task Task { get; set; }
        [ForeignKey("TargetDurationId")]
        public Duration TargetDuration { get; set; }
        [ForeignKey("PerTimeframeDurationId")]
        public Duration PerTimeframeDuration { get; set; }

    }
}
