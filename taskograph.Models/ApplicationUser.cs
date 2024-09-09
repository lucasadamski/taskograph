using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using taskograph.Models.Tables;
using Task = taskograph.Models.Tables.Task;
using Group = taskograph.Models.Tables.Group;

namespace taskograph.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ICollection<Group> Groups { get; set; }
        public ICollection<Task> Tasks { get; set; }
        public ICollection<Setting> Settings { get; set; }

        //make migration, i connected the relations here 
    }
}
