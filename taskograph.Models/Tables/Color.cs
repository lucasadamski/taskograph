using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace taskograph.Models.Tables
{
    public class Color
    {
        public int Id { get; set; }
        [MaxLength(15)]
        [Column(TypeName = "varchar(15)")]
        public string Name { get; set; }

        public ICollection<Group> Groups { get; set; }

    }
}
