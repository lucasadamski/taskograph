using Microsoft.CodeAnalysis.Options;
using taskograph.Models;
using taskograph.Models.Tables;

namespace taskograph.Web.Models.DTOs
{
    //Eg in one week graph mode, it will be 7 individual days (cells) and one summary cell for the whole week.
    //Every item is TextGraphOneCellDTO
    public class Column
    {
        public string Title { get; set; }   //eg. Monday | Week 21 | March | Year 2022
        public List<TaskDTO> Tasks { get; set; } = new List<TaskDTO>();
        public Duration DurationSummary { get; set; }
    }
}
