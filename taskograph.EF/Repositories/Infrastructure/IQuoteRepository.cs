using taskograph.Models.Tables;

namespace taskograph.EF.Repositories.Infrastructure
{
    public interface IQuoteRepository
    {
        public bool Add(Quote regularTarget);
        public bool Edit(Quote regularTarget);
        public bool Delete(Quote regularTarget);
        public Quote Get(int id);
        public IEnumerable<Quote> GetAll(string userId);
    }
}
