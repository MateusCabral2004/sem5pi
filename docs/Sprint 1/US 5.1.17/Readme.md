# US 5.1.17 - As a Doctor, I want to update an operation requisition, so that the Patient has access to the necessary healthcare

## Requirements Engineering

### Customer Specifications and Clarifications
**From the specifications document:**
**From the client's clarifications:**

> **Question:** [...] The doctor cannot change the Patient ID but can change the Priority. Besides the Priority, could the doctor also update the Deadline Date or Operation Type?
>
> **Answer:** [...] The doctor can change the deadline, the priority, and the description. The doctor cannot change the operation type nor the patient.

> **Question:** You want to log all updates to the operation request. Do you plan to have this info available in the app or is this just for audit purposes?
>
> **Answer:** The history of the operation type definition is part of the application's data. If the user needs to view the details of an operation that was performed last year, they need to be able to see the operation configuration that was in place at that time.

### Acceptance Criteria
- Doctors can update operation requests they created (e.g., change the deadline or priority).
- The system checks that only the requesting doctor can update the operation request.
- The system logs all updates to the operation request (e.g., changes to priority or deadline).
- Updated requests are reflected immediately in the system and notify the Planning Module of
  any changes.


### Found Out Dependencies
