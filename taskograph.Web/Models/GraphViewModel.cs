using Microsoft.AspNetCore.Mvc.Rendering;
using taskograph.Web.Models.DTOs;
using taskograph.Web.Models.Enums;
using taskograph.Web.Models.Graph;

namespace taskograph.Web.Models
{
    public class GraphViewModel
    {
        public List<Table> Tables { get; set; } = new List<Table>();
        public string GraphDescription { get; set; }
        public int HowManyCalendarUnits { get; set; }
        public GraphTimeUnit GraphTimeUnit{ get; set; }
        public List<SelectListItem> GraphUnitsSLI { get; set; }

        public DateTime Start { get; set; }
        public DateTime End { get; set; }

    }
}
