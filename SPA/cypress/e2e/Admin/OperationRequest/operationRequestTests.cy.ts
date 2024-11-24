import json from '../../../globalVariables.json';

describe('Operation Request Tests', () => {

  beforeEach(() => {
    cy.clearCookies();
    cy.setCookie('.AspNetCore.Cookies', json.Cookies.Doctor);
    cy.visit('/doctor/operationRequests');
  });

  it('Visits the Operation Type Management Component', () => {
    cy.contains('h2', 'Search Operation Requisitions');
  });

  it('Checks the number of Operation Requests displayed', () => {
    cy.get('form button[type="submit"]').click();
    cy.get('.results table tbody tr').should('have.lengthOf.at.least', 1);
  });

  it('Checks the number of Operation Requests displayed with patient name', () => {
    cy.get('#patientName').type('Alice Smith');
    cy.get('form button[type="submit"]').click();
    cy.get('.results table tbody tr').should('have.lengthOf.at.least', 1);
  });

  it('Checks the number of Operation Requests displayed with operation type', () => {
    cy.get('#operationType').type('Brain Surgery');
    cy.get('form button[type="submit"]').click();
    cy.get('.results table tbody tr').should('have.lengthOf.at.least', 1);
  });

  it('Checks the number of Operation Requests displayed with a certain priority', () => {
    cy.get('#priority').type('LOW');
    cy.get('form button[type="submit"]').click();
    cy.get('.results table tbody tr').should('have.lengthOf.at.least', 1);
  });

  it('Checks the number of Operation Requests displayed with a specific status', () => {
    cy.get('#status').type('PENDING');
    cy.get('form button[type="submit"]').click();
    cy.get('.results table tbody tr').should('have.lengthOf.at.least', 1);
  });

  it('Deletes the last Operation Request', () => {
    cy.visit('/doctor/operationRequests');
    cy.get('form button[type="submit"]').click();
    cy.get('.results table tbody tr').then(($rows) => {
      const initialRowCount = $rows.length;
      if (initialRowCount > 0) {
        // Selects the Delete button in the last row
        cy.get('.results table tbody tr:last-child td button')
            .contains('Delete')
            .click();

        cy.get('.results table tbody tr').should('have.length', initialRowCount - 1);
      } else {
        cy.log('No rows to delete');
      }
    });
  });
});
