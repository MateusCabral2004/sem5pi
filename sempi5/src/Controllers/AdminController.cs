using Microsoft.AspNetCore.Mvc;
using Sempi5.Domain.Patient;
using Sempi5.Domain.User;
using Sempi5.Services;
    
namespace Sempi5.Controllers
{   
    [Route("[controller]")]
    [ApiController]
    //[Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly AdminService _adminService;
    
        public AdminController(AdminService staffService)
        {
            _adminService = staffService;
        }
    
        [HttpGet]
        public IActionResult Home()
        {
            return Ok("Welcome to the Admin Page");
        }
    
        [HttpPost("registerStaff")]
        public async Task<IActionResult> RegisterStaff(SystemUserDTO user)
        {
            try
            { 
                await _adminService.RegisterUser(user);
                return Ok("Staff member registered successfully");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        [HttpGet("listPatientProfilesByEmail")]
        public async  Task<IActionResult> ListPatientProfilesByEmail(string email)
        {
            try
            {
                
                var patientProfiles = await _adminService.ListPatientByEmail(email);
                return Ok(patientProfiles);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message + e.StackTrace);
            }
        }
        
        [HttpGet("listPatientProfilesByName")]
        public async Task<IActionResult> ListPatientProfilesByName(string name)
        {
            try
            {
                var patientProfiles = await _adminService.ListPatientByName(name);
                return Ok(patientProfiles);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message + e.StackTrace);
            }
        }

        [HttpPost(template: "registerPatientProfile")]
        public async Task<IActionResult> RegisterPatientProfile(PatientDTO patient)
        {
            try
            {
                await _adminService.CreatePatientProfile(patient);
                return Ok("Patient profile created successfully");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message + e.StackTrace);
            }
        }
    }
}