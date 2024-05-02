namespace taskograph.Models.Tables
{
    public class User
    {
        public int Id { get; set; }
        public string UserId { get; set; }

        public Task Task { get; set; }
        public Group Group { get; set; }
        public Quote Quote { get; set; }
        public Setting Setting { get; set; }
    }
}
