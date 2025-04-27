namespace WebApplication1.Models
{
    public class Schedule
    {
        public int Id { get; set; }
        public string Activity { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Duration { get; set; }
    }
}
