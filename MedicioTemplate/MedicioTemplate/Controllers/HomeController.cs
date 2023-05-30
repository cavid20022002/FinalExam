using MedicioTemplate.DAL;
using MedicioTemplate.Models;
using MedicioTemplate.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MedicioTemplate.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            List<Testimonial> testimonials= await _context.Testimonials.ToListAsync();
            List<CustomService> customServices = await _context.CustomServices.ToListAsync();
            List<Doctor>doctors=await _context.Doctors.ToListAsync();

            HomeVM homeVM = new HomeVM()
            {
                Testimonials = testimonials,
                CustomServices = customServices,
                Doctors=doctors
            };


            return View(homeVM);
        }
    }
}
