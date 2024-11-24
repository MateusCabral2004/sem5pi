import json from '../../../globalVariables.json';

describe("Patient Profile Management", ()=>{

  beforeEach(() => {
  cy.setCookie('.AspNetCore.Cookies', json.Cookies.Admin);
  cy.visit('/admin/patient');
  cy.reload();
  cy.wait(1000);
});
  
  it("Visits the Patient Profile Management Component", () => {
    cy.contains("h1", "Patient Profiles Management");
  });

    describe("Filter Patient Profiles", () => {
      it("Filter by Name", () => {

        const name = 'new User';
        cy.get('.filter-input').click();
        cy.get('.filter-options > :nth-child(1)').click();
        cy.get('#name-input').type(name);
        cy.get('.modal-actions > :nth-child(1)').click();
        cy.get('.patient-profile-list .patient-profile-item').should('have.length', 1);

      });

      it("Filter by Email", () => {
        const email = 'anotherEmail@gmail.com';
        cy.get('.filter-input').click();
        cy.get('.filter-options > :nth-child(2)').click();
        cy.get('#name-input').type(email);
        cy.get('.modal-actions > :nth-child(1)').click();

        cy.get('.patient-profile-list .patient-profile-item').should('have.length.at.least', 1);
      });

      it("Filter by Medical Record Number", () => {
        const mRNumber = '20241100005';
        cy.get('.filter-input').click();
        cy.get('.filter-options > :nth-child(3)').click();
        cy.get('#name-input').type(mRNumber);
        cy.get('.modal-actions > :nth-child(1)').click();

        cy.get('.patient-profile-list .patient-profile-item').should('have.length.at.least', 1);
      });

      it("Filter by Birth Date", () => {
      const birthDate = '2009-12-12';
      cy.get('.filter-input').click();
      cy.get('.filter-options > :nth-child(3)').click();
      cy.get('#name-input').type(birthDate);
      cy.get('.modal-actions > :nth-child(1)').click();

      cy.get('.patient-profile-list .patient-profile-item').should('have.length.at.least', 1);
    });
    });

  it("Deletes a Patient Profile", () => {
    cy.get(".patient-profile-list .patient-profile-item").then(($items) => {
      const initialSize = $items.length;

      if (initialSize > 0) {
        cy.get(".patient-profile-list .patient-profile-item:last-child .delete-button").click();
        cy.get('.modal-actions > :nth-child(1)').click();

        cy.wait(1000);
        cy.reload();

        cy.get(".patient-profile-list .patient-profile-item")
          .should("have.length", (initialSize - 1));
      } else {
        cy.log("No patient profiles to delete.");
      }
    });
  });

  it("Adds a Patient Profile", () => {
    cy.get(".patient-profile-list .patient-profile-item").then(($items) => {
      const initialSize = $items.length;

      cy.get(".add-button").click();

      const firstName="Carlos";
      const lastName="Joseph";
      const birthDate='10-10-2004';
      const email='carlinhosbombeiro@gmail.com';
      const phoneNumber='987654321';
      const gender="Alpha male"
      const emergencyContact='911';

      cy.get("#firstName").type(firstName);
      cy.get("#lastName").type(lastName);
      cy.get("#birthDate").type(birthDate);
      cy.get("#email").type(email);
      cy.get("#phoneNumber").type(phoneNumber);
      cy.get("#gender").type(gender);
      cy.get("#emergencyContact").type(emergencyContact);
      cy.get(".inline-fields > button").click();
      cy.get("[type='submit']").click();

      cy.wait(1000);
      cy.visit('/admin/patient');
      cy.reload();

      cy.log(initialSize + "");
      cy.get(".patient-profile-list .patient-profile-item")
        .should("have.length", (initialSize + 1));
    });
  });

  it("Edits the First Name of a Patient", () => {
    const currentFirstName = "Carlos"; // Nome atual do paciente
    const newFirstName = "Carlos-Updated"; // Novo nome para atualização

    cy.get(".patient-profile-list .patient-profile-item").contains(currentFirstName).then(($patient) => {
      cy.wrap($patient).find(".edit-button").click();

      cy.get("#firstName").clear().type(newFirstName);

      cy.get("[type='submit']").click();

      cy.wait(1000);

      cy.visit('/admin/patient');
      cy.reload();

      cy.get(".patient-profile-list .patient-profile-item")
        .contains(newFirstName)
        .should("exist");
    });
  });

  describe("Filter Patient Profiles Invalids", () => {
    it("Filter by Name", () => {

      const name = 'jojo todynho';
      cy.get('.filter-input').click();
      cy.get('.filter-options > :nth-child(1)').click();
      cy.get('#name-input').type(name);
      cy.get('.modal-actions > :nth-child(1)').click();
      cy.get('.patient-profile-list .patient-profile-item').should('have.length', 0);

    });

    it("Filter by Email", () => {
      const email = 'lololole@gmail.com';
      cy.get('.filter-input').click();
      cy.get('.filter-options > :nth-child(2)').click();
      cy.get('#name-input').type(email);
      cy.get('.modal-actions > :nth-child(1)').click();
      cy.get('.patient-profile-list .patient-profile-item').should('have.length.at.least', 0);
    });

    it("Filter by Birth Date", () => {
      const birthDate = '2000-11-11';
      cy.get('.filter-input').click();
      cy.get('.filter-options > :nth-child(3)').click();
      cy.get('#name-input').type(birthDate);
      cy.get('.modal-actions > :nth-child(1)').click();
      cy.get('.patient-profile-list .patient-profile-item').should('have.length.at.least', 0);
    });

    it("Filter by Medical Record Number", () => {
      const mRNumber = '20231100005';
      cy.get('.filter-input').click();
      cy.get('.filter-options > :nth-child(3)').click();
      cy.get('#name-input').type(mRNumber);
      cy.get('.modal-actions > :nth-child(1)').click();
      cy.get('.patient-profile-list .patient-profile-item').should('have.length.at.least', 0);
    });
  });
})
