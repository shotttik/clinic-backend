using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using Microsoft.IdentityModel.Tokens;
using clinic.Models;

namespace clinic.Controllers.Category
{
    public class GetCategoriesController :Controller
    {
        private readonly DataContext _dataContext;
        public GetCategoriesController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        [HttpGet("categories")]
        public async Task<IActionResult> GetCategories(bool withDoctors = false)
        {
            if (!withDoctors)
            {
                var categoriesOnly = await _dataContext.Categories.ToListAsync();
                if (categoriesOnly.IsNullOrEmpty()) return NotFound();
                return Ok(categoriesOnly);
            }


            var categoriesWithDoctors = await _dataContext.Categories.Include(c => c.Users.Where(u=> u.Role == UserRole.Doctor)).Select(c => new
            {
                id = c.Id,
                name = c.Name,
                doctorsCount = c.Users.Count(),

            }).ToListAsync();


            if (categoriesWithDoctors == null) return NotFound();
            return Ok(categoriesWithDoctors);
        }
    }
}
