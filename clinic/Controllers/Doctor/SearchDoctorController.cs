using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WebApplication1.Data;

namespace clinic.Controllers.Doctor
{
    public class SearchDoctorController :Controller
    {
        private readonly DataContext _dataContext;
        public SearchDoctorController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        [HttpPost("searchDoctor")]
        public async Task<IActionResult> SearchDoctor(string byName = null, string byCategory = null)
        {
            if (byName != null && byCategory == null)
            {
                var doctorsByName = await _dataContext.Doctors.Where(
                    d => EF.Functions.FreeText(d.FirstName, byName) || EF.Functions.FreeText(d.LastName, byName) || EF.Functions.FreeText(d.Email , byName)
                    ).ToListAsync();
                if (doctorsByName.IsNullOrEmpty()) return NotFound("ექიმი ვერ მოიძებნა.");
                return Ok(doctorsByName);

            }else if (byCategory != null && byName == null)
            {
                var doctorsByCategory = await _dataContext.Doctors.Where(
                    d => EF.Functions.FreeText(d.Category.Name, byCategory)
                    ).ToListAsync();
                if (doctorsByCategory.IsNullOrEmpty()) return NotFound("ექიმი ვერ მოიძებნა.");
                return Ok(doctorsByCategory);

            }else if(byName != null && byCategory != null)
            {
                var doctorsByBoth = await _dataContext.Doctors.Where(
                    d => EF.Functions.FreeText(d.FirstName, byName) || EF.Functions.FreeText(d.LastName, byName) || EF.Functions.FreeText(d.Email, byName) && EF.Functions.FreeText(d.Category.Name, byCategory)
                    ).ToListAsync();
                 if(doctorsByBoth.IsNullOrEmpty()) return NotFound("ექიმი ვერ მოიძებნა.");
                return Ok(doctorsByBoth);

            }
            return BadRequest("ერთ პარამეტრი მაინც უნდა იყოს მითითებული.");
        }
    }
}
