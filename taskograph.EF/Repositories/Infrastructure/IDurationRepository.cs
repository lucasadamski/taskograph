using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using taskograph.Models.Tables;

namespace taskograph.EF.Repositories.Infrastructure
{
    public interface IDurationRepository
    {
        public IEnumerable<Duration> GetAll();
        public IEnumerable<Duration> GetFirst(int amount);
        public Duration Get(int id);
        public Duration Add(Duration duration);
    }
}
