using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace taskograph.Models.Tables
{
    public class Entry
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public long Duration { get; set; }
        public DateTime Created { get; set; }
        public DateTime? LastUpdated { get; set; }
        public DateTime? Deleted { get; set; }

        public Task Task{ get; set; }
    }
}
