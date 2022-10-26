namespace Infrastructure.Dtos
{
    public class FaqDto
    {
        public Guid Id { get; set; }

        public string Question { get; set; }

        public string Answer { get; set; }

        public DateTime? CreateDate { get; set; } = DateTime.Now;

        public DateTime? UpdateTime { get; set; } = DateTime.Now;
    }
}
