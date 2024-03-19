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
        public int DurationId { get; set; }
        public int DateId { get; set; }

        public Task Task{ get; set; }
        public Duration Duration { get; set; }
        public Date Date { get; set; }
    }
}
