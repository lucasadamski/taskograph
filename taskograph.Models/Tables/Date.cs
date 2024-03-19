using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace taskograph.Models.Tables
{
    public class Date
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime? LastUpdated { get; set; }
        public DateTime? Deleted { get; set; }

        public ICollection<Entry> Entries { get; set; }
        public ICollection<Group> Groups { get; set; }
        public ICollection<PreciseTarget> PreciseTargets { get; set; }
        public ICollection<RegularTarget> RegularTargets { get; set; }
        public ICollection<Task> Tasks { get; set; }
    }
}
