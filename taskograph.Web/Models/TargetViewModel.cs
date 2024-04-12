using taskograph.Models.DTOs;
using taskograph.Models.Tables;

namespace taskograph.Web.Models
{
    public class TargetViewModel
    {
        public List<PreciseTarget> PreciseTargets { get; set; } = new List<PreciseTarget>();
        public List<RegularTargetDTO> RegularTargets { get; set; } = new List<RegularTargetDTO>();
    }
}
