namespace taskograph.Web.Models.DTOs
{
    public class TaskDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Group { get; set; }
        public string Color { get; set; }
        public string TotalDurationToday { get; set; }
    }
}
