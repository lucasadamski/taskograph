namespace taskograph.Models.Tables
{
    public class AppUser
    {
        public int Id { get; set; }
        public string UserId { get; set; }

        public IEnumerable<Task> Tasks { get; set; }
        public IEnumerable<Group> Groups { get; set; }
        public IEnumerable<Quote> Quotes { get; set; }
        public IEnumerable<Setting> Settings { get; set; }
    }
}
