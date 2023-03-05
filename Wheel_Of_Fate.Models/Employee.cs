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
    public class Employee
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EmpId { get; set; }
        [Required]
        public string Name { get; set; }

    }
}
