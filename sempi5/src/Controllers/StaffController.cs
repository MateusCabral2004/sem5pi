using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sempi5.Domain.PatientAggregate;
using Sempi5.Domain.StaffAggregate;
using Sempi5.Domain.StaffAggregate.DTOs;
using Sempi5.Infrastructure.Databases;
using Sempi5.Services;

namespace Sempi5.Controllers.StaffControllers
{
    [Route("[controller]")]
    [ApiController]
   // [Authorize]
    public class StaffController : ControllerBase
    {
   private readonly StaffService _staffService;  
        
        public StaffController(StaffService staffService)
        {
            _staffService = staffService;
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
                await _staffService.EditStaffProfile(editStaffDto);
                return Ok("Staff profile edited successfully");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message + e.StackTrace);
            }
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