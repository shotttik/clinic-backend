using clinic.Schemas;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using System.Net.Http.Headers;
using clinic.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace clinic.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    [Authorize(Policy = "IsAdmin")]
    public class CreateDoctorController :Controller
    {
        private readonly DataContext _dataContext;
        public CreateDoctorController (DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        [HttpPost("createDoctor")]
        public async Task<IActionResult> CreateDoctor([FromBody] CreateDoctorRequest request)
        {
            if (!ModelState.IsValid)
            {
                var errors = new List<string>();
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        errors.Add(error.ErrorMessage);
                    }
                }
                return BadRequest(errors);
            }
            if (await _dataContext.Doctors.AnyAsync(d => d.Email == request.Email || d.Pid == request.Pid))
            {
                return BadRequest("ექიმი უკვე რეგისტრირებულია.");
            }
            var doctor = new Doctor
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Pid = request.Pid,
                CategoryId = request.CategoryId,
            };
            if (request.Image != null)
            {
                var imagePath = await Upload(request.Image);
                doctor.Image = imagePath;
            }
            if (request.Document != null)
            {
                var documentPath = await Upload(request.Image);
                doctor.Document = documentPath;
            }
            await _dataContext.Doctors.AddAsync(doctor);
            await _dataContext.SaveChangesAsync();


            return Ok();
        }

        public async Task<string> Upload(IFormFile file)
        {
            
            var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var folderName = "";
            var IsImage = false;
            if (fileName.EndsWith(".pdf"))
            {
                folderName = Path.Combine("Resources", "Documents");
            }
            else
            {
                folderName = Path.Combine("Resources", "Images");
                IsImage = true;
            }
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

            var dbPath = Path.Combine(folderName, fileName);
            var fullPath = Path.Combine(pathToSave, fileName);
            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            if (IsImage)
            {
                string [] paths = { Directory.GetCurrentDirectory(), folderName, "Thumbnails", fileName };
                dbPath = Path.Combine(paths.Skip(1).ToArray());
                var thumbPath = Path.Combine(paths);
                Utils.GenerateThumbnail(fullPath, thumbPath);
            }
            return dbPath;
        }
    }
}

