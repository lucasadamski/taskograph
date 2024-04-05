using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace taskograph.Models.Tables
{
    public class Setting
    {
        public int Id { get; set; }
        [MaxLength(30)]
        [Column(TypeName = "varchar(30)")]
        public string Name { get; set; }
        [MaxLength(256)]
        [Column(TypeName = "varchar(256)")]
        public string Value { get; set; }
        public string UserId { get; set; }
    }
}
