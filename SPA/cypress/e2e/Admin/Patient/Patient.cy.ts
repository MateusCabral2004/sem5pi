import json from '../../../globalVariables.json';

describe('Register a new patient', () => {

  beforeEach(() => {
    cy.clearCookies();
    cy.setCookie('.AspNetCore.Cookies', json.Cookies.RegisteredPatient);
  });

  it('should register a new patient', () => {
    cy.clearCookies();
    cy.setCookie('.AspNetCore.Cookies', json.Cookies.UnregisteredPatient);
    cy.visit('/unregistered');
    cy.get('form.ng-untouched > div > .ng-untouched').type('938536401');
    cy.get('button').click();
  });

  it('should access the patient account', () => {
    cy.log('Accept the email or this test will fail');
    cy.wait(5000)
    cy.reload();
    cy.visit('/patient');
    cy.contains('Patient Dashboard');
  });

  describe('Update patient account', () => {
    it('should update the patient account email', () => {
      cy.visit('/patient/updateAccount');
      cy.get('form.ng-untouched > :nth-child(1) > .ng-untouched').type('mat@gmail.com');
      cy.get('button').click();
      cy.visit('/patient');
      cy.log('Should receive the email to confirm the changes');
    });

    it('should update the patient account phone number', () => {
      cy.visit('/patient/updateAccount');
      cy.get('form.ng-untouched > :nth-child(2) > .ng-untouched').type('938536402');
      cy.get('button').click();
      cy.visit('/patient');
      cy.log('Should receive the phone number to confirm the changes');
    });

    it('should update the patient first name', () => {
      cy.visit('/patient/updateAccount');
      cy.get('form.ng-untouched > :nth-child(3) > .ng-untouched').type('Mat');
      cy.get('button').click();
      cy.visit('/patient');
      cy.log('Should receive the first name to confirm the changes');
    });

    it('should update the patient last name', () => {
      cy.visit('/patient/updateAccount');
      cy.get('form.ng-untouched > :nth-child(4) > .ng-untouched').type('Smith');
      cy.get('button').click();
      cy.visit('/patient');
      cy.log('Should receive the last name to confirm the changes');
    });

    it('should update the patient birth date', () => {
      cy.visit('/patient/updateAccount');
      cy.get('form.ng-untouched > :nth-child(5) > .ng-untouched').type('2000-10-10');
      cy.get('button').click();
      cy.visit('/patient');
      cy.log('Should receive the address to confirm the changes');
    });

    it('should update the patient gender', () => {
      cy.visit('/patient/updateAccount');
      cy.get('form.ng-untouched > :nth-child(6) > .ng-untouched').type('Other');
      cy.get('button').click();
      cy.visit('/patient');
      cy.log('Should receive the address to confirm the changes');
    });

    it('should update the patient allergies', () => {
      cy.visit('/patient/updateAccount');
      cy.get('form.ng-untouched > :nth-child(7) > .ng-untouched').type('Peanuts');
      cy.get('button').click();
      cy.visit('/patient');
      cy.log('Should receive the address to confirm the changes');
    });

    it('should update the patient appointment history', () => {
      cy.visit('/patient/updateAccount');
      cy.get('form.ng-untouched > :nth-child(8) > .ng-untouched').type('2021-10-10');
      cy.get('button').click();
      cy.visit('/patient');
      cy.log('Should receive the address to confirm the changes');
    });
  });

  describe('Delete patient account', () => {
    it('should delete the patient account', () => {
      cy.visit('/patient/deleteAccount');
      cy.get('button').click();
    });
  });
});
