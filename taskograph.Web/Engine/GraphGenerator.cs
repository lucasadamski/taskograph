using taskograph.Models.Tables;
using taskograph.Models;
using taskograph.Web.Models.DTOs;
using taskograph.Web.Models.Enums;
using Task = taskograph.Models.Tables.Task;
using static taskograph.Helpers.Messages;
using taskograph.EF.Repositories.Infrastructure;
using taskograph.Web.Models.Graph;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace taskograph.Web.Engine
{
    public class GraphGenerator
    {
        private ITaskRepository _taskRepository;
        private IEntryRepository _entryRepository;

        public GraphGenerator(ITaskRepository taskRepository, IEntryRepository entryRepository)
        {
            _taskRepository = taskRepository;
            _entryRepository = entryRepository;
        }

        // howManyCalendarUnits - eg 2, 4, 3
        // calendarUnit - eg. week, month, year
        // => 2 weeks, 4 years etc.
        public List<Table> GenerateTables(GraphTimeUnit graphTimeUnit, string _userId, DateTime from, DateTime to)
        {
            List<Table> result = new List<Table>();

            List<Task> tasks = new List<Task>();
            tasks = _taskRepository.GetAll(_userId).ToList();
            if (tasks == null)
            {
                return result;
            }

            /*temp = new Column()
            {
                Title = $"{day.AddDays(i + 1).DayOfWeek} {day.AddDays(i + 1).Date.ToString("dd-MM-yy")}",
                Tasks = ConvertTasksToDTO(tasks, day.AddDays(i + 1)),
                DurationSummary = new Duration(GetTotalDurationFromTasks(ConvertTasksToDTO(tasks, day.AddDays(i + 1))))
            };
            graphVM.OneWeekTable.Add(temp);*/

            Column column;
            Table table = new Table();

            if (graphTimeUnit == GraphTimeUnit.Week)
            {
                // week
                // check beggining of a week
                // check end of the week
                // 

                //find begging of a week (Monday)
                while (from.DayOfWeek != DayOfWeek.Monday)
                {
                    from = from.AddDays(-1);
                }
                //find end of the week (Sunday)
                while (to.DayOfWeek != DayOfWeek.Sunday)
                {
                    to = to.AddDays(1);
                }

                // generate tables
                do
                {
                    column = new Column()
                    {
                        Title = $"{from.DayOfWeek} {from.Date.ToString("dd-MM-yy")}",
                        Tasks = ConvertTasksToDTO(tasks, from),
                        DurationSummary = new Duration(GetTotalDurationFromTasks(ConvertTasksToDTO(tasks, from)))
                    };
                    table.Columns.Add(column);
                    table.Total += column.DurationSummary;
                    if (from.DayOfWeek == DayOfWeek.Sunday)
                    {
                        result.Add(table);
                        table = new Table();
                    }
                    if (from.DayOfWeek == DayOfWeek.Monday)
                    {
                        //determine Week number
                        table.Description = "Week " + System.Globalization.ISOWeek.GetWeekOfYear(from).ToString();
                    }
                    from = from.AddDays(1);
                } while (from.Date <= to.Date);

            }
            if (graphTimeUnit == GraphTimeUnit.Month)
            {
                // month
                // determine start and end of month
                // for each week get total week 
                // get total month
                // 1 month = 1 table 

                table.Description = from.ToString("MMMM") + " " + from.ToString("yyyy");

                // start of the month
                int month = from.Month;
                int year = from.Year;
                from = new DateTime(year, month, 1);

                // end of the month
                month = to.Month;
                year = to.Year;
                to = new DateTime(year, month, 1);
                to = to.AddMonths(1);
                to = to.AddDays(-1);

                //find begging of a week (Monday)
                while (from.DayOfWeek != DayOfWeek.Monday)
                {
                    from = from.AddDays(-1);
                }

                //find end of the week (Sunday)
                while (to.DayOfWeek != DayOfWeek.Sunday)
                {
                    to = to.AddDays(1);
                }

                List<Entry> entries = _entryRepository.Get(_userId, from, to).ToList();

                int currentMonth = from.Month;
                do
                {
                    // ask DAL for entries for current week that have any duration
                    List<Entry> entriesForCurrentWeek = entries.Where(n => n.Duration != 0 && n.Created >= from && n.Created <= from.AddDays(7)).ToList();
                    column = new Column()
                    {
                        Title = "Week " + System.Globalization.ISOWeek.GetWeekOfYear(from).ToString(),
                        Tasks = new List<TaskDTO>()
                    };
                    column.DurationSummary = new Duration(entriesForCurrentWeek.Select(n => n.Duration).Sum());

                    // calculate individual tasks, only those who has entires

                    foreach (var entry in entriesForCurrentWeek)
                    {
                        column.Tasks.Add(new TaskDTO()
                        {
                            Name = entry.Task.Name,
                            Duration = new Duration(entry.Duration)
                        });

                    }


                    table.Columns.Add(column);
                    from = from.AddDays(7);
                } while (from.Date <= to.Date);

                table.Total = new Duration(12345);
                result.Add(table);
            }

            return result;

        }

        public string GenerateGraphDescription(DateTime from, DateTime to, GraphTimeUnit graphTimeUnit)
        {
            //option = weekly, monthly, yearly
            StringBuilder result = new StringBuilder("Monthly graph for: " + from.ToString("MMMM") + " " + from.ToString("yyyy"));
            if (!(from.Month == to.Month && from.Year == to.Year))
            {
                for (int i = 1; from.AddMonths(i) <= to; i++)
                {
                    result = result.Append(", " + from.AddMonths(i).ToString("MMMM") + " " + from.AddMonths(i).ToString("yyyy"));
                }
            }

            return result.ToString();
        }


        private List<TaskDTO> ConvertTasksToDTO(List<Task> input, DateTime date)
        {
            return input.Select(n => new TaskDTO()
            {
                Id = n.Id,
                Name = n.Name,
                Group = n.Group?.Name ?? NULL_VALUE,
                Duration = new Duration(_entryRepository.GetTotalDurationForTask(n.Id, date))
            })
                .ToList();
        }

        private long GetTotalDurationFromTasks(List<TaskDTO> input)
        {
            if (input.IsNullOrEmpty() == true)
            {
                return 0;
            }
            else
            {
                List<long> durations = input.Select(n => n.Duration.Minutes).ToList();
                return durations.Aggregate((a, b) => a + b);
            }

        }

    }
}
