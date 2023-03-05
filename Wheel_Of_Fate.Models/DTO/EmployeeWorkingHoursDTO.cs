using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wheel_Of_Fate.Models.DTO
{
    public class EmployeeWorkingHoursDTO
    {
        public int Id { get; set; }
        public int WorkingShift { get; set; }
        public DateTime WorkedDate { get; set; }
        [Required]
        public int EmpsId { get; set; }
    }
}
