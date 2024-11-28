# Dependências entre User Stories

Este documento detalha as dependências entre as User Stories (US) do projeto de gerenciamento de agendamentos e recursos para cirurgias, destacando quais histórias estão interligadas.

---

## 1. Dependências entre Backoffice e 3D Visualization Module

### US 5.1.3 - Como Paciente, quero registrar-me para criar um perfil e agendar consultas
- **Dependência**: Depende da **US 6.2.1**, que implementa a interface de registo para pacientes, permitindo a criação de perfis necessários para visualização de agendamentos no módulo 3D.
- **Impacto**: A criação de perfis de pacientes no Backoffice é essencial para garantir que o módulo de Visualização 3D possa exibir corretamente as salas ocupadas pelos pacientes.

---

## 2. Dependências entre GDPR Module e Backoffice

### US 5.1.5 - Como Paciente, quero eliminar a minha conta e todos os dados associados para exercer meu direito ao esquecimento (GDPR)
- **Dependência**: Depende da **US 6.6.1** (conformidade com GDPR), que assegura que o processo de eliminação de dados esteja em conformidade com as regulamentações.
- **Impacto**: Assegura que todos os dados sejam removidos em conformidade com GDPR, evitando retenções indevidas.

---

## 3. Dependências entre Backoffice e Planning Module

### US 5.1.12 - Como Admin, quero criar um novo perfil de profissional de saúde
- **Dependência**: Relaciona-se com **US 6.1.4**, que depende da criação de perfis de profissionais de saúde para sincronizar os dados entre o Backoffice e o módulo de Planning.
- **Impacto**: Garante que o módulo de Planning tenha acesso a dados consistentes sobre os profissionais de saúde, essenciais para o agendamento.

### US 5.1.13 - Como Admin, quero editar o perfil de um profissional de saúde
- **Dependência**: Relaciona-se com **US 6.1.5**, que depende de dados atualizados dos perfis de profissionais de saúde no Backoffice para sincronizar com o módulo de Planning.
- **Impacto**: Permite que as atualizações de disponibilidade de profissionais sejam refletidas corretamente nos agendamentos.

### US 5.1.14 - Como Admin, quero desativar o perfil de um profissional
- **Dependência**: Relaciona-se com **US 6.1.5**, que depende de perfis de profissionais de saúde ativos e atualizados para evitar conflitos de agendamento no módulo de Planning.
- **Impacto**: Garante que apenas profissionais de saúde ativos possam ser agendados no Planning.

### US 5.1.16 - Como Médico, quero requisitar uma operação para um paciente
- **Dependência**: Depende da **US 6.2.14** (interface para requisitar operações) para que médicos possam realizar pedidos de operação, que serão integrados no módulo de Planning.
- **Impacto**: A criação de novos pedidos de operação afeta o Planning e a alocação de recursos.

### US 5.1.17 - Como Médico, quero atualizar um pedido de operação existente
- **Dependência**: Relaciona-se com **US 6.2.15** (interface para atualizar requisições de operação), permitindo que médicos alterem detalhes de um pedido que será refletido no Planning.
- **Impacto**: Mudanças nos pedidos de operação afetam o Planning e podem requerer novos ajustes.

### US 5.1.18 - Como Médico, quero remover um pedido de operação existente
- **Dependência**: Depende da **US 6.2.16** (interface para remoção de requisições de operação), permitindo que médicos cancelem um pedido que afeta o módulo de Planning.
- **Impacto**: A remoção de pedidos de operação altera a disponibilidade de salas e profissionais, necessitando novo planeamento.

### US 5.1.20 - Como Admin, quero adicionar novos tipos de operações
- **Dependência**: Relaciona-se com **US 6.1.4**, que depende da criação de tipos de operação para que o Planning possa aceder dados consistentes sobre operações.
- **Impacto**: Garante que o módulo de Planning tenha acesso aos tipos de operação disponíveis para o agendamento.

---

## 4. Dependências entre Integração, Planning Module e 3D Visualization Module

