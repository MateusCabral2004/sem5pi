using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Sempi5.Domain.OperationTypeAggregate.DTOs;
using Sempi5.Domain.PatientAggregate;
using Sempi5.Domain.RequiredStaffAggregate.DTOs;
using Sempi5.Domain.Shared;
using Sempi5.Domain.StaffAggregate.DTOs;
using Sempi5.Domain.StaffAggregate;
using Sempi5.Domain.UsefullDTOs;
using Sempi5.Domain.User;
using Sempi5.Infrastructure.StaffRepository;
using Sempi5.Services;

namespace Sempi5.Controllers
{
    [Route("[controller]")]
    [ApiController]
    //[Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly AdminService _adminService;

        private readonly Serilog.ILogger _logger; 

        public AdminController(AdminService staffService, Serilog.ILogger logger)
        {
            _adminService = staffService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Home()
        {
            return Ok("Welcome to the Admin Page");
        }

        [HttpPost("registerStaff")]
        public async Task<IActionResult> RegisterStaff(RegisterUserDTO user)
        {
            try
            {
                Console.WriteLine("StaffId: " + user.staffOrStaffId);
                await _adminService.RegisterUser(user);
                return Ok("Staff member registered successfully");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
       

        [HttpGet("viewPatientRecord")]
        public async Task<IActionResult> ViewPatientRecord(PatientIdDto patientId)
        {
            try
            {
                var patientRecord = await _adminService.GetPatientRecordByPatientId(patientId);
                return Ok(patientRecord);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        

        [HttpPost("createStaffProfile")]
        public async Task<IActionResult> CreateStaffProfile(StaffDTO staff)
        {
            try
            {
                await _adminService.CreateStaffProfile(staff);
                return Ok("Staff profile created successfully");
            }
            catch (Exception e)
            {
                return BadRequest("Error creating Staff:" + e.Message);
            }
        }
        
        
        [HttpPatch("editStaffProfile")]
        public async Task<IActionResult> EditStaffProfile(EditStaffDTO editStaffDto)
        {
            try
            {
                await _adminService.EditStaffProfile(editStaffDto);
                return Ok("Staff profile edited successfully");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message + e.StackTrace);
            }
        }
        
        
    }
}