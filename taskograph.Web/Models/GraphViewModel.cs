using taskograph.Web.Models.DTOs;
using taskograph.Web.Models.Graph;

namespace taskograph.Web.Models
{
    public class GraphViewModel
    {
        public List<Table> Tables { get; set; } = new List<Table>();
        public string GraphDescription { get; set; }
        public int HowManyCalendarUnits { get; set; }
        public int CalendarUnit { get; set; }

    }
}
