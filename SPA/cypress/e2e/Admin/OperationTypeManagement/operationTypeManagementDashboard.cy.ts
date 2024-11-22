import json from '../../../globalVariables.json'

describe("Open Operation Type Management Component", () => {
  it("Visits the Operation Type Management Component", () => {
    cy.setCookie('.AspNetCore.Cookies',json.Cookies.Admin)
    cy.visit("/admin/operationTypeManagement");
    cy.contains("h1", "Operation Type Management");
  });
})

describe("Check Number of Operation Types Displayed", () => {
  it("Check the number of Operation Types", () => {
    cy.setCookie('.AspNetCore.Cookies',json.Cookies.Admin)
    cy.visit("/admin/operationTypeManagement");
    cy.get("table tbody tr").should("have.length", 3);
  });
})
