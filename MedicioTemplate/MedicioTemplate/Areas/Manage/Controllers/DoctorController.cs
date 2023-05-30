using MedicioTemplate.DAL;
using MedicioTemplate.Models;
using MedicioTemplate.Utilities.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace MedicioTemplate.Areas.Manage.Controllers
{
    public class DoctorController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly AppDbContext _context;

        public DoctorController(IWebHostEnvironment env,AppDbContext context)
        {
            _env = env;
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public  async Task<IActionResult> Create(Doctor doctor)
        {
            if (doctor.Photo == null) 
            {
                ModelState.AddModelError("Photo", "Photo bos gonderile bilmez");
                return View();
            }

            if (!doctor.Photo.CheckFileType("image/"))
            {
                ModelState.AddModelError("Photo", "Image type i uygun gelmir");
                return View();

            }
            if (!doctor.Photo.CheckFileSize(200))
            {
                ModelState.AddModelError("Photo", "File olcusu 200 kb dan az ola bilmez");
                return View();

            }
            doctor.Image = await doctor.Photo.CreateFileAsync(_env.WebRootPath, "assets/img");
            await _context.AddAsync(doctor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null || id < 1) return BadRequest();

            Doctor existed = await _context.Doctors.FirstOrDefaultAsync(s => s.Id == id);

            if (existed == null) return NotFound();

            return View(existed);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null || id < 1) return BadRequest();

            Doctor existed = await _context.Doctors.FirstOrDefaultAsync(s => s.Id == id);

            if (existed == null) return NotFound();

            return View(existed);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id < 1) return BadRequest();

            Doctor doctor = await _context.Doctors.FirstOrDefaultAsync(s => s.Id == id);

            if (doctor == null) return NotFound();

            doctor.Image.DeleteFile(_env.WebRootPath, "assets/img/doctors");

            _context.Doctors.Remove(doctor);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));


        }
    }
}
