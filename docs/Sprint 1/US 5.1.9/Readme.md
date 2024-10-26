# US 5.1.9 - As an Admin, I want to edit an existing patient profile, so that I can update their information when needed.

## Requirements Engineering

### Customer Specifications and Clarifications
**From the specifications document:**
**From the client's clarifications:**

> **Question:** When one of the contents that administrator edits is a sensitive content (eg. email), the notification is sent for what patient's email, the email in patient account, the old email of patient or the new email of patient?
>
> **Answer:** If the email is changed, the notification should be sent to the "old" email.

> **Question:** In one of the questions asked previously, the customer responded that the Admin, with us 5.1.8, can only enter the first name, last name, date of birth and contact information.
However, in the specifications, in the US 5.1.9 acceptance criteria it is written the following: "Editable fields include name, contact information, medical history, and allergies.". Therefore, shouldn't this acceptance criterion be just the parameters he himself entered?
>
> **Answer:** Yes. must consider as acceptance criteria: Editable fields are first name, last name, date of birth and contact information.

> **Question:** In this US an admin can edit a user profile. Does the system display a list of all users or the admin searches by ID? Or both.
>
> **Answer:** This requirement is for the editing of the user profile. from a usability point of view, the user should be able to start this feature either by searching for a specific user or listing all users and selecting one. 
> Note that we are not doing the user interface of the system in this sprint.


> **Question:**
>
> **Answer:**

> **Question:**
>
> **Answer:**

> **Question:**
>
> **Answer:**

### Acceptance Criteria
- Admins can search for and select a patient profile to edit.
- Editable fields include name, contact information and date of birth.
- Changes to sensitive data (e.g., contact information) trigger an email notification to the patient.
- The system logs all profile changes for auditing purposes.

### Found Out Dependencies
* US 5.1.6
* US 5.1.8