import json from '../../../globalVariables.json';

beforeEach(() => {
  cy.setCookie('.AspNetCore.Cookies', json.Cookies.Admin);
  cy.visit('/admin/operationTypeManagement');
  cy.wait(1000);
});

describe("View Operation Details", () => {
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


