using Microsoft.CodeAnalysis.Options;
using taskograph.Models.Tables;

namespace taskograph.Web.Models.DTOs
{
    //Eg in one week graph mode, it will be 7 individual days (cells) and one summary cell for the whole week.
    //Every item is TextGraphOneCellDTO
    public class TextGraphOneCellDTO
    {
        public string Description { get; set; }
        public List<TaskDTO> Tasks { get; set; } = new List<TaskDTO>();
        public Duration TotalDuration { get; set; }
    }
}
