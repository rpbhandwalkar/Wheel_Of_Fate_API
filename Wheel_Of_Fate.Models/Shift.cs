using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wheel_Of_Fate.Models
{
    public class Shift
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Shift Type")]
        public string ShiftType { get; set; }
    }
}
