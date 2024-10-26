# US 5.1.19 - As a doctor, I want to list/search operation requisitions so that I can view the details, edit, and remove operation requisitions.

## Requirements Engineering

### Customer Specifications and Clarifications
**From the specifications document:**
- Doctors should be able to search for operation requisitions by patient name, operation type, priority, and status.
- The system should display a list of operation requisitions in a searchable and filterable view.
- Each entry in the list should include details of the operation requisition (e.g., patient name, operation type, status).
- Doctors should be able to select an operation requisition to view, update, or delete it.

## Additional Functional Requirements for Search and Display

### 1. Filter and Sorting Options for Search Results

- **Filtering:** Requisitions should be filterable based on the following specific criteria:
    - **Patient Name:** Allow filtering by patient name.
    - **Operation Type:** Allow filtering by operation type.
    - **Priority:** Allow filtering by requisition priority.
    - **Status:** Allow filtering by requisition status.

### 2. Visual Indicators for Requisition Status
- **Status Indicators:** The interface should visually indicate the status of each requisition using:
    - **Color Codes** or **Icons** to represent different statuses:
        - **Pending:** Indicated by a specific color or icon.
        - **Scheduled:** Shown with a unique color or icon.
        - **Completed:** Marked with a final color or icon to denote completion.

### Acceptance Criteria
- Doctors can search for operation requisitions by patient name, operation type, priority, and status.
- The system displays a list of operation requisitions in a searchable and filterable format.
- Each entry in the list includes operation requisition details, such as patient name, operation type, and status.
- Doctors can select an operation requisition to view its details, update it, or delete it.

### Dependencies
- The system should provide visual indicators for the status of operation requisitions.
