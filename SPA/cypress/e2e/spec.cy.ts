describe('My First Test', () => {
  it('Visits the initial project page', () => {
    cy.visit('/')
    cy.contains('h1', 'Welcome to Surgical App System')
  })
})
