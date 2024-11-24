import json from '../../../globalVariables.json';

describe("Operation Type Management", () => {

  beforeEach(() => {
    cy.clearCookies();
    cy.clearLocalStorage();
    cy.setCookie('.AspNetCore.Cookies', json.Cookies.Admin);
    cy.reload();
    cy.wait(1000);
    cy.visit('/admin/operationTypeManagement');
  });

  it("Visits the Operation Type Management Component", () => {
    cy.contains("h1", "Operation Type Management");
  });

  it("Check the number of Operation Types displayed", () => {
    cy.intercept('GET', '/operationType/listOperationType').as('getOperationTypes');

    cy.reload();
    cy.wait('@getOperationTypes').then((interception) => {
      const operationTypesCount = interception.response?.body.length || 0;

      cy.get(".operation-list .operation-item")
        .should("have.length", operationTypesCount);
    });
  });

  it("View Operation Type Details", () => {
    cy.get(".operation-list .operation-item:first").within(() => {
      cy.get(".operation-details p:first-of-type")
        .invoke("text")
        .then((text) => {
          const operationName = text.trim().replace(/^Name:\s*/, '');
          cy.wrap(operationName).as("operationName");
          cy.get(".view-button").click();
        });
    });

    cy.get('.form-group label:contains("Operation Name")').siblings('p')
      .invoke('text')
      .then((operationName) => {
        cy.log("Operation Name:", operationName.trim());

        cy.get("@operationName").should("eq", operationName.trim());
      });

  });

  it("Deletes an Operation Type", () => {
    cy.get(".operation-list .operation-item").then(($items) => {
      const initialSize = $items.length;

      if (initialSize > 0) {
        cy.get(".operation-list .operation-item:last-child .delete-button").click();
        cy.get('.modal-actions > :nth-child(1)').click();

        cy.wait(1000);
        cy.reload();

        cy.get(".operation-list .operation-item")
          .should("have.length", (initialSize - 1));
      } else {
        cy.log("No operation types to delete.");
      }
    });
  });

  it("Adds a new Operation Type", () => {
    cy.get(".operation-list .operation-item").then(($items) => {
      const initialSize = $items.length;

      cy.get(".add-button").click();
      const time = new Date().toLocaleTimeString([], {
        hour: '2-digit',
        minute: '2-digit',
        second: '2-digit'
      }).replace(/:/g, "");
      const newOperationName = "OP " + time;

      cy.get("#operationName").type(newOperationName);
      cy.get("#setupDuration").type("00:30");
      cy.get("#surgeryDuration").type("02:00");
      cy.get("#cleanupDuration").type("00:30");
      cy.get("#specialization").type("Test Specialization");
      cy.get("#numberOfStaff").type("2");
      cy.get(".inline-fields > button").click();
      cy.get("[type='submit']").click();

      cy.wait(1000);
      cy.visit('/admin/operationTypeManagement');
      cy.reload();

      cy.log(initialSize + "");
      cy.get(".operation-list .operation-item")
        .should("have.length", (initialSize + 1));
    });
  });

  describe("Edit Operation Durations", () => {
    const editDuration = (durationSelector: string, validationIndex: number) => {
      const newDuration = new Date().toLocaleTimeString([], {hour: '2-digit', minute: '2-digit'});

      cy.get(".operation-list .operation-item:first").within(() => {
        cy.get(".edit-button").click();
      });

      cy.get(durationSelector).clear().type(newDuration);
      cy.get("[type='submit']").click();

      cy.wait(500);
      cy.reload();

      cy.get(".operation-list .operation-item:first").within(() => {
        cy.get(`.operation-details p:nth-of-type(${validationIndex})`)
          .invoke("text")
          .should("contain", newDuration);
      });
    };

    it("Edits Setup Duration", () => {
      editDuration("#setupDuration", 2);
    });

    it("Edits Surgery Duration", () => {
      editDuration("#surgeryDuration", 3);
    });

    it("Edits Cleanup Duration", () => {
      editDuration("#cleanupDuration", 4);
    });
  });

  describe("Edit Required Staff", () => {
    it("Add required staff", () => {
      const specialization = "test doctor " + new Date().toLocaleTimeString([], {
        hour: '2-digit',
        minute: '2-digit',
        second: '2-digit'
      });
      const numberOfStaff = 5;

      cy.get(".operation-list .operation-item:first").within(() => {
        cy.get(".operation-details p:first-of-type");

        cy.get(".edit-button").click();
      });

      cy.get("#specialization").type(specialization);
      cy.get("#numberOfStaff").clear().type(numberOfStaff.toString());
      cy.get(".inline-fields > button").click();
      cy.get("[type='submit']").click();

      cy.wait(1000);
      cy.reload();

      cy.get(".operation-list .operation-item:first").within(() => {
        cy.get(".operation-details p:first-of-type");

        cy.get(".view-button").click();
      });

      cy.get("ul").last().then(($lastitem) => {
        cy.wrap($lastitem).should("contain", specialization);
        cy.wrap($lastitem).should("contain", numberOfStaff);
      });
    });

    it("Remove required staff", () => {

      cy.get(".operation-list .operation-item:first").within(() => {
        cy.get(".operation-details p:first-of-type");

        cy.get(".edit-button").click();
      });

      cy.get("ul li:last-child").then(($lastitem) => {
        cy.wrap($lastitem.text().trim()).as("lastItemText");
        cy.wrap($lastitem).find(".remove-btn").click();
      });

      cy.get('[type="submit"]').click();

      cy.log(`Captured last item's text: @lastItemText`);

      cy.wait(1000);
      cy.reload();

      cy.get(".operation-list .operation-item:first").within(() => {
        cy.get(".operation-details p:first-of-type");
        cy.get(".view-button").click();
      });

      cy.get("@lastItemText").then((lastItemText) => {
        cy.get("ul").should("not.contain", lastItemText);
      });
    });
  });

  describe("Filter Operation Types", () => {
    it("Filter by Name", () => {

      const name = 'Heart Surgery';
      cy.get('.filter-input').click();
      cy.get('.filter-options > :nth-child(1)').click();
      cy.get('#name-input').type(name);
      cy.get('.modal-actions > :nth-child(1)').click();
      cy.get('.operation-list .operation-item').should('have.length', 1);

    });

    it("Filter by Specialization", () => {
      const name = 'Cleaner';
      cy.get('.filter-input').click();
      cy.get('.filter-options > :nth-child(2)').click();
      cy.get('#name-input').type(name);
      cy.get('.modal-actions > :nth-child(1)').click();

      cy.get('.operation-list .operation-item').should('have.length.at.least', 1);
    });

    it("Filter by Number of Staff", () => {
      cy.get('.filter-input').click();
      cy.get('.filter-options > :nth-child(3)').click();
      cy.get('#name-input').type('false');
      cy.get('.modal-actions > :nth-child(1)').click();

      cy.get('.operation-list .operation-item').should('have.length.at.least', 1);

    });
  });
});
