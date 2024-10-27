using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sempi5.Domain.OperationRequestAggregate.DTOs;
using Sempi5.Services;
using ILogger = Serilog.ILogger;

namespace Sempi5.Controllers;

[Route("[controller]")]
[ApiController]
public class OperationRequestController : ControllerBase
{
    private readonly OperationRequestService _operationRequestService;
    private readonly Serilog.ILogger _logger;

    public OperationRequestController(OperationRequestService operationRequestService, ILogger logger)
    {
        _operationRequestService = operationRequestService;
        _logger = logger;
    }

    [Authorize(Roles = "Doctor")]
    [HttpPost("requestOperation")]
    public async Task<IActionResult> RequestOperation(OperationRequestDTO operationRequestDto)
    {
        try
        {
            await _operationRequestService.RequestOperation(operationRequestDto);
            return Ok("Operation request submitted successfully");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message+e.StackTrace);
        }
    }

    [HttpPost("updateOperationRequest/deadline{deadline}")]
    public async Task<IActionResult> UpdateOperationRequestDeadline(OperationRequestDTO operationRequestDto, string doctor)
    {
        try
        {
            await _operationRequestService.UpdateOperationRequestDeadline(operationRequestDto, doctor);
            return Ok("Operation request updated successfully");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpPost("updateOperationRequest/{priority}")]
    public async Task<IActionResult> UpdateOperationRequestPriority(OperationRequestDTO operationRequestDto, string doctor)
    {
        try
        {
            await _operationRequestService.UpdateOperationRequestPriority(operationRequestDto, doctor);
            return Ok("Operation request updated successfully");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}