namespace WebApplication1.Models
{
    public class Goal
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime TargetDate { get; set; }
    }
}
