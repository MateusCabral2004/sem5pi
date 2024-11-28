# Relatório de Reunião: Módulo GDPR (6.6)

**Data:** 13 de novembro de 2024  
**Objetivo:** Discutir os requisitos e a implementação do **Módulo GDPR**, incluindo o conhecimento sobre o impacto de dados pessoais e o processo de notificação de violação de dados.

---

## 1. Introdução

Esta reunião teve como objetivo revisar os requisitos do **Módulo GDPR (6.6)**, que garantirá que a equipa entenda completamente o impacto da solução técnica sobre os dados pessoais dos pacientes e que o processamento esteja em conformidade com a legislação de proteção de dados (GDPR). Discutimos as medidas para garantir que os dados pessoais sejam tratados corretamente e que as notificações de violação de dados sejam enviadas de maneira adequada.

---

## 2. Discussão sobre os Subtópicos

### 2.1 Conhecimento da Equipe sobre o Projeto e Dados Pessoais (6.6.1)

- **Objetivo:** Como entidade responsável pela implementação da solução técnica, a equipa precisa ter um bom entendimento sobre o projeto, como ele afeta os dados pessoais dos pacientes e garantir que o processamento esteja em conformidade com a legislação.

- **Abordagem sugerida:**
    - **Identificação dos dados pessoais:** A equipa precisa identificar quais dados pessoais podem ser processados pelo projeto. Isso incluirá informações como nomes, moradas, dados médicos, etc.
    - **Identificação dos tipos de processamento:** A equipa deve compreender quais tipos de processamento serão realizados nos dados pessoais, como recolha, armazenamento, e destruição, ...
    - **Base legal para o processamento:** A equipa deve identificar as bases legais para o processamento de dados pessoais, como o consentimento do paciente, a necessidade de cumprimento de uma obrigação contratual ou legal, etc.

- **Próximos Passos:**
    - Definir claramente os tipos de dados pessoais processados e os tipos de processamento realizados.
    - Documentar a base legal para o processamento de cada tipo de dado.

### 2.2 Notificação de Violação de Dados (6.6.2)

- **Objetivo:** O sistema deve ser capaz de notificar tanto os utilizadores quanto a autoridade responsável em caso de violação de dados, para garantir o cumprimento dos requisitos do GDPR.

- **Abordagem sugerida:**
    - **Deteção automática de violações:** O sistema deve ser capaz de detetar violações de dados, como acessos não autorizados, alterações indevidas ou fuga de dados.
    - **Notificação dos utilizadores:** Quando uma violação for detectada, os utilizadores afetados devem ser notificados imediatamente. A notificação deve incluir:
        - Detalhes da violação (quais dados foram comprometidos).
        - Medidas de prevenção (ex: alteração de senhas, monitoramento de atividades suspeitas).
        - Recomendações para os utilizadores, como a mudança de senhas ou vigilância em relação a atividades suspeitas.
    - **Notificação à autoridade de proteção de dados:** A autoridade responsável (ex: ANPD) também deve ser notificada imediatamente. A notificação deve incluir logs detalhados da violação e das ações tomadas para remediá-la.
    - **Cumprimento dos prazos legais:** As notificações aos utilizadores e à autoridade devem ser enviadas dentro do prazo legal exigido (por exemplo, dentro de 72 horas após a descoberta da violação).
    - **Registro das notificações:** O sistema deve registrar todas as notificações de violação de dados e as ações subsequentes para auditoria e conformidade com o GDPR.
---

## 3. Considerações Finais e Próximos Passos

- **Estudo:** A equipa precisa ser completamente treinada sobre o GDPR e as implicações do projeto para garantir o cumprimento da lei.

- **Deteção e Notificação de Violação de Dados:** A implementação da detecção de violações e o processo de notificação devem ser uma prioridade. O sistema precisa ser configurado para identificar rapidamente qualquer incidente de violação e enviar notificações no prazo legal.

- **Ação de Seguimento:**
    - Planear a implementação das funcionalidades de deteção e notificação de violação de dados.
    - Estabelecer um processo de auditoria para garantir que o sistema esteja em conformidade com os requisitos de GDPR.

