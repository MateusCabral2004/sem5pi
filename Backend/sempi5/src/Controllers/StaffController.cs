using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Sempi5.Domain.Encrypt;
using Sempi5.Domain.PatientAggregate;
using Sempi5.Domain.SpecializationAggregate;
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditStaffProfile(EditStaffDTO editStaffDto)
        {
            try
            {
                var staff = await _staffService.GetStaffById(editStaffDto.Id);

                if (editStaffDto.email != null || editStaffDto.phoneNumber > 0)
                {
                    if (editStaffDto.email != null) await _staffService.VerifyEmailAvailability(editStaffDto.email);
                    if (editStaffDto.phoneNumber > 0)
                        await _staffService.VerifyPhoneNumberAvailability(editStaffDto.phoneNumber);
                    
                    
                    await _emailService.PrepareEditStaffConfirmationEmail(staff.Person.ContactInfo._email.ToString(),
                        editStaffDto);
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
                return BadRequest(e.Message);
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
                
                _logger.ForContext("CustomLogLevel", "CustomLevel")
                    .Information($"\nChanges To Staff {editStaffDto.Id} :" +
                                 $" {editStaffDto.phoneNumber}" +
                                 $" {editStaffDto.email}" +
                                 $" {editStaffDto.specialization} \n");
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeactivateStaffProfile(StaffIdDTO staffId)
        {
            try
            {
                await _staffService.DeactivateStaffProfile(staffId);
                return Ok(new { message = "Staff deactivated successfully." });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpGet("listStaffProfilesByName")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ListStaffProfileByName(NameDTO nameDto)
        {
            try
            {
                
                //var nameDto = new NameDTO {name = name};
                
                var staffProfile = await _staffService.ListStaffByName(nameDto);
                return Ok(staffProfile);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("listStaffProfileByEmail")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ListStaffProfileByEmail(EmailDTO emailDto)
        {
            try
            {
                var staffProfiles = await _staffService.ListStaffByEmail(emailDto);
                return Ok(staffProfiles);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("request/deleteRequest")]
       [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> DeleteRequest()
        {
            try
            {
                 await _staffService.DeleteRequestAsync(getEmail());
                return Ok("Operation request deleted successfully");
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("listStaffProfilesBySpecialization")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ListStaffProfilesBySpecialization(SpecializationNameDTO specializationDto)
        {
            try
            {
                var staffProfile = await _staffService.ListStaffBySpecialization(specializationDto);
                return Ok(staffProfile);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message + e.StackTrace);
            }
        }
        
        [HttpGet("listAllStaffProfiles")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ListAllStaffProfiles()
        {
            try
            {
                var staffProfile = await _staffService.ListAllStaff();
                return Ok(staffProfile);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpGet("search/requests")]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> SearchRequests(SeachFilterDto seachFilterDto)
        {
            try
            {
                var requests = await _staffService.SearchRequestsAsync(seachFilterDto.patientName, seachFilterDto.type,
                    seachFilterDto.priority, seachFilterDto.status);

                if (seachFilterDto.status != null)
                {
                    var tableData = new List<object>();

                    for (int i = 0; i < requests.Count; i++)
                    {
                        var operationRequest = requests[i];
                        tableData.Add(new
                        {
                            PatientName = operationRequest.Patient.Person?.FullName.ToString(),
                            OperationType = operationRequest.OperationType.Name.ToString(),
                            Priority = operationRequest.PriorityEnum.ToString(),
                            Status = seachFilterDto.status
                        });
                    }
                    return Ok(tableData);
                }
                else
                {
                    var tableData = new List<object>();

                    for (int i = 0; i < requests.Count; i++)
                    {
                        var operationRequest = requests[i];
                        tableData.Add(new
                        {
                            PatientName = operationRequest.Patient.Person?.FullName.ToString(),
                            OperationType = operationRequest.OperationType.Name.ToString(),
                            Priority = operationRequest.PriorityEnum.ToString(),
                        });
                    }
                    
                    return Ok(tableData);
                }
            }
            catch (Exception e)
            {
                return BadRequest($"Error: {e.Message}");
            }
        }

        public string getEmail()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            return claimsIdentity?.FindFirst(ClaimTypes.Email).Value;
        }
    }
}