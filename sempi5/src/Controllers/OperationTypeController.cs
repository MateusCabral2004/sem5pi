using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sempi5.Domain.OperationTypeAggregate;
using Sempi5.Domain.OperationTypeAggregate.DTOs;
using Sempi5.Domain.RequiredStaffAggregate;
using Sempi5.Domain.RequiredStaffAggregate.DTOs;
using Sempi5.Domain.Shared;
using Sempi5.Domain.SpecializationAggregate;
using Sempi5.Infrastructure.OperationTypeRepository;
using Sempi5.Infrastructure.SpecializationRepository;
using Sempi5.Services;

namespace Sempi5.Controllers;

[Route("[controller]")]
[ApiController]
public class OperationTypeController : ControllerBase
{
    private readonly OperationTypeService _operationTypeService;
    private readonly Serilog.ILogger _logger;

    public OperationTypeController(OperationTypeService operationTypeService, Serilog.ILogger logger)
    {
        _operationTypeService = operationTypeService;
        _logger = logger;
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("addNewOperationType")]
    public async Task<IActionResult> AddNewOperationType(OperationTypeDTO operationType)
    {
        Console.WriteLine("Operation type: " + operationType.OperationName.ToString());
        try
        {
            await _operationTypeService.AddNewOperationType(operationType);
            _logger.ForContext("CustomLogLevel", "CustomLevel")
                .Information($"New operation created: {operationType.OperationName}");

            return Ok("Operation type added successfully");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message + e.StackTrace);
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("deleteOperationType/{operationTypeName}")]
    public async Task<IActionResult> DeleteOperationType(string operationTypeName)
    {
        try
        {
            await _operationTypeService.DeleteOperationType(operationTypeName);
            return Ok("Operation type deleted successfully");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message + e.StackTrace);
        }
    }
    
    [Authorize(Roles = "Admin")]
    [HttpPut("editOperationType/name/{oldOperationName}")]
    public async Task<IActionResult> EditOperationType(string oldOperationName, [FromBody] string newOperationName)
    {
        try
        {
            await _operationTypeService.EditOperationTypeName(oldOperationName, newOperationName);
            return Ok("Operation type edited successfully");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message + e.StackTrace);
        }
    }
    
    [Authorize(Roles = "Admin")]
    [HttpPut("editOperationType/requiredStaff/add/{operationTypeName}")]
    public async Task<IActionResult> AddRequiredStaffToOperationType(RequiredStaffDTO requiredStaffDto,string operationTypeName)
    {
        try
        {
            await _operationTypeService.AddRequiredStaffToOperationType(operationTypeName, requiredStaffDto);
            return Ok("Required staff added to operation type successfully");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message + e.StackTrace);
        }
    }
    
    [Authorize(Roles = "Admin")]
    [HttpPut("editOperationType/requiredStaff/remove/{operationTypeName}")]
    public async Task<IActionResult> RemoveRequiredStaffFromOperationType(string operationTypeName,
        [FromBody] string specializationName)
    {
        try
        {
            await _operationTypeService.RemoveRequiredStaffFromOperationType(operationTypeName, specializationName);
            return Ok("Required staff removed from operation type successfully");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message + e.StackTrace);
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("editOperationType/duration/{durationType}/{operationTypeName}")]
    public async Task<IActionResult> EditOperationTypeDuration(string durationType, string operationTypeName,
        [FromBody] string newDuration)
    {
        try
        {
            switch (durationType)
            {
                case "setup":
                    await _operationTypeService.EditOperationTypeSetupDuration(operationTypeName, newDuration);
                    break;
                case "surgery":
                    await _operationTypeService.EditOperationTypeSurgeryDuration(operationTypeName, newDuration);
                    break;
                case "cleaning":
                    await _operationTypeService.EditOperationTypeCleaningDuration(operationTypeName, newDuration);
                    break;
                default:
                    return BadRequest("Invalid duration type");
            }

            return Ok("Operation type duration edited successfully");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message + e.StackTrace);
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("listOperationTypeByName")]
    public async Task<IActionResult> ListOperationTypesByName(OperationNameDTO operationName)
    {
        try
        {
            var listOperationTypeByName = await _operationTypeService.ListOperationTypeByName(operationName);
            return Ok(listOperationTypeByName);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpGet("listOperationTypeBySpecialization/{specializationName}")]
    public async Task<IActionResult> ListOperationTypesBySpecialization(SpecializationNameDTO specializationName)
    {
        try
        {
           var listOperationTypeBySpecialization= await _operationTypeService.ListOperationTypeBySpecialization(specializationName);
            return Ok(listOperationTypeBySpecialization);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpGet("listOperationTypeByStatus/{status}")]
    public async Task<IActionResult> ListOperationTypesByStatus(bool status)
    {
        try
        {
            var listOperationTypeByStatus = await _operationTypeService.ListOperationTypeByStatus(status);
            return Ok(listOperationTypeByStatus);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

}