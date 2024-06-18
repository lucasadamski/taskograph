using Moq;
using Xunit;
using Autofac.Extras.Moq;
using taskograph.EF.Repositories.Infrastructure;
using Task = taskograph.Models.Tables.Task;
using taskograph.Web.Controllers;


namespace taskograph.Tests
{
    public class TaskRepositoryTests
    {
        DateTime _dayToday = DateTime.Today;

        [Fact]
        public void GetAll_ValidCall()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<ITaskRepository>()
                    .Setup(n => n.GetAll("test"))
                    .Returns(GetSampleTasks());

                var repository = mock.Create<ITaskRepository>();
                var expected = GetSampleTasks().ToList();

                var actual = repository.GetAll("test").ToList();

                Assert.True(actual != null);
                Assert.Equal(expected.Count(), actual.Count());

                for (int i = 0; i < expected.Count(); i++)
                {
                    Assert.Equal(expected[i].Name, actual[i].Name);
                }
            }
        }

        [Fact]
        public void AddTask_ValidCall()
        {
            using (var mock = AutoMock.GetLoose())
            {
                var task = GetSampleTasks().ToList().ElementAt(0);

                mock.Mock<ITaskRepository>()
                    .Setup(n => n.Add(task));

                var repository = mock.Create<ITaskRepository>();

                repository.Add(task);

                mock.Mock<ITaskRepository>()
                    .Verify(n => n.Add(task), Times.Exactly(1));
            }
        }

        private IEnumerable<Task> GetSampleTasks()
        {
            return new List<Task>()
            {
                new Task { Id = 1, Name = "Running", GroupId = 4,   Created = _dayToday, ApplicationUserId = "test" },
                  new Task { Id = 2, Name = "Reading", GroupId = 2,   Created = _dayToday , ApplicationUserId = "test"},
                  new Task { Id = 3, Name = "Cooking", GroupId = 1,   Created = _dayToday , ApplicationUserId = "test"},
                  new Task { Id = 4, Name = "Dancing", GroupId = 7,   Created = _dayToday , ApplicationUserId = "test"}
            };
        }
    }
}