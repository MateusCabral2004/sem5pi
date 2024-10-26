# US 5.1.18 - As a doctor, I want to remove an operation requisition so that healthcare activities can be provided as needed.

## Requirements Engineering

### Customer Specifications and Clarifications
**From the specifications document:**
- Doctors should be able to delete operation requisitions that have not yet been scheduled.
- The deletion of the operation requisition must be irreversible, completely removing it from the patient's medical record.
- Removing a requisition should trigger a notification to the Planning Module, adjusting any schedules that depend on this operation.

# Critérios para o Sistema de Requisição de Operações

## 1. Deleção de Requisições de Operação
- **Critério:** A deleção de uma requisição de operação deve ser irreversível.
- **Especificação:** O sistema deve apresentar um prompt de confirmação antes da exclusão, e a requisição deve ser removida permanentemente após a confirmação.

## 2. Autorização para Deletar Requisições
- **Critério:** Somente o médico que criou a requisição pode excluí-la.
- **Especificação:** A deleção só pode ocorrer se a operação correspondente ainda não tiver sido agendada.


### Acceptance Criteria
- Doctors can only delete the operation requisitions they created, provided the operation has not yet been scheduled.
- Before deletion, the system displays a confirmation prompt to the doctor.
- Once deleted, the requisition is removed from the patient's medical record and cannot be recovered.
- The system sends a notification to the Planning Module, updating dependent schedules for this requisition.

### Found Out Dependencies
- **Scheduling Module:** Check the operation status to prevent the deletion of already scheduled requisitions.
- **Notification Module:** Send notifications to the Planning Module after deletion.
- **Access Control Module:** Ensure that only the doctor who created the requisition can delete it.
