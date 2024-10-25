using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Sempi5.Domain.PatientAggregate;
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
        public async Task<IActionResult> ListPatientProfilesByEmail(EmailDTO emailDto)
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
        public async Task<IActionResult> ListPatientProfilesByMedicalRecordNumber(
            PatientIdDto patientId)
        {
            try
            {
                var patientProfiles = await _adminService.ListPatientByMedicalRecordNumber(patientId);
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
        
        [HttpPut("editPatientRecord")]
        public async Task<IActionResult> EditPatientRecord(EditPatientRecordDTO editPatientRecord)
        {
            try
            {
                var patientDto = await _adminService.EditPatientRecord(editPatientRecord);
                
                return Ok("Patient profile edited successfully.");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        [HttpPut("deletePatientRecord")]
        public async Task<IActionResult> DeletePatientRecord(PatientIdDto patientIdDto)
        {
            try
            {
                var patientDto = await _adminService.DeletePatientRecord(patientIdDto);
                
                return Ok("Patient profile deleted successfully.");
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

        [HttpPost("editPatientProfile2")]
        public async Task<IActionResult> EditPatientProfile2(PatientDTO patientDto)
        {
            try
            {
                await _adminService.EditPatientProfile2(patientDto);
                return Ok("Patient profile edited successfully");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        
        [HttpPost("deletePatientProfile2")]
        public async Task<IActionResult> DeletePatientProfile2(PatientDTO patientDto)
        {
            try
            {
                await _adminService.DeletePatientProfile2(patientDto);
                return Ok("Patient deleted successfully");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


    }
}