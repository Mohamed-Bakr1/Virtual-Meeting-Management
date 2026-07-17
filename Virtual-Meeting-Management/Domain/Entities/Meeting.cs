namespace Domain.Entities
{
    public class Meeting
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string HostId { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}