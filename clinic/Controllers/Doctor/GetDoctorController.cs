using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WebApplication1.Data;

namespace clinic.Controllers.Doctor
{
    public class GetDoctorController :Controller
    {
        private readonly DataContext _dataContext;
        public GetDoctorController (DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        [HttpGet("getDoctors")]
        public async Task<IActionResult> GetDoctors()
        {
            var doctors = await _dataContext.Doctors
            .Include(c => c.Category)
            .Select(d => new
            {
                d.Id,
                d.FirstName,
                d.LastName,
                d.Email,
                d.Pid,
                d.Views,
                d.Image,
                d.Document,
                category= new
                {
                d.Category.Id,
                d.Category.Name,
                },
            })
            .ToListAsync();
            if (doctors.IsNullOrEmpty())
            {
                return NotFound();
            }


            return Ok(doctors);
        }
        [HttpGet("getDoctor/{id}")]
        public async Task<IActionResult> GetDoctor(int id)
        {
            var doctor = await _dataContext.Doctors.FindAsync(id);
            if (doctor == null)
            {
                return NotFound();
            }

            doctor.Views += 1;
            await _dataContext.SaveChangesAsync();
            var category = await _dataContext.Categories.FindAsync(doctor.CategoryId);
            if (category == null)
            {
                return NotFound("ექიმს კატეგორია არაქვს");
            }
            return Ok(new
            {
                doctor.Id,
                doctor.FirstName,
                doctor.LastName,
                doctor.Email,
                doctor.Pid,
                doctor.Views,
                doctor.Image,
                doctor.Document,
                category = new
                {
                    category.Id,
                    category.Name,
                },
            });
        }
        [HttpGet("getDoctors/category/{id}")]
        public async Task<IActionResult> GetDoctorsByCategory(int id)
        {
            var doctors = await _dataContext.Doctors.Where(d=> d.CategoryId == id).Include(c => c.Category)
            .Select(d => new
            {
                d.Id,
                d.FirstName,
                d.LastName,
                d.Email,
                d.Pid,
                d.Views,
                d.Image,
                d.Document,
                category = new
                {
                    d.Category.Id,
                    d.Category.Name,
                },
            })
            .ToListAsync();
            if (doctors.IsNullOrEmpty())
            {
                return NotFound();
            }

            return Ok(doctors);
        }

    }
}
