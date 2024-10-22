using taskograph.Models;
using taskograph.Web.Models.DTOs;

namespace taskograph.Web.Models.Graph
{
    public class Table
    {
        public List<Column> Columns { get; set; } = new List<Column>();
        public string Description { get; set; }
        public Duration Total { get; set; }

    }
}
