using taskograph.Web.Models.DTOs;

namespace taskograph.Web.Models
{
    public class GraphViewModel
    {
        public List<TextGraphOneCellDTO> TextGraphCell { get; set; } = new List<TextGraphOneCellDTO>();
    }
}
