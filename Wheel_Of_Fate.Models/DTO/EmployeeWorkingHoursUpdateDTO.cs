using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wheel_Of_Fate.Models.DTO
{
    public class EmployeeWorkingHoursUpdateDTO
    {
        public int Id { get; set; }
        public int WorkingHours { get; set; }

        [Required]
        public int EmpsId { get; set; }
        

        public DateTime WorkedDate { get; set; }
    }
}
