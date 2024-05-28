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
        [MaxLength(30)]
        [Column(TypeName = "varchar(30)")]
        public string Name { get; set; }
        public int? ColorId { get; set; }
        public string AppUserId { get; set; }
        public DateTime Created { get; set; }
        public DateTime? LastUpdated { get; set; }
        public DateTime? Deleted { get; set; }


        public Color Color { get; set; }
        public virtual ApplicationUser AppUser { get; set; }
        public ICollection<Task> Tasks { get; set; }
    }
}
