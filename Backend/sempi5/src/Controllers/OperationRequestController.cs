using System.Security.Claims;
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
            _logger.ForContext("CustomLogLevel", "CustomLevel")
                .Information($"New request created: {operationRequestDto.operationName}");
            return Ok("Operation request submitted successfully");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut("updateOperationRequestDeadline/{operationRequestId}/{deadline}")]
    public async Task<IActionResult> UpdateOperationRequestDeadline(string operationRequestId, string deadline)
    {
        try
        {
            await _operationRequestService.UpdateOperationRequestDeadline(operationRequestId, deadline, getEmail());
            _logger.ForContext("CustomLogLevel", "CustomLevel")
                .Information($"Update To Operation Request {operationRequestId}"+
                             $"Deadline:{deadline}");
            return Ok("Operation request updated successfully");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpPut("updateOperationRequestPriority/{operationRequestId}/{priority}")]
    public async Task<IActionResult> UpdateOperationRequestPriority(string operationRequestId, string priority)
    {
        try
        {
            await _operationRequestService.UpdateOperationRequestPriority(operationRequestId, priority,getEmail());
            _logger.ForContext("CustomLogLevel", "CustomLevel")
                .Information($"Update To Operation Request {operationRequestId}"+
                             $"Priority:{priority}");
            return Ok("Operation request updated successfully");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    public string getEmail()
    {
        var claimsIdentity = User.Identity as ClaimsIdentity;
        return claimsIdentity?.FindFirst(ClaimTypes.Email).Value;
    }
}