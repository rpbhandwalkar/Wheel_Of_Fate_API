using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wheel_Of_Fate.Models.DTO
{
    public class EmployeeCreateDTO
    {
        [Key]
        public int EmpId { get; set; }
        [Required]
        public string Name { get; set; }

       
    }
}
