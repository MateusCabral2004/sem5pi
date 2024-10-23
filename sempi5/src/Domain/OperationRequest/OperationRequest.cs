using Sempi5.Domain;
using Sempi5.Domain.Shared;


namespace Sempi5.Domain.OperationRequest
{

    public class OperationRequest : Entity<OperationRequestID>, IAggregateRoot
    {
        public OperationRequestID Id { get; set; }
        public Staff.Staff doctor { get; set; }
        public Patient.Patient patient { get; set; }
        
        
    }
    
}

