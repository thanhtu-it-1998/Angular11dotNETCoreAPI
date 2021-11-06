using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        public readonly AppDbContext context;
        public DepartmentController(AppDbContext _context)
        {
            context = _context;
        }

        [HttpGet]
        public async Task<JsonResult> Get()
        {
            var result = await this.context.Departments.ToListAsync();
            return new JsonResult(result);
        }

        [HttpPost]
        public async Task<JsonResult> Post(Department department)
        {
            await this.context.Departments.AddAsync(department);
            await this.context.SaveChangesAsync();
            return new JsonResult("Added Successfully");
        }
        [HttpPut]
        public async Task<JsonResult> Put(Department department)
        {
            this.context.Update(department);
            await this.context.SaveChangesAsync();
            return new JsonResult("Updated Successfully");
        }
        [HttpDelete("{id}")]
        public async Task<JsonResult> Delete(int id)
        {
            var result = await this.context.Departments.FirstOrDefaultAsync(item => item.DepartmentId == id);
            this.context.Departments.Remove(result);
            await this.context.SaveChangesAsync();
            return new JsonResult("Deleted Successfully");
        }
    }
}
