# US 5.1.18 - As a doctor, I want to remove an operation requisition so that healthcare activities can be provided as needed.

## Requirements Engineering

### Customer Specifications and Clarifications
**From the specifications document:**
- Doctors should be able to delete operation requisitions that have not yet been scheduled.
- The deletion of the operation requisition must be irreversible, completely removing it from the patient's medical record.
- Removing a requisition should trigger a notification to the Planning Module, adjusting any schedules that depend on this operation.

## Criteria for the Operation Requisition System

### 1. Deletion of Operation Requisitions
- **Criterion:** Deletion of an operation requisition must be irreversible.
- **Specification:** The system must display a confirmation prompt before deletion, and the requisition must be permanently removed upon confirmation.

### 2. Authorization to Delete Requisitions
- **Criterion:** Only the doctor who created the requisition can delete it.
- **Specification:** Deletion can only occur if the corresponding operation has not yet been scheduled.

### Acceptance Criteria
- Doctors can only delete the operation requisitions they created, provided the operation has not yet been scheduled.
- Before deletion, the system displays a confirmation prompt to the doctor.
- Once deleted, the requisition is removed from the patient's medical record and cannot be recovered.
- The system sends a notification to the Planning Module, updating dependent schedules for this requisition.

### Found Out Dependencies
- **Notification Module:** Send notifications to the Planning Module after deletion.
