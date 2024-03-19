using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace taskograph.Models.Tables
{
    public class Quote
    {
        public int Id { get; set; }
        [MaxLength(256)]
        [Column(TypeName = "256")]
        public string Name { get; set; }
    }
}
