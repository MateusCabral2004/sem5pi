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
    public class StaffController : ControllerBase
    {

        private readonly DBContext dataBase;

        public StaffController(DBContext context)
        {
            dataBase = context;
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






    }
}