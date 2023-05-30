using System.ComponentModel.DataAnnotations.Schema;

namespace MedicioTemplate.Models
{
    public class Doctor
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string Fullname { get; set; }
        public string Profession { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }
    }
}
