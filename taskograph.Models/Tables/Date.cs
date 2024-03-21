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

        public Entry Entry { get; set; }
        public Task Tasks { get; set; }
        public Group Group { get; set; }
        public PreciseTarget PreciseTarges { get; set; }
        public RegularTarget RegularTarget { get; set; }
    }
}
