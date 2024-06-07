using System.Text.RegularExpressions;
using taskograph.Web.Models.DTOs;
using Task = taskograph.Models.Tables.Task;
using Group = taskograph.Models.Tables.Group;

namespace taskograph.EF.Repositories.Infrastructure
{
    public interface IGroupRepository
    {
        bool Add(Group group);
        bool Edit(Group group);
        bool Delete(Group group);
        public IEnumerable<Group> GetAll(string userId);
        public Group Get(int id);

    }
}
