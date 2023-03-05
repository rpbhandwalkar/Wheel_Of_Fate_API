using AutoMapper;
using Wheel_Of_Fate.Models;
using Wheel_Of_Fate.Models.DTO;

namespace Wheel_Of_Fate.BL.Mapper
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {

            CreateMap<Employee, EmployeeDTO>().ReverseMap();
            CreateMap<Employee, EmployeeCreateDTO>().ReverseMap();
            CreateMap<Employee, EmployeeUpdateDTO>().ReverseMap();

            CreateMap<EmployeeWorkingHours, EmployeeWorkingHoursDTO>().ReverseMap();
            CreateMap<EmployeeWorkingHours, EmployeeWorkingHoursUpdateDTO>().ReverseMap();
   
        }
    }
}
