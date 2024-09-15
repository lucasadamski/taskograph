using Microsoft.Extensions.Logging;
using Color = taskograph.Models.Tables.Color;
using static taskograph.Helpers.Messages;
using taskograph.EF.DataAccess;
using taskograph.EF.Repositories.Infrastructure;

namespace taskograph.EF.Repositories
{
    public class ColorRepository : IColorRepository
    {
        private readonly TasksContext _db;
        private readonly ILogger<ColorRepository> _logger;

        public ColorRepository(TasksContext db, ILogger<ColorRepository> logger)
        {
            _db = db;
            _logger = logger;
        }


        public bool Add(string name)
        {
            Color color = new Color() { Name = name };
            bool result = true;
            try
            {
                _db.Colors.Add(color);
                _db.SaveChanges();
            }
            catch (Exception e)
            {
                _logger.LogError($"Exception: {e.Message} StackTrace: {e.StackTrace}");
                result = false;
            }
            return result;
        }

        public bool Delete(Color color)
        {
            bool result = true;
            try
            {
                _db.Colors.Remove(color);
                _db.SaveChanges();
            }
            catch (Exception e)
            {
                _logger.LogError($"Exception: {e.Message} StackTrace: {e.StackTrace}");
                result = false;
            }
            return result;
        }

        public bool Edit(Color color)
        {
            bool result = true;
            try
            {
                _db.Colors.Update(color);
                _db.SaveChanges();
            }
            catch (Exception e)
            {
                _logger.LogError($"Exception: {e.Message} StackTrace: {e.StackTrace}");
                result = false;
            }
            return result;
        }

        public Color Get(int id)
        {
            Color? result = new Color();
            try
            {
                result = _db.Colors
                    .Where(n => n.Id == id)
                    .FirstOrDefault();
            }
            catch (Exception e)
            {
                _logger.LogError($"Exception: {e.Message} StackTrace: {e.StackTrace}");
            }
            return result;
        }

        public IEnumerable<Color> GetAll()
        {
            List<Color> result = new List<Color>();
            try
            {
                result = _db.Colors.ToList();
            }
            catch (Exception e)
            {
                _logger.LogError($"Exception: {e.Message} StackTrace: {e.StackTrace}");
            }
            return result;
        }
    }
}
