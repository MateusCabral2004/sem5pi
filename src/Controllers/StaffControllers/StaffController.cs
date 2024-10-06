using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sempi5.Domain.Staff.DTOs;
using Sempi5.Infrastructure.Databases;
using Sempi5.Services;

namespace Sempi5.Controllers.StaffControllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class StaffController : Controller
    {

        private readonly DBContext dataBase;

        public StaffController(DBContext context)
        {
            dataBase = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StaffDTO>>> GetAllStaffMembers()
        {

            var staffMembers = await dataBase.StaffMembers
                .Select(s => new StaffDTO
                {
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    FullName = s.FullName,
                    LicenseNumber = s.LicenseNumber,
                    Specialization = s.Specialization,
                    ContactInfo = s.ContactInfo,
                    Email = s.User.Email,
                    Role = s.User.Role,
                    Password = s.Password,
                    AvailabilitySlots = s.AvailabilitySlots,
                }).ToListAsync();


            // Check if the list is empty
            if (staffMembers == null)
            {
                return NotFound("Staff members is null");
            }
            else if (staffMembers.Count == 0)
            {
                return NotFound("No staff members found");
            }

            return Ok(staffMembers);
        }

        [HttpGet("{email}")]
        public async Task<ActionResult<StaffDTO>> GetStaffMember(string email)
        {
            var staffMember = await dataBase.StaffMembers
                .Include(s => s.User)
                .FirstOrDefaultAsync(s => s.User.Email == email);

            if (staffMember == null)
            {
                return NotFound();
            }

            var staffDto = ManageStaffService.StaffToDTO(staffMember);

            return Ok(staffDto);
        }






    }
}