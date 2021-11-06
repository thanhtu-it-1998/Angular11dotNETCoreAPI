using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        public int DepartmentId { get; set; }

        [Required]
        public string EmployeeName { get; set; }
        public string DateOfJoining { get; set; }
        public string PhotoFileName { get; set; }

        [ForeignKey("DepartmentId")]
        public Department Department { get; set; }


    }
}
