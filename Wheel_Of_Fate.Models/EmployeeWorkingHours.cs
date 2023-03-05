using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wheel_Of_Fate.Models
{
    public class EmployeeWorkingHours
    {
        public int Id { get; set; }
        public int WorkingShift { get; set; }
        [DataType(DataType.Date)]
        public DateTime WorkedDate { get; set; }

        [Required]
        [ForeignKey("employee")]
        public int EmpsId { get; set; }
        [ValidateNever]
        public Employee employee { get; set; }
    }
}
