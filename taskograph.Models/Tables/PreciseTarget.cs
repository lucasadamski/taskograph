using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace taskograph.Models.Tables
{
    public class PreciseTarget
    {
        public int Id { get; set; }
        [MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string Name { get; set; }
        public int TaskId { get; set; }
        public DateTime DateDue { get; set; }
        public int DateId { get; set; }

        public Task Task { get; set; }
        public Date Date { get; set; }
    }
}
