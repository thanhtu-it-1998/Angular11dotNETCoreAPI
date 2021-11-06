using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : Controller
    {
        public readonly AppDbContext context;
        private readonly IWebHostEnvironment env;
        public EmployeeController(AppDbContext _context, IWebHostEnvironment _env)
        {
            context = _context;
            env = _env;
        }
        [HttpGet]
        public async Task<JsonResult> Get()
        {
            var result = await this.context.Employees.ToListAsync();
            return new JsonResult(result);
        }

        [HttpPost]
        public async Task<JsonResult> Post(Employee employee)
        {
            await this.context.Employees.AddAsync(employee);
            await this.context.SaveChangesAsync();
            return new JsonResult("Added Successfully");
        }
        [HttpPut]
        public async Task<JsonResult> Put(Employee employee)
        {
            this.context.Update(employee);
            await this.context.SaveChangesAsync();
            return new JsonResult("Updated Successfully");
        }
        [HttpDelete("{id}")]
        public async Task<JsonResult> Delete(int id)
        {
            var result = await this.context.Employees.FirstOrDefaultAsync(item => item.EmployeeId == id);
            this.context.Employees.Remove(result);
            await this.context.SaveChangesAsync();
            return new JsonResult("Added Successfully");
        }
        [Route("SaveFile")]
        [HttpPost]
        public JsonResult SaveFile()
        {
            try
            {
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                string filename = postedFile.FileName;
                var physicalPath = env.ContentRootPath + "/Photos/" + filename;

                using (var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }

                return new JsonResult(filename);
            }
            catch (Exception)
            {

                return new JsonResult("anonymous.png");
            }
        }

        [Route("GetAllDepartmentNames")]
        [HttpGet]
        public async Task<JsonResult> GetAllDepartmentNames()
        {
            var result = await this.context.Departments.Select(item => item.DepartmentName).ToListAsync();
            return new JsonResult(result);
        }


    }
}
