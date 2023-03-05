using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Wheel_OF_Fate.DA.Repository.IRepository;
using Wheel_Of_Fate.Models;
using System;
using System.Net;
using Wheel_Of_Fate.Models.DTO;

namespace Wheel_Of_Fate_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[ApiExplorerSettings(IgnoreApi = true)]
    public class EmployeeJobManagementController : ControllerBase
    {

        private readonly APIResponse _response;
        //private readonly IEmployeesRepository _repo;
        private readonly IMapper _mapper;
        private readonly IEmployeeWorkingHoursRepository _employeeWorking;
        private int _totalWorkingHours, _WorkingHourID = 0;

        public EmployeeJobManagementController(IEmployeesRepository repo, IMapper mapper, IEmployeeWorkingHoursRepository employeeWorking)
        {
            _response = new();
            //_repo = repo;
            _mapper = mapper;
            _employeeWorking = employeeWorking;
        }


        [HttpGet]
        public async Task<ActionResult<APIResponse>> GetEmployeeHours()
        {
            try
            {
                IEnumerable<EmployeeWorkingHours> _emphour = await _employeeWorking.GetAllAsync();
                _response.Result = _mapper.Map<List<EmployeeWorkingHoursDTO>>(_emphour);
                _response.IsSuccess = true;
                _response.StatusCode = HttpStatusCode.OK;

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = new List<string>() { ex.ToString() };
            }


            return Ok(_response);
        }

        [HttpGet("{EmpId:int}", Name = "GetEmployeeHoursByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetEmployeeByID(int EmpId)
        {
            try
            {
                if (EmpId == 0)
                {
                    //logger.LogError("Caught not supported Id =" + id);
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                List<EmployeeWorkingHours> _empHour = await _employeeWorking.GetAllAsync(x => x.EmpsId == EmpId);
                if (_empHour == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                //logger.LogInformation("Getting Villas with respect to id");
                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = _mapper.Map<List<EmployeeWorkingHoursDTO>>(_empHour);
                _response.IsSuccess = true;

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = new List<string>() { ex.ToString() };
            }
            return Ok(_response);

        }




        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> AddEmployeeHours([FromBody] EmployeeWorkingHoursDTO createDTO)
        {
            try
            {
                if (createDTO == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                EmployeeWorkingHours model = _mapper.Map<EmployeeWorkingHours>(createDTO);
                await _employeeWorking.Create(model);

                //logger.LogInformation("Create new Villas");
                _response.StatusCode = HttpStatusCode.Created;
                _response.Result = _mapper.Map<EmployeeWorkingHours>(model);
                _response.IsSuccess = true;
                //return CreatedAtRoute("GetEmployeeByID", new { EmpId = model.EmpsId }, _response);
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = new List<string>() { ex.InnerException.ToString() };
                return BadRequest(_response);
            }
        }


        [HttpPut("{EmpID:int}", Name = "UpdateEmployeeHours")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<APIResponse>> UpdateEmployee(int EmpID, [FromBody] EmployeeWorkingHoursUpdateDTO updateDTO)
        {
            try
            {
                if (updateDTO == null || EmpID != updateDTO.EmpsId)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);

                }

                EmployeeWorkingHours model = _mapper.Map<EmployeeWorkingHours>(updateDTO);

                await _employeeWorking.UpdateAsync(model);
                //logger.LogInformation("Update Villas");
                _response.StatusCode = HttpStatusCode.NoContent;
                //_response.Result = mapper.Map<VillaDto>(model);
                _response.IsSuccess = true;

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = new List<string>() { ex.ToString() };
            }
            return Ok(_response);
        }


        [HttpDelete("{id:int}", Name = "DeleteHours")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<APIResponse>> DeleteHours(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                EmployeeWorkingHours employee = await _employeeWorking.Get(x => x.Id == id);
                if (employee == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return NotFound(_response);
                }
                //logger.LogInformation("Delete Villa");
                await _employeeWorking.Remove(employee);

                _response.StatusCode = HttpStatusCode.NoContent;
                //_response.Result = mapper.Map<VillaDto>(model);
                _response.IsSuccess = true;

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = new List<string>() { ex.ToString() };
            }

            return Ok(_response);
        }

    }
}
