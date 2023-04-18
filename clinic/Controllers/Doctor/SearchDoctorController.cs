using clinic.Schemas;
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
        [HttpPost("search")]
        public async Task<IActionResult> SearchDoctor([FromBody] SearchRequest request)
        {
            if (request.byName != null && request.byCategory == null)
            {
                var doctorsByName = await _dataContext.Doctors.Where(
                    d => EF.Functions.Like(d.FirstName  + " "+  d.LastName + " " + d.Email, $"%{request.byName}%")
                    ).Include(d=> d.Category).Select(d => new {
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
                    }).ToListAsync();
                if (doctorsByName.IsNullOrEmpty()) return NotFound("ექიმი ვერ მოიძებნა.");
                return Ok(doctorsByName);

            }else if (request.byCategory != null && request.byName == null)
            {
                var doctorsByCategory = await _dataContext.Doctors.Where(
                    d => EF.Functions.Like(d.Category.Name, $"%{request.byCategory}%")
                    ).Include(d => d.Category).Select(d => new {
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
                    }).ToListAsync(); ;
                if (doctorsByCategory.IsNullOrEmpty()) return NotFound("ექიმი ვერ მოიძებნა.");
                return Ok(doctorsByCategory);

            }else if(request.byName != null && request.byCategory != null)
            {
                var doctorsByBoth = await _dataContext.Doctors.Where(
                    d => EF.Functions.Like(d.FirstName + " " + d.LastName + " "+ d.Email, $"%{request.byName}%") && EF.Functions.Like(d.Category.Name, $"%{request.byCategory}%")
                    ).Include(d => d.Category).Select(d => new {
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
                    }).ToListAsync(); ;
                 if(doctorsByBoth.IsNullOrEmpty()) return NotFound("ექიმი ვერ მოიძებნა.");
                return Ok(doctorsByBoth);

            }
            return BadRequest("ერთ პარამეტრი მაინც უნდა იყოს მითითებული.");
        }
    }
}
