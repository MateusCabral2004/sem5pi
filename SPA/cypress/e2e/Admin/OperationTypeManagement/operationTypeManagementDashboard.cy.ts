import json from '../../../globalVariables.json';

beforeEach(() => {
  cy.setCookie('.AspNetCore.Cookies', json.Cookies.Admin);
  cy.visit('/admin/operationTypeManagement');
  cy.wait(1000);
});

describe("Operation Type Management", () => {

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
      cy.reload();

      cy.log(initialSize + "");
      cy.get(".operation-list .operation-item")
        .should("have.length", (initialSize + 1));
    });
  });

  it("Edits an Operation Name", () => {
    cy.get(".operation-list .operation-item:first").within(() => {
      cy.get(".operation-details p:first-of-type")
        .invoke("text")
        .then((text) => {
          const oldOperationName = text.trim();
          cy.wrap(oldOperationName).as("oldOperationName");
        });
      cy.get(".edit-button").click();
    });

    const newOperationName = "Edited OP " + new Date().toLocaleTimeString([], {
      hour: '2-digit',
      minute: '2-digit',
      second: '2-digit'
    }).replace(/:/g, "");

    cy.get("#operationName").clear().type(newOperationName);
    cy.get("[type='submit']").click();

    cy.wait(500);
    cy.reload();

    cy.get("@oldOperationName").then((oldName) => {
      cy.get(".operation-list .operation-item").each(($item) => {
        cy.wrap($item).within(() => {
          cy.get(".operation-details p:first-of-type").should("not.contain", oldName);
        });
      });

      cy.get(".operation-list .operation-item:first").within(() => {
        cy.get(".operation-details p:first-of-type").should("contain", newOperationName);
      });
    });
  });

  describe("Edit Operation Durations", () => {
    const editDuration = (durationSelector: string, validationIndex: number) => {
      // Generate a dynamic duration in HH:mm format based on the current time
      const newDuration = new Date().toLocaleTimeString([], {hour: '2-digit', minute: '2-digit'});

      // Click edit on the first operation item
      cy.get(".operation-list .operation-item:first").within(() => {
        cy.get(".edit-button").click();
      });

      // Edit the duration
      cy.get(durationSelector).clear().type(newDuration);
      cy.get("[type='submit']").click();

      // Wait for UI update and reload the page
      cy.wait(500);
      cy.reload();

      // Validate the updated duration
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
});
