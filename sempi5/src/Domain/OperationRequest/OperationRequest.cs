using Sempi5.Domain;
using Sempi5.Domain.OperationType;
using Sempi5.Domain.Shared;


namespace Sempi5.Domain.OperationRequest
{

    public class OperationRequest : Entity<OperationRequestID>, IAggregateRoot
    {
        public OperationRequestID Id { get; set; }
        
        public Staff.Staff Doctor { get; set; }
        
        public Patient.Patient Patient { get; set; }
        
        public OperationType.OperationType OperationType { get; set; }
        
        public DateTime DeadLineDate { get; set; }
        
        public PriorityEnum PriorityEnum { get; set; }
        
        private OperationRequest() {}
     
        public OperationRequest(Staff.Staff doctor, Patient.Patient patient, OperationType.OperationType operationType, DateTime deadLineDate, PriorityEnum priorityEnum)
        {
            Doctor = doctor;
            Patient = patient;
            OperationType = operationType;
            DeadLineDate = deadLineDate;
            PriorityEnum = priorityEnum;
        }
        
        
    }
    
}

