using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;

namespace clinic.Controllers.Category
{
    [Authorize(Policy = "Admin")]
    public class DeleteCategory :Controller
    {
        private readonly DataContext _dataContext;
        public DeleteCategory(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpPost("deleteCategory/{id}")]
        public async Task<IActionResult> DelCategory(int id)
        {

            var category = await _dataContext.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            var doctor = await _dataContext.Doctors.FirstOrDefaultAsync(x => x.CategoryId == id);
            if (doctor != null)
            {
                return Conflict("წაშლა შეუძლებელია, რადგან ექიმი ამ კატეგორიით არსებობს.");
            }
            _dataContext.Categories.Remove(category);
            await _dataContext.SaveChangesAsync();
            return Ok();
        }
    }
}
