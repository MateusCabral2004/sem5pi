using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
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

        public StaffController(StaffService staffService, Serilog.ILogger logger)
        {
            _staffService = staffService;
            _logger = logger;
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

                if (editStaffDto.email != null || editStaffDto.phoneNumber > 0)
                {
                    await _staffService.PrepareConfirmationEmail(editStaffDto);
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
            
            Console.Write("Inciando edit staff");
            
            EditStaffDTO editStaffDto;
            try
            {
                editStaffDto = JsonSerializer.Deserialize<EditStaffDTO>(jsonString);
                

                Console.Write(editStaffDto);
                
                await _staffService.EditStaffProfile(editStaffDto);
                
            }
            catch (JsonException ex)
            {
                return BadRequest($"Invalid JSON format: {ex.Message}");
            } catch (Exception e)
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
    }
}