using Microsoft.EntityFrameworkCore;
using taskograph.EF.DataAccess;
using Task = taskograph.Models.Tables.Task;

namespace taskograph.DBTests
{
    public class TaskRepositoryTests
    {
       private async Task<TasksContext> GetDbContext()
        {
            var options = new DbContextOptionsBuilder<TasksContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) //in memory nuget necessary
                .Options;
            //creating DBcontext from mock
            var dbContext = new TasksContext(options);
            dbContext.Database.EnsureCreated();

            //filling with seed data
            if (await dbContext.Tasks.CountAsync() < 0)
            {
                dbContext.Tasks.Add(new Task { Id = 1, Name = "Running", GroupId = 4, Created = DateTime.Now, ApplicationUserId = "none" });
                dbContext.Tasks.Add(new Task { Id = 2, Name = "Cooking", GroupId = 4, Created = DateTime.Now, ApplicationUserId = "none" });
                dbContext.Tasks.Add(new Task { Id = 3, Name = "Reading", GroupId = 4, Created = DateTime.Now, ApplicationUserId = "none" });
                await dbContext.SaveChangesAsync();
            }
            return dbContext;
        }
    }
}