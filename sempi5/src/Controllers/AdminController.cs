using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Sempi5.Domain.Patient;
using Sempi5.Domain.Shared;
using Sempi5.Domain.Staff.DTOs;
using Sempi5.Domain.Staff;
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
        
        [HttpGet("listPatientProfilesByName")]
        public async Task<IActionResult> ListPatientProfilesByName(NameDTO nameDto)
        {
            try
            {
                var patientProfile = await _adminService.ListPatientByName(nameDto);
                return Ok(patientProfile);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        [HttpGet("listPatientProfilesByEmail")]
        public async  Task<IActionResult> ListPatientProfilesByEmail(EmailDTO emailDto)
        {
            try
            {
                
                var patientProfiles = await _adminService.ListPatientByEmail(emailDto);
                return Ok(patientProfiles);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message + e.StackTrace);
            }
        }
        
        [HttpGet("listPatientProfilesByMedicalRecordNumber")]
        public async Task<IActionResult> ListPatientProfilesByMedicalRecordNumber(MedicalRecordNumberDTO medicalRecordNumberDto)
        {
            try
            {
                var patientProfiles = await _adminService.ListPatientByMedicalRecordNumber(medicalRecordNumberDto);
                return Ok(patientProfiles);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        [HttpGet("listPatientProfilesByDateOfBirth")]
        public async Task<IActionResult> ListPatientProfilesByDateOfBirth(DateDTO dateDto)
        {
            try
            {
                var patientProfiles = await _adminService.ListPatientByDateOfBirth(dateDto);
                return Ok(patientProfiles);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        } 

        [HttpPost("registerPatientProfile")]
        public async Task<IActionResult> RegisterPatientProfile(PatientDTO patient)
        {
            Console.WriteLine("OLA");
            try
            {
                await _adminService.CreatePatientProfile(patient);
                return Ok("Patient profile created successfully");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}