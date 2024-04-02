using taskograph.Models.Tables;

namespace taskograph.Web.Models
{
    public class TargetViewModel
    {
        public List<PreciseTarget> PreciseTargets { get; set; } = new List<PreciseTarget>();
        public List<RegularTarget> RegularTargets { get; set; } = new List<RegularTarget>();
    }
}
