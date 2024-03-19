using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace taskograph.Models.Tables
{
    public class Group
    {
        public int Id { get; set; }
        [MaxLength(20)]
        [Column(TypeName = "varchar(20)")]
        public string Name { get; set; }
        public int? ColorId { get; set; }
        public int DateId { get; set; }

        public Color Color { get; set; }
        public Date Date { get; set; }

        public ICollection<Task> Tasks { get; set; }
    }
}