### US 6.1.1 - Como utilizador, quero uma UI integrada para todos os módulos do sistema
- **Dependência**: Não possui dependências específicas, mas suporta o fluxo geral de integração dos módulos de forma acessível.
- **Impacto**: Facilita a navegação entre módulos sem a necessidade de troca de URLs.

### US 6.1.2 - Como utilizador, quero que o menu da aplicação se ajuste conforme o meu papel
- **Dependência**: Não possui dependências específicas, mas é necessária para a acessibilidade dos recursos com base nos papéis dos utilizadores.
- **Impacto**: Melhora a usabilidade, mostrando apenas as opções relevantes para cada tipo de utilizador.

### US 6.1.3 - Como equipa de saúde, quero ver as informações de disponibilidade de salas na Visualização 3D sincronizadas com o Planning
- **Dependência**: Relaciona-se com **US 6.1.5**, pois a sincronização entre Planning e Backoffice garante que o status das salas exibido no módulo 3D esteja atualizado.
- **Impacto**: Exibe ocupação de salas em tempo real, essencial para visualização.

### US 6.1.4 - Como Admin, quero que o módulo de Planning sincronize com dados do Backoffice
- **Dependência**: Depende da **US 5.1.12** (criação de perfis de profissionais de saúde) e da **US 5.1.20** (criação de tipos de operação) para fornecer dados atualizados ao módulo de Planning.
- **Impacto**: Permite que o módulo de Planning tenha dados consistentes sobre profissionais e tipos de operação, essenciais para o agendamento.

### US 6.1.5 - Como Admin, quero que a disponibilidade e agendamentos no módulo de Planning estejam sincronizados com o Backoffice
- **Dependência**: Depende das US **5.1.13** e **5.1.14** para atualização de disponibilidade e perfis de profissionais no Backoffice.
- **Impacto**: A sincronização precisa entre módulos evita conflitos de agendamento.

### US 6.3.1 - Como Admin, quero obter o melhor agendamento de operações para uma sala em um dia específico
- **Dependência**: Depende da **US 6.1.3** para garantir que o Planning reflita o uso e a ocupação das salas conforme visualizado no módulo 3D.
- **Impacto**: Permite um planeamento preciso e visualização sincronizada dos agendamentos.

### US 6.3.3 - Como Admin, quero obter um agendamento eficiente próximo ao ideal em tempo real
- **Dependência**: Similar à **US 6.1.3**, depende da visualização em tempo real do módulo 3D para verificar os efeitos do agendamento rápido.
- **Impacto**: Facilita a confirmação visual e rápida das decisões de agendamento.

---

## 5. Dependências entre GDPR Module e Sistema de Notificação

### US 6.6.1 - Como staff, quero garantir que temos bom entendimento do projeto e de seu impacto em dados pessoais
- **Dependência**: Afeta todas as funcionalidades que lidam com dados pessoais (ex: **US 5.1.3**, **US 5.1.5**) para garantir que estão em conformidade com GDPR.
- **Impacto**: Estabelece que todas as práticas de proteção de dados nos módulos do sistema estejam de acordo com a GDPR.

### US 6.6.2 - Como sistema, quero notificar utilizadores e autoridade responsável em caso de violação de dados
- **Dependência**: Depende do monitoramento de segurança do sistema que afeta dados no Backoffice e Planning (ex: **US 5.1.5** para exclusão de dados) para detectar e notificar violações de dados.
- **Impacto**: Garante a conformidade com GDPR ao notificar rapidamente qualquer violação de dados pessoais.

---

## Resumo das Dependências Principais

1. **Sincronização entre Módulos**:
    - Backoffice, Planning e Visualização 3D precisam estar sincronizados para garantir consistência em tempo real.

2. **Conformidade com GDPR**:
    - O módulo GDPR monitora e audita operações em dados pessoais para assegurar conformidade, impactando principalmente a exclusão e gestão de dados sensíveis.

3. **Integração entre Planning e Visualização 3D**:
    - O módulo de Planning depende do módulo 3D para garantir que o uso das salas seja exibido corretamente.
