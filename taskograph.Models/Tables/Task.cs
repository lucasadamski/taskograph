﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace taskograph.Models.Tables
{
    public class Task
    {
        public int Id { get; set; }
        [MaxLength(30)]
        [Column(TypeName = "varchar(30)")]
        public string Name { get; set; }
        public int? GroupId { get; set; }
        public DateTime Created { get; set; }
        public DateTime? LastUpdated { get; set; }
        public DateTime? Deleted { get; set; }
        public string ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
        public Group Group { get; set; }
        public ICollection<PreciseTarget> PreciseTargets { get; set; }
        public ICollection<RegularTarget> RegularTargets { get; set; }

    }
}
