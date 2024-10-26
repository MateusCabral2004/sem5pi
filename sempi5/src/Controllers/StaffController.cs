using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Sempi5.Domain.Encrypt;
using Sempi5.Domain.StaffAggregate;
using Sempi5.Domain.StaffAggregate.DTOs;
using Sempi5.Services;

namespace Sempi5.Controllers.StaffControllers
{
    [Route("[controller]")]
    [ApiController]
    // [Authorize]
    public class StaffController : ControllerBase
    {
        private readonly StaffService _staffService;
        private readonly Serilog.ILogger _logger;
        private readonly EmailService _emailService;

        public StaffController(StaffService staffService, Serilog.ILogger logger, EmailService emailService)
        {
            _staffService = staffService;
            _logger = logger;
            _emailService = emailService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StaffDTO>>> GetAllStaffMembers()
        {
            return Ok();
        }

        [HttpGet("{email}")]
        public async Task<ActionResult<StaffDTO>> GetStaffMember(string email)
        {
            return Ok();
        }

        [HttpPost("createStaffProfile")]
        public async Task<IActionResult> CreateStaffProfile(StaffDTO staff)
        {
            try
            {
                await _staffService.CreateStaffProfile(staff);
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
                
                var staff = await _staffService.GetStaffById(editStaffDto.Id);
                
                if (editStaffDto.email != null || editStaffDto.phoneNumber > 0)
                {
                    await _emailService.PrepareEditStaffConfirmationEmail(staff.Person.ContactInfo._email.ToString() ,editStaffDto);
                }
                else
                {
                    await _staffService.EditStaffProfile(editStaffDto);
                    _logger.ForContext("CustomLogLevel", "CustomLevel")
                        .Information($"\nChanges To Staff {editStaffDto.Id} :" +
                                     $" {editStaffDto.phoneNumber}" +
                                     $" {editStaffDto.email}" +
                                     $" {editStaffDto.specialization} \n");

                    return Ok("Staff profile edited successfully!");
                }

                return Ok("A confirmation email was sent.");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message + e.StackTrace);
            }
        }
        

        [HttpGet("editStaffProfile/{jsonString}")]
        public async Task<IActionResult> EditStaffProfile(string jsonString)
        {
            EditStaffDTO editStaffDto;
            try
            {
                var cryptography = new Cryptography();
                var encryptedString = cryptography.DecryptString(jsonString);
                    
                editStaffDto = JsonSerializer.Deserialize<EditStaffDTO>(encryptedString);
                
                await _staffService.EditStaffProfile(editStaffDto);
            }
            catch (JsonException ex)
            {
                return BadRequest($"Invalid JSON format: {ex.Message}");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok("Staff profile edited successfully!");
        }

        [HttpPatch("deactivateStaffProfile")]
        public async Task<IActionResult> DeactivateStaffProfile(StaffIdDTO staffId)
        {
            try
            {
                await _staffService.DeactivateStaffProfile(staffId);
                return Ok("Staff deleted successfully");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /*
        [HttpGet("listStaffProfilesByName")]
        public async Task<IActionResult> ListStaffProfilesByName(NameDTO nameDto)
        {
            try
            {
         //       var staffProfile = await _staffService.ListStaffByName(nameDto);
             //   return Ok(staffProfile);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        */
    }
}