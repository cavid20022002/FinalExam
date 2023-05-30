using MedicioTemplate.Models;

namespace MedicioTemplate.ViewModels
{
    public class HomeVM
    {
        public List<CustomService> CustomServices { get; set; }
        public List<Testimonial> Testimonials { get; set; }
        public List<Doctor> Doctors { get; set; }

    }
}
