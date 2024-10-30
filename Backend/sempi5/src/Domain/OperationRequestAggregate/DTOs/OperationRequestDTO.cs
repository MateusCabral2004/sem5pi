namespace Sempi5.Domain.OperationRequestAggregate.DTOs;

public class OperationRequestDTO
{
    public string? operationRequestId { get; set; }
    public string patientID { get; set; } 
    public string doctorId{ get; set; }
    public string operationName { get; set; }
    public string operationType { get; set; }
    public string deadline{ get; set; }
    public string priority{ get; set; }
}