# US 5.1.16 - As a Doctor, I want to request an operation, so that the Patient has access to the necessary healthcare

## Requirements Engineering

### Customer Specifications and Clarifications
**From the specifications document:**
**From the client's clarifications:**

> **Question:** Does the system adds automatically the operation request to the medical history of the patient?
>
> **Answer:** No need. It will be the doctor's responsibility to add it.

> **Question:** Can a doctor make more than one operation request for the same patient? If so, is there any limit or rules to follow? For example, doctors can make another operation request for the same patient as long as it's not the same operation type?
>
> **Answer:** It should not be possible to have more than one "open" surgery request (that is, a surgery that is requested or scheduled but not yet performed) for the same patient and operation type.

### Acceptance Criteria
- Doctors can create an operation request by selecting the patient, operation type, priority, and
  suggested deadline.
- The system validates that the operation type matches the doctor’s specialization.
- The operation request includes: Patient ID, Doctor ID, Operation Type, Deadline, Priority
- The system confirms successful submission of the operation request and logs the request in
  the patient’s medical history.

### Found Out Dependencies
