using System.ComponentModel.DataAnnotations;

namespace Model.Entities
{
    public class Faq 
    {
        [Key]
        public Guid Id { get; set; }


        [Required]
        public string Question { get; set; }

        [Required]

        public string Answer { get; set; }

        public DateTime? CreateDate { get; set; } = DateTime.Now;

        public DateTime? Updatetime { get; set; }=DateTime.Now;

        public bool IsRemoved { get; set; } = false;
    }
}
