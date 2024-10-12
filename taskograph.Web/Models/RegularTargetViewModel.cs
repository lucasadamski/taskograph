using Microsoft.AspNetCore.Mvc.Rendering;

namespace taskograph.Web.Models
{
    public class RegularTargetViewModel
    {
        public long TimeDedicated { get; set; }
        public long TimeInterval { get; set; }
        public int TaskId { get; set; }
        public List<SelectListItem> TasksSLI { get; set; }
    }
}
