using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using taskograph.Models.Tables;

namespace taskograph.EF.Repositories.Infrastructure
{
    public interface IPreciseTargetRepository
    {
        public bool Add(PreciseTarget preciseTarget);
        public bool Edit(PreciseTarget preciseTarget);
        public bool Delete(PreciseTarget preciseTarget);
        public PreciseTarget Get(int id);
        public IEnumerable<PreciseTarget> Get(string userId, DateTime from, DateTime to);
        public IEnumerable<PreciseTarget> GetAll(string userId);
    }
}
