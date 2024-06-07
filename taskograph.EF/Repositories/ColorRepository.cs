using Microsoft.Extensions.Logging;
using Task = taskograph.Models.Tables.Task;
using Group = taskograph.Models.Tables.Group;
using Color = taskograph.Models.Tables.Color;
using static taskograph.Helpers.Messages;
using taskograph.EF.DataAccess;
using taskograph.EF.Repositories.Infrastructure;
using taskograph.Models.Tables;
using System.Drawing;
using Microsoft.EntityFrameworkCore;

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
            try
            {
                _db.Colors.Add(color);
                _db.SaveChanges();
            }
            catch (Exception e)
            {
                _logger.LogError($"Add {color.Name}: Message: {DATABASE_ERROR_CONNECTION} Exception: {e.Message}");
                return false;
            }
            return true;
        }

        public bool Delete(Color color)
        {
            try
            {
                _db.Colors.Remove(color);
                _db.SaveChanges();
            }
            catch (Exception e)
            {
                _logger.LogError($"Delete {color.Name}: Message: {DATABASE_ERROR_CONNECTION} Exception: {e.Message}");
                return false;
            }
            return true;
        }

        public bool Edit(Color color)
        {
            try
            {
                _db.Colors.Update(color);
                _db.SaveChanges();
            }
            catch (Exception e)
            {
                _logger.LogError($"Edit {color.Name}: Message: {DATABASE_ERROR_CONNECTION} Exception: {e.Message}");
                return false;
            }
            return true;
        }

        public Color Get(int id)
        {
            Color? result;
            try
            {
                result = _db.Colors
                    .Where(n => n.Id == id)
                    .FirstOrDefault();
            }
            catch (Exception e)
            {
                _logger.LogError($"Get: id {id} Message: {DATABASE_ERROR_CONNECTION} Exception: {e.Message}");
                return new Color();
            }
            if (result == null)
            {
                _logger.LogError($"Get: id {id} Message: {EMPTY_VARIABLE}");
                return new Color();
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
                _logger.LogError($"GetAll: Message: {DATABASE_ERROR_CONNECTION} Exception: {e.Message}");
                return new List<Color>();
            }
            if (result == null)
            {
                _logger.LogError($"GetAll: Message: {EMPTY_VARIABLE}");
                return new List<Color>();
            }
            return result;
        }
    }
}
