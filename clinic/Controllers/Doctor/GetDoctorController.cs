﻿using Microsoft.AspNetCore.Mvc;
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

            return Ok(doctor);
        }
        [HttpGet("getDoctors/category/{id}")]
        public async Task<IActionResult> GetDoctorsByCategory(int id)
        {
            var doctors = await _dataContext.Doctors.Where(d=> d.CategoryId == id).ToListAsync();
            if (doctors.IsNullOrEmpty())
            {
                return NotFound();
            }

            return Ok(doctors);
        }
    }
}
