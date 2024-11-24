# Planejamento do Sprint 2 - 4 Semanas

## Objetivo Geral
Desenvolver e integrar funcionalidades principais dos módulos Backoffice, Planejamento, Visualização 3D e GDPR, com foco inicial na implementação das interfaces de usuário. O desenvolvimento das funcionalidades de backend, sincronização e conformidade com GDPR será feito nas últimas duas semanas.

---

## Semanas 1 e 2 - Implementação de Interfaces de Usuário (UI)

### Objetivos Gerais
1. Desenvolver as interfaces de usuário para todas as funcionalidades dos módulos do Backoffice e Planejamento.
2. Garantir que a navegação entre módulos seja integrada e consistente.
3. Configurar controle de acesso e menus baseados nos papéis dos usuários.

### Tarefas

#### Semana 1
- **Tarefa 6.1.1**: Implementar interface unificada para todos os módulos.
    - **Descrição**: Criar layout de navegação unificado para que o usuário possa acessar diferentes módulos sem trocar de URL.
    - **Responsáveis**: Equipe de Frontend
    - **Duração Estimada**: 2 dias

- **Tarefa 6.1.2**: Implementar controle de acesso baseado em papéis para o menu.
    - **Descrição**: Configurar menus de acordo com o papel do usuário, exibindo apenas as opções pertinentes.
    - **Responsáveis**: Equipe de Frontend
    - **Duração Estimada**: 1 dia

- **Tarefa 6.2.x**: Desenvolvimento das interfaces do Backoffice para gerenciamento de:
    - **Usuários**: Criação, edição, remoção e listagem de perfis de pacientes e equipe médica.
    - **Operações**: Criação, atualização e remoção de tipos de operações e agendamentos.
    - **Tarefas associadas**:
        - Criar formulários, tabelas de visualização e modais para interação.
        - Testar interfaces para garantir funcionalidade e consistência visual.
    - **Responsáveis**: Equipe de Frontend
    - **Duração Estimada**: 5 dias

#### Semana 2
- **Tarefa 6.2.x** (continuação): Conclusão das interfaces do Backoffice para funcionalidades pendentes da semana 1.
    - **Descrição**: Refinar e finalizar as interfaces do Backoffice, garantindo que estejam prontas para integração com o backend.
    - **Responsáveis**: Equipe de Frontend
    - **Duração Estimada**: 3 dias

- **Tarefa 6.1.3**: Desenvolver UI inicial do módulo de Visualização 3D para exibir a planta do hospital.
    - **Descrição**: Implementar controles básicos e a estrutura inicial de visualização das salas (sem sincronização de dados).
    - **Responsáveis**: Equipe de Visualização 3D
    - **Duração Estimada**: 2 dias

- **Tarefa 6.1.4**: Preparar UI de Planejamento para configuração de agendamentos.
    - **Descrição**: Criar interface para visualização e configuração de agendamentos com parâmetros básicos (ex.: sala, data).
    - **Responsáveis**: Equipe de Planejamento
    - **Duração Estimada**: 2 dias

### Reuniões
- **Reunião de Início do Sprint**: Alinhamento das tarefas e responsabilidades para a implementação das UIs.
- **Standups Diários**: Acompanhamento das tarefas de UI e resolução de bloqueios.
- **Revisão de UI ao Final da Semana 2**: Avaliação e feedback das interfaces concluídas para iniciar o desenvolvimento do backend.

---

## Semanas 3 e 4 - Implementação de Funcionalidades, Integração e Testes

### Objetivos Gerais
1. Implementar funcionalidades de backend e lógica de sincronização entre módulos.
2. Garantir que os dados entre o Backoffice, Planejamento e Visualização 3D estejam sincronizados.
3. Desenvolver a conformidade com GDPR e o sistema de notificação de violação de dados.

### Tarefas

