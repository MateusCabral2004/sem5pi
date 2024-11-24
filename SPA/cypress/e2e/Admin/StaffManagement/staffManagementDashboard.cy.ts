import json from '../../../globalVariables.json';

beforeEach(() => {
  cy.clearCookies();   // Limpar cookies
  cy.clearLocalStorage();  // Limpar o localStorage
  cy.setCookie('.AspNetCore.Cookies', json.Cookies.Admin);  // Recarregar os cookies necessários
  cy.visit('/admin/staff');
  cy.wait(1000);  // Aguarde um pouco para garantir que a página carregue
});

describe("Staff Management", () => {

  it("Check the number of Staffs displayed", () => {
    cy.intercept('GET', '/Staff').as('getStaffs');

    cy.reload();

    cy.wait('@getStaffs', { timeout: 10000 }).then((interception) => {
      const staffsCount = interception.response?.body.length || 0;

      cy.get(".staff-profile-list", { timeout: 10000 })
        .find(".staff-profile-item")
        .and('have.length', staffsCount);
    });
  });


  describe("Filter Staffs", () => {
    it("Filter By Name Valid Case", () => {
      const name = "Rui Soares";
      cy.get('.filter-input').click();
      cy.get('.filter-options > :nth-child(1)').click();
      cy.get('#name-input').type(name);
      cy.get('.modal-actions > :nth-child(1)').click();
      cy.get('.staff-profile-list .staff-profile-item').should('have.length.at.least', 1);
    });

    it("Filter By Name Not Found Case", () => {
      const name = "Not Found Name";
      cy.get('.filter-input').click();
      cy.get('.filter-options > :nth-child(1)').click();
      cy.get('#name-input').type(name);
      cy.get('.modal-actions > :nth-child(1)').click();
      cy.get('.staff-profile-list .staff-profile-item').should('have.length', 0);
      cy.contains(".error-message", "There are no staff profiles to list.");
    });

    it("Filter By Name Invalid Case", () => {
      cy.get('.filter-input').click();
      cy.get('.filter-options > :nth-child(1)').click();
      cy.get('.modal-actions > :nth-child(1)').click();
      cy.get('.staff-profile-list .staff-profile-item').should('have.length', 0);
      cy.contains(".error-message", "Invalid Name.");
    });

    it("Filter By Email Valid Case", () => {
      const email = "rpsoares8@gmail.com";
      cy.get('.filter-input').click();
      cy.get('.filter-options > :nth-child(2)').click();
      cy.get('#name-input').type(email);
      cy.get('.modal-actions > :nth-child(1)').click();
      cy.get('.staff-profile-list .staff-profile-item').should('have.length', 1);
    });

    it("Filter By Email Not Found Case", () => {
      const email = "notfoundemail@gmail.com";
      cy.get('.filter-input').click();
      cy.get('.filter-options > :nth-child(2)').click();
      cy.get('#name-input').type(email);
      cy.get('.modal-actions > :nth-child(1)').click();
      cy.get('.staff-profile-list .staff-profile-item').should('have.length', 0);
      cy.contains(".error-message", "There are no staff profiles to list.");
    });

    it("Filter By Email Invalid Case Empty Space", () => {
      cy.get('.filter-input').click();
      cy.get('.filter-options > :nth-child(2)').click();
      cy.get('.modal-actions > :nth-child(1)').click();
      cy.get('.staff-profile-list .staff-profile-item').should('have.length', 0);
      cy.contains(".error-message", "Invalid Email.");
    });

    it("Filter By Email Invalid Case Invalid Email Format", () => {
      const email = "Not Found Email";
      cy.get('.filter-input').click();
      cy.get('.filter-options > :nth-child(2)').click();
      cy.get('#name-input').type(email);
      cy.get('.modal-actions > :nth-child(1)').click();
      cy.get('.staff-profile-list .staff-profile-item').should('have.length', 0);
      cy.contains(".error-message", "Invalid Email.");
    });

    it("Filter By Specialization Valid Case", () => {
      const specialization = "Nurse";
      cy.get('.filter-input').click();
      cy.get('.filter-options > :nth-child(3)').click();
      cy.get('#name-input').type(specialization);
      cy.get('.modal-actions > :nth-child(1)').click();
      cy.get('.staff-profile-list .staff-profile-item').should('have.length.at.least', 1);
    });

    it("Filter By Specialization Not Found Case", () => {
      const specialization = "Not Found Specialization";
      cy.get('.filter-input').click();
      cy.get('.filter-options > :nth-child(3)').click();
      cy.get('#name-input').type(specialization);
      cy.get('.modal-actions > :nth-child(1)').click();
      cy.get('.staff-profile-list .staff-profile-item').should('have.length', 0);
      cy.contains(".error-message", "Specialization not found.");
    });

    it("Filter By Specialization Staffs Not Found Case", () => {
      const specialization = "Anesthesiologist";
      cy.get('.filter-input').click();
      cy.get('.filter-options > :nth-child(3)').click();
      cy.get('#name-input').type(specialization);
      cy.get('.modal-actions > :nth-child(1)').click();
      cy.get('.staff-profile-list .staff-profile-item').should('have.length', 0);
      cy.contains(".error-message", "There are no staff profiles to list.");
    });

    it("Filter By Specialization Invalid Case", () => {
      cy.get('.filter-input').click();
      cy.get('.filter-options > :nth-child(3)').click();
      cy.get('.modal-actions > :nth-child(1)').click();
      cy.get('.staff-profile-list .staff-profile-item').should('have.length', 0);
      cy.contains(".error-message", "Invalid Specialization.");
    });

  });

   describe("Delete Staff Profile", () => {
    it("Delete a Staff Profile", () => {
      cy.get(".staff-profile-list .staff-profile-item").then(($items) => {
        const initialSize = $items.length;

        if (initialSize > 0) {
          cy.get(":nth-child(1) > .staff-profile-actions > app-delete-staff-profile-button > .delete-button").click();
          cy.get('.modal-actions > :nth-child(1)').click();

          cy.wait(1000);
          cy.reload();

          cy.get(".staff-profile-list .staff-profile-item")
            .should("have.length", (initialSize - 1));
        } else {
          cy.log("No operation types to delete.");
        }
      });
    });
  });

  describe("Edit Staff Profile", () => {
    it("Edit a Staff Profile", () => {
      cy.get(":nth-child(1) > .staff-profile-actions > app-edit-staff-profile-button > .edit-button").click();
      const newSpecialization = "New Specialization Test";
      cy.get("#specialization").clear().type(newSpecialization);
      cy.get("button").click();

      cy.get(":nth-child(1) > app-staff-details > .staff-profile-details > :nth-child(3)")
        .invoke('text')
        .then((text) => {
          expect(text).contains(newSpecialization);
        });
    });

  });


  describe("Edit Staff Profile Phone Number Already In Use", () => {
    it("Edit a Staff Profile", () => {
      cy.get(":nth-child(1) > .staff-profile-actions > app-edit-staff-profile-button > .edit-button").click();
      const phoneNumber = 964666298;
      cy.get("#phoneNumber").clear().type(`${phoneNumber}`);
      cy.get("button").click();

      cy.wait(1000);

      cy.get('p').contains('Error: Phone Number already in use.');

    });

  });

  describe("Edit Staff Profile Invalid Phone Number", () => {
    it("Edit a Staff Profile", () => {
      cy.get(":nth-child(1) > .staff-profile-actions > app-edit-staff-profile-button > .edit-button").click();
      const phoneNumber = 987;
      cy.get("#phoneNumber").clear().type(`${phoneNumber}`);
      cy.get("button").click();

      cy.wait(1000);

      cy.get('p').contains('Error: Phone number must be 9 digits long.');

    });

  });

    it("Create a Staff Profile", () => {

      cy.get(".add-button").click();

      const firstName = "Paulo";
      const lastName = "Soares";
      const email = "paulossoares@gmail.com";
      const specialization = "Nurse";
      const licenseNumber = 123426;
      const phoneNumber = 902020202;

      cy.get("#firstName").clear().type(firstName);
      cy.get("#lastName").clear().type(lastName);
      cy.get("#email").clear().type(email);
      cy.get("#specialization").clear().type(specialization);
      cy.get("#licenseNumber").clear().type(`${licenseNumber}`);
      cy.get("#phoneNumber").clear().type(`${phoneNumber}`);

      cy.get("button").should('not.be.disabled').click();

      cy.wait(1000);

    });

    it("Create a Staff Profile Duplicate License Number", () => {

      cy.get(".add-button").click({force: true});

      const firstName = "Paulo";
      const lastName = "Soares";
      const email = "paulossoaresssss@gmail.com";
      const specialization = "Nurse";
      const licenseNumber = 201;
      const phoneNumber = 100000000;

      cy.get("#firstName").clear().type(firstName);
      cy.get("#lastName").clear().type(lastName);
      cy.get("#email").clear().type(email);
      cy.get("#specialization").clear().type(specialization);
      cy.get("#licenseNumber").clear().type(`${licenseNumber}`);
      cy.get("#phoneNumber").clear().type(`${phoneNumber}`);

      cy.get("button").click();

      cy.wait(1000);

      cy.get('p').contains('Error: License Number already in use.');
    });

    it("Create a Staff Profile Invalid Email Format", () => {

      cy.get(".add-button").click({force: true});

      const firstName = "Paulo";
      const lastName = "Soares";
      const email = "paulossoaresssss";
      const specialization = "Nurse";
      const licenseNumber = 123427;
      const phoneNumber = 100000000;

      cy.get("#firstName").clear().type(firstName);
      cy.get("#lastName").clear().type(lastName);
      cy.get("#email").clear().type(email);
      cy.get("#specialization").clear().type(specialization);
      cy.get("#licenseNumber").clear().type(`${licenseNumber}`);
      cy.get("#phoneNumber").clear().type(`${phoneNumber}`);

      cy.get("button").click();

      cy.wait(1000);

      cy.get('p').contains('Error: Email address must contain an @ symbol.');
    });

    it("Create a Staff Profile Duplicate Email", () => {

      cy.get(".add-button").click({force: true});

      const firstName = "Paulo";
      const lastName = "Soares";
      const email = "rpsoares8@gmail.com";
      const specialization = "Nurse";
      const licenseNumber = 123427;
      const phoneNumber = 100000000;

      cy.get("#firstName").clear().type(firstName);
      cy.get("#lastName").clear().type(lastName);
      cy.get("#email").clear().type(email);
      cy.get("#specialization").clear().type(specialization);
      cy.get("#licenseNumber").clear().type(`${licenseNumber}`);
      cy.get("#phoneNumber").clear().type(`${phoneNumber}`);

      cy.get("button").click();

      cy.wait(1000);

      cy.get('p').contains('Error: Email already in use.');
    });

    it("Create a Staff Profile Duplicate Phone Number", () => {

      cy.get(".add-button").click({force: true});

      const firstName = "Paulo";
      const lastName = "Soares";
      const email = "paulossoaresssssss@gmail.com";
      const specialization = "Nurse";
      const licenseNumber = 123427;
      const phoneNumber = 964666298;

      cy.get("#firstName").clear().type(firstName);
      cy.get("#lastName").clear().type(lastName);
      cy.get("#email").clear().type(email);
      cy.get("#specialization").clear().type(specialization);
      cy.get("#licenseNumber").clear().type(`${licenseNumber}`);
      cy.get("#phoneNumber").clear().type(`${phoneNumber}`);

      cy.get("button").click();

      cy.wait(1000);

      cy.get('p').contains('Error: Phone Number already in use.');

    });

    it("Create a Staff Profile Invalid Phone Number Format", () => {

      cy.get(".add-button").click({force: true});

      const firstName = "Paulo";
      const lastName = "Soares";
      const email = "paulossoaresssssss@gmail.com";
      const specialization = "Nurse";
      const licenseNumber = 123427;
      const phoneNumber = 9;

      cy.get("#firstName").clear().type(firstName);
      cy.get("#lastName").clear().type(lastName);
      cy.get("#email").clear().type(email);
      cy.get("#specialization").clear().type(specialization);
      cy.get("#licenseNumber").clear().type(`${licenseNumber}`);
      cy.get("#phoneNumber").clear().type(`${phoneNumber}`);

      cy.get("button").click();

      cy.wait(1000);

      cy.get('p').contains('Error: Phone number must be 9 digits long.');

    });
});
