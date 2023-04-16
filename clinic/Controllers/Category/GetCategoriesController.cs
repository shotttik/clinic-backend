﻿using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;

namespace clinic.Controllers.Category
{
    public class GetCategoriesController :Controller
    {
        private readonly DataContext _dataContext;
        public GetCategoriesController (DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        [HttpGet("categories")]
        public async Task<IActionResult> GetCategories(bool withDoctors=false)
        {
            if (!withDoctors)
            {
                var categoriesOnly = await _dataContext.Categories.ToListAsync();
                if (categoriesOnly == null) return NotFound();
                return Ok(categoriesOnly);
            }
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            };

            var categoriesWithDoctors = JsonSerializer.Serialize(await _dataContext.Categories.Include(c => c.Doctors ).ToListAsync(), options);


            if (categoriesWithDoctors == null) return NotFound();
            return Ok(categoriesWithDoctors);
        }
    }
}