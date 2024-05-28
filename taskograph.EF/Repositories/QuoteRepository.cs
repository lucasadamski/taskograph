using AutoMapper;
using Microsoft.Extensions.Logging;
using taskograph.EF.DataAccess;
using taskograph.EF.Repositories.Infrastructure;
using taskograph.Models.Tables;
using Task = taskograph.Models.Tables.Task;
using static taskograph.Helpers.Messages;
using Microsoft.EntityFrameworkCore;

namespace taskograph.EF.Repositories
{
    public class QuoteRepository : IQuoteRepository
    {
        private readonly TasksContext _db;
        private readonly ILogger<QuoteRepository> _logger;
        private readonly IMapper _mapper;

        public QuoteRepository(TasksContext db, ILogger<QuoteRepository> logger, IMapper mapper)
        {
            _db = db;
            _logger = logger;
            _mapper = mapper;
        }

        public bool Add(Quote quote)
        {
            try
            {
                _db.Quotes.Add(quote);
                _db.SaveChanges();
                _logger.LogDebug($"QuoteRepository: Add quoteId: {quote.Id}: Message: {DATABASE_OK}");
            }
            catch (Exception e)
            {
                _logger.LogError($"QuoteRepository: Add quoteId: {quote.Id}: Message: {DATABASE_ERROR_CONNECTION} Exception: {e.Message}");
                return false;
            }
            return true;
        }

        public bool Delete(Quote quote)
        {
            try
            { 
                _db.Remove(quote);
                _db.SaveChanges();
                _logger.LogDebug($"QuoteRepository: Delete quoteId: {quote.Id}: Message: {DATABASE_OK}");
            }
            catch (Exception e)
            {
                _logger.LogError($"QuoteRepository: Delete quoteId: {quote.Id}: Message: {DATABASE_ERROR_CONNECTION} Exception: {e.Message}");
                return false;
            }
            return true;
        }

        public bool Edit(Quote quote)
        {
            try
            {
                _db.Quotes.Update(quote);
                _db.SaveChanges();
                _logger.LogDebug($"QuoteRepository: Edit quoteId: {quote.Id}: Message: {DATABASE_OK}");
            }
            catch (Exception e)
            {
                _logger.LogError($"QuoteRepository: Edit quoteId: {quote.Id}: Message: {DATABASE_ERROR_CONNECTION} Exception: {e.Message}");
                return false;
            }
            return true;
        }

        public Quote Get(int id)
        {
            Quote? result;
            try
            {
                result = _db.Quotes
                    .Where(n => n.Id == id)                    
                    .FirstOrDefault();
            }
            catch (Exception e)
            {
                _logger.LogError($"QuoteRepository: Get: id {id} Message: {DATABASE_ERROR_CONNECTION} Exception: {e.Message}");
                return new Quote();
            }
            if (result == null)
            {
                _logger.LogError($"QuoteRepository: Get: id {id} Message: {EMPTY_VARIABLE}");
                return new Quote();
            }
            _logger.LogDebug($"QuoteRepository: Get: id {id} Message: {DATABASE_OK}");
            return result;
        }

        public IEnumerable<Quote> GetAll(string userId)
        {
            List<Quote> result;
            try
            {
                result = _db.Quotes
                    .Include(n => n.AppUser)
                    .Where(n => n.AppUserId == userId).ToList();
            }
            catch (Exception e)
            {
                _logger.LogError($"QuoteRepository: GetAll from Message: {DATABASE_ERROR_CONNECTION} Exception: {e.Message}");
                return new List<Quote>();
            }
            _logger.LogDebug($"QuoteRepository: Get GetAll from Message: {DATABASE_OK}");
            return result;
        }
    }
}