#### Semana 3
- **Tarefa 6.1.3 (Continuação)**: Integrar dados de ocupação e disponibilidade de salas entre Planejamento e Visualização 3D.
    - **Descrição**: Sincronizar ocupação das salas para exibição em tempo real na interface 3D.
    - **Responsáveis**: Equipe de Planejamento e Visualização 3D
    - **Duração Estimada**: 3 dias

- **Tarefa 6.1.4 e 6.1.5**: Sincronização de dados entre Backoffice e Planejamento.
    - **Descrição**: Sincronizar dados de profissionais, tipos de operação e agendamentos, assegurando a atualização entre módulos.
    - **Responsáveis**: Equipe de Backend
    - **Duração Estimada**: 3 dias

- **Tarefa 6.3.1**: Implementar algoritmo de agendamento otimizado.
    - **Descrição**: Desenvolver o algoritmo para gerar o melhor agendamento de operações com base em disponibilidade de sala e profissionais.
    - **Responsáveis**: Equipe de Planejamento
    - **Duração Estimada**: 3 dias

- **Tarefa 6.6.1**: Revisar práticas de proteção de dados e aplicar conformidade com GDPR.
    - **Descrição**: Avaliar as interfaces e fluxos de dados para assegurar que atendem aos requisitos de GDPR.
    - **Responsáveis**: Equipe de Compliance e GDPR
    - **Duração Estimada**: 1 dia

#### Semana 4
- **Tarefa 6.3.3**: Implementar heurísticas para um agendamento eficiente em tempo real.
    - **Descrição**: Implementar agendamento rápido (não otimizado) para situações onde o tempo de resposta é mais importante.
    - **Responsáveis**: Equipe de Planejamento
    - **Duração Estimada**: 2 dias

- **Tarefa 6.6.2**: Implementar sistema de notificação de violação de dados.
    - **Descrição**: Configurar monitoramento de segurança e sistema de alerta para conformidade com GDPR.
    - **Responsáveis**: Equipe de GDPR e Segurança
    - **Duração Estimada**: 2 dias

- **Testes de Integração e Conformidade GDPR**:
    - **Descrição**: Realizar testes para assegurar a integração entre módulos e a conformidade com GDPR.
    - **Responsáveis**: Toda a equipe
    - **Duração Estimada**: 2 dias

- **Correções e Ajustes Finais**: Revisar funcionalidades e realizar ajustes de UI e backend, conforme necessário.
    - **Responsáveis**: Toda a equipe
    - **Duração Estimada**: 1 dia

- **Documentação e Preparação para a Revisão Final**: Documentar as funcionalidades e preparar apresentação final.
    - **Responsáveis**: Toda a equipe
    - **Duração Estimada**: 1 dia

### Reuniões
- **Standups Diários**: Acompanhamento das tarefas de backend e integração.
- **Reunião de Revisão do Sprint**: Apresentação final das funcionalidades implementadas e feedback.
- **Retrospectiva do Sprint**: Reflexão sobre pontos fortes, desafios e melhorias para o próximo sprint.

---

## Resumo do Planejamento por Semana

| Semana | Foco Principal                        | Tarefas                                                                                         |
|--------|---------------------------------------|-------------------------------------------------------------------------------------------------|
| 1      | Desenvolvimento inicial de UIs        | Interface unificada (6.1.1), controle de acesso (6.1.2), UIs do Backoffice (parte 1) (6.2.x)   |
| 2      | Conclusão das UIs                     | UIs do Backoffice (parte 2) (6.2.x), Interface 3D inicial (6.1.3), UI de Planejamento (6.1.4) |
| 3      | Implementação de backend e integração | Sincronização Backoffice-Planejamento (6.1.4, 6.1.5), Algoritmos de Planejamento (6.3.1), GDPR (6.6.1) |
| 4      | Testes e finalização                  | Agendamento eficiente (6.3.3), Notificação de violação (6.6.2), Testes de integração e conformidade |

---

Este planejamento prioriza o desenvolvimento das interfaces nas primeiras duas semanas e a implementação das funcionalidades e integração nas últimas duas semanas, alinhando-se ao objetivo do Sprint 2.
