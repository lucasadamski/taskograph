using taskograph.Models.DTOs;
using taskograph.Models.Tables;

namespace taskograph.EF.Repositories.Infrastructure
{
    public interface IRegularTargetRepository
    {
        public bool Add(RegularTarget regularTarget);
        public bool Edit(RegularTarget regularTarget);
        public bool Delete(RegularTarget regularTarget);
        public RegularTarget Get(int id);
        public IEnumerable<RegularTargetDTO> Get(string userId, DateTime? from = null, DateTime? to = null);
    }
}
