using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Wheel_Of_Fate.Models;
using Wheel_Of_Fate.Models.DTO;
using Wheel_OF_Fate.DA.Repository.IRepository;

namespace Wheel_Of_Fate_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MergedController : ControllerBase
    {
        private readonly APIResponse _response;
        private readonly IEmployeesRepository _repo;
        private readonly IMapper _mapper;
        private readonly IEmployeeWorkingHoursRepository _employeeWorking;
        private readonly IShiftRepository _shiftRepository;
        private int _totalWorkingHours, _WorkingHourID = 0;

        public MergedController(IEmployeesRepository repo, IMapper mapper,
            IEmployeeWorkingHoursRepository employeeWorking, IShiftRepository shiftRepository)
        {
            _response = new();
            _repo = repo;
            _mapper = mapper;
            _employeeWorking = employeeWorking;
            _shiftRepository = shiftRepository;

        }


        [HttpGet]
        public async Task<ActionResult<APIResponse>> GetAvaliableEmployees()
        {
            try
            {
                // _response.Result = _mapper.Map<List<EmployeeDTO>>(employees);
                IEnumerable<Employee> employees = await _repo.GetAllAsync();
                IEnumerable<EmployeeWorkingHours> workingHours = await _employeeWorking
                    .GetAllAsync(x => x.WorkedDate.Date >= DateTime.Today.Date.AddDays(-14));

                IEnumerable<Shift> shifts = await _shiftRepository.GetAllAsync();
                //List<EmployeeDTO> employees1 = _mapper.Map<List<EmployeeDTO>>(employees);
                List<MergedDTO> mergeds = new List<MergedDTO>();
                bool isShift = false;
                if (workingHours != null && workingHours.Count()!=0)
                {
                    foreach (var employee in employees)
                    {
                        foreach (var item in workingHours)
                        {
                           
                            if (employee.EmpId == item.EmpsId)
                            {
                                if (item.WorkingShift == 0)
                                {
                                    isShift = true;
                                }
                               
                                mergeds.Add(new MergedDTO
                                {
                                    WorkingHourID = item.Id,
                                    EmpId = employee.EmpId,
                                    ShiftType = shifts.Where(x=>x.Id == item.WorkingShift).FirstOrDefault().ShiftType,
                                    Name = employee.Name
                                });
                            }

                        }
                       
                    }
                }
                _response.Result = mergeds;
                _response.IsSuccess = true;
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = new List<string>() { ex.ToString() };
                return Ok(_response);
            }
        }

        [HttpGet("{EmpID:int}", Name = "GetEmployeeHours")]
        public async Task<ActionResult<APIResponse>> GetEmployeeHours(int EmpID)
        {
            try
            {
                // _response.Result = _mapper.Map<List<EmployeeDTO>>(employees);
                IEnumerable<Employee> employees = await _repo.GetAllAsync(x => x.EmpId == EmpID);
                IEnumerable<EmployeeWorkingHours> workingHours = await _employeeWorking
                    .GetAllAsync(x => x.WorkedDate.Date >= DateTime.Today.Date.AddDays(-14) && x.EmpsId == EmpID);
                //List<EmployeeDTO> employees1 = _mapper.Map<List<EmployeeDTO>>(employees);

                List<Merged> mergeds = new List<Merged>();
                
                    foreach (var employee in employees)
                    {
                        foreach (var item in workingHours)
                        {
                            if (employee.EmpId == item.EmpsId)
                            {
                                _WorkingHourID = item.Id;
                                _totalWorkingHours += item.WorkingShift;
                            }

                        }

                        mergeds.Add(new Merged
                        {
                            WorkingHourID = _WorkingHourID,
                            EmpId = employee.EmpId,
                            Hours = _totalWorkingHours,
                            Name = employee.Name
                        });
                        _totalWorkingHours = 0;

                    }

                
                _response.Result = mergeds;
                _response.IsSuccess = true;
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = new List<string>() { ex.ToString() };
                return Ok(_response);
            }
        }


    }
}
