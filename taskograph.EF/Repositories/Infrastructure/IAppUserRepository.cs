using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using taskograph.Models.Tables;
using taskograph.Web.Models.DTOs;
using Task = taskograph.Models.Tables.Task;

namespace taskograph.EF.Repositories.Infrastructure
{
    public interface IAppUserRepository
    {
        bool Add(string userId);
        public AppUser Get(int id);
        public int GetId(string userId);
    }
}
