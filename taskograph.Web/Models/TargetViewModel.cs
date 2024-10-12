using Microsoft.AspNetCore.Mvc.Rendering;
using taskograph.Models.DTOs;
using taskograph.Models.Tables;

namespace taskograph.Web.Models
{
    public class TargetViewModel
    {
        public string Name { get; set; }
        public int TaskId { get; set; }
        public DateTime DateDue { get; set; } = new DateTime();
        public List<PreciseTarget> PreciseTargets { get; set; } = new List<PreciseTarget>();
        public List<RegularTargetDTO> RegularTargets { get; set; } = new List<RegularTargetDTO>();
        public List<SelectListItem> TasksSLI { get; set; }
    }
}
