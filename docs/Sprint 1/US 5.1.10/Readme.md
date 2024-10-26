# US 5.1.10 - As an Admin, I want to delete a patient profile, so that I can remove patients who are no longer under care

## Requirements Engineering

### Customer Specifications and Clarifications
**From the specifications document:**
**From the client's clarifications:**

> **Question:** When generating the audit record to log the deletion of patient profiles what patient information (if any) are we allowed to keep in the log for identification purposes? If none are the logs then only a record of deletion operations and not actually tied to the deletion of a specific patient?
>
> **Answer:** The ERS (health regulator) issued an opinion on the retention of health data in which it established a minimum retention period of 5 years, after which the data can be deleted.
You may wish to keep some of the information anonymised for statistical purposes only, limiting yourself to, for example, gender and type of surgery.

> **Question:**
>
> **Answer:**

> **Question:**
>
> **Answer:**

### Acceptance Criteria
- Admins can search for a patient profile and mark it for deletion.
- Before deletion, the system prompts the admin to confirm the action.
- Once deleted, all patient data is permanently removed from the system within a predefined
  time frame.
- The system logs the deletion for audit and GDPR compliance purposes.

### Found Out Dependencies
* US 5.1.6
* US 5.1.8
