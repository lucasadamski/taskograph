using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using taskograph.Models.Tables;

namespace taskograph.EF.Repositories.Infrastructure
{
    public interface IColorRepository
    {
        public bool Add(string name);
        public bool Edit(Color color);
        public bool Delete(Color color);
        public Color Get(int id);
        public IEnumerable<Color> GetAll();
    }
}
