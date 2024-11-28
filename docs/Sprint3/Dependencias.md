# Dependências entre as User Stories (US)

## **7.1 Integração**
- **7.1.3** (Sincronização entre visualização 3D e planejamento):  
  Depende do **módulo de planejamento** para exibir informações corretas sobre disponibilidade de salas. Relacionado a:
    - **7.3.1** (Método de atribuição automática de salas).
    - **7.3.2** (Uso de algoritmos genéticos para planejamento).

- **7.1.4** (Sincronização do módulo de backoffice):  
  Depende do **módulo de backoffice** para garantir que informações, como alergias (**7.2.2**) e condições médicas (**7.2.4**), estejam sincronizadas com o sistema.

- **7.1.2** (Menu ajustado por função):  
  Conecta-se a **todos os módulos**, pois o menu deve apresentar apenas opções relevantes baseadas no papel do usuário:
    - Médicos: acesso ao prontuário (**7.2.6**) e agendamento de cirurgia (**7.2.8**).
    - Administradores: gestão de especializações (**7.2.11**) e planejamento (**7.3.1**).

---

## **7.2 Módulo Backoffice**
- **7.2.6** (Atualização de prontuários):  
  Depende de:
    - **7.2.2** (Cadastro de alergias).
    - **7.2.4** (Cadastro de condições médicas).

- **7.2.7** (Busca em prontuários):  
  Relaciona-se com **7.2.6**, pois as informações atualizadas no prontuário devem estar disponíveis para busca.

- **7.2.14** (Prontuário no perfil do paciente):  
  Reúne funcionalidades de:
    - **7.2.6** (Atualização de prontuários).
    - **7.2.7** (Busca no prontuário).

- **7.2.8** (Criação de agendamentos de cirurgia):  
  Depende de:
    - **7.3.1** (Atribuição automática de salas).
    - **7.3.4** (Seleção do melhor método de planejamento).

- **7.2.10** (Cadastro de tipos de salas):  
  Impacta diretamente o **módulo de planejamento**, especificamente:
    - **7.3.1** (Alocação de cirurgias).

- **7.2.11, 7.2.12 e 7.2.13** (Gestão de especializações):  
  Relaciona-se à criação e edição de especializações, impactando:
    - Perfis de médicos.
    - Procedimentos médicos associados no backoffice.

---

## **7.3 Módulo de Planejamento**
- **7.3.2** (Algoritmos genéticos para agendamento):  
  Depende de **7.3.1** (Atribuição automática de salas) para otimizar a alocação.

- **7.3.4** (Seleção do método de planejamento):  
  Integra abordagens como:
    - **7.3.1** (Atribuição automática).
    - **7.3.2** (Algoritmos genéticos).

- **7.3.1 e 7.3.4**:  
  Ambos fornecem dados ao módulo de visualização 3D (**7.1.3**) para exibir informações sincronizadas.

---

## **7.4 Continuidade de Negócios**
- **7.4.2** (Justificativa para mudanças na infraestrutura):  
  Depende de:
    - **7.4.1** (Plano de recuperação de desastres).

- **7.4.3 e 7.4.4** (Backup e gestão de backups):
    - **7.4.4** depende de **7.4.3** para gerenciar as cópias realizadas.

- **7.4.5** (Log e alertas de falha nos backups):  
  Conecta-se com:
    - **7.4.3** (Criação de backups).
    - **7.4.4** (Gestão de backups).

- **7.4.12** (Validação automática de backups):  
  Depende de:
    - **7.4.3** (Criação de backups).
    - **7.4.4** (Gerenciamento de backups).

---

## **7.5 Módulo de Visualização 3D**
- **7.5.1** (Seleção de salas com clique):  
  Depende de:
    - **7.3.1** (Planejamento automático de salas).

- **7.5.2** (Exibir informações atualizadas):  
  Relaciona-se a:
    - **7.3.1** (Dados do planejamento).
    - **7.3.4** (Métodos de otimização).

- **7.5.3 e 7.5.4** (Animações e iluminação):  
  Dependem de:
    - **7.5.1** (Seleção de salas).
    - **7.5.2** (Exibição de informações atualizadas).

---

## **7.6 Módulo GDPR**
- **7.6.1** (Download de histórico médico):  
  Depende de:
    - **7.2.6** (Atualização de prontuários).

- **7.6.2** (Exclusão de dados pessoais):  
  Relaciona-se a:
    - **7.6.3** (Política de privacidade clara).

- **7.6.4** (Políticas de retenção):  
  Impacta:
    - **7.6.1** (Histórico médico), pois define o tempo de armazenamento.
