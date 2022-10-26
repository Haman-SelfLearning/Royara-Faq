namespace Infrastructure.Dtos
{
    public class AddFaqDto
    {
        public Guid Id { get; set; }

        public string Question { get; set; }

        public string Answer { get; set; }
    }
}
