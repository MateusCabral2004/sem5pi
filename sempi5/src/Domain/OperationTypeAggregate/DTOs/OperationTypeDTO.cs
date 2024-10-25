using Sempi5.Domain.RequiredStaffAggregate.DTOs;

namespace Sempi5.Domain.OperationTypeAggregate.DTOs;

public class OperationTypeDTO
{
    public string OperationName { get; set; }
        
    public List<RequiredStaffDTO> RequiredStaff { get; set; }
        
    public string SetupDuration { get; set; }
    
    public string SurgeryDuration { get; set; }
    
    public string CleaningDuration { get; set; }
}