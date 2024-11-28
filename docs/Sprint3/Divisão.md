# Divisão de Tarefas - Grupo de 4 Membros

## **Membro 1 - Gestão de Dados Médicos**
1. **7.2.2** - Como Admin, quero adicionar novas alergias, para que os médicos possam atualizar os prontuários dos pacientes.
2. **7.2.3** - Como Médico, quero buscar alergias, para que eu possa utilizá-las no prontuário.
3. **7.2.4** - Como Admin, quero adicionar novas condições médicas, para que os médicos possam atualizar os prontuários.
4. **7.2.5** - Como Médico, quero buscar condições médicas, para que eu possa utilizá-las no prontuário.

---

## **Membro 2 - Prontuários e Atualização Médica**
1. **7.2.6** - Como Médico, quero atualizar o prontuário do paciente, respeitando condições médicas e alergias.
2. **7.2.7** - Como Médico, quero buscar entradas no prontuário, respeitando condições médicas e alergias.
3. **7.2.14** - Como Médico, quero acessar o prontuário médico ao visualizar o perfil do paciente, para gerenciá-lo no contexto.

---

## **Membro 3 - Agendamento e Salas**
1. **7.2.8** - Como Médico, quero criar um agendamento de cirurgia para que o paciente não precise esperar pelo planejamento automático.
2. **7.2.9** - Como Médico, quero atualizar um agendamento de cirurgia, para que eu possa sobrescrever o planejamento automático.
3. **7.2.10** - Como Admin, quero adicionar tipos de salas para refletir os procedimentos médicos disponíveis no sistema.

---

## **Membro 4 - Gestão de Especializações**
1. **7.2.11** - Como Admin, quero adicionar novas especializações, para atualizar ou corrigir informações sobre a equipe e os tipos de operações.
2. **7.2.12** - Como Admin, quero listar/pesquisar especializações, para ver os detalhes e editar as especializações.
3. **7.2.13** - Como Admin, quero editar especializações, para atualizar ou corrigir informações sobre a equipe e tipos de operações.
4. **7.2.15** - Como Admin, quero listar/pesquisar especializações, para ver os detalhes, editar e remover especializações.

---

## **Resumo Consolidado**

| **Membro**   | **Tarefas**                                                                                     |
|--------------|-------------------------------------------------------------------------------------------------|
| **Membro 1** | 7.2.2, 7.2.3, 7.2.4, 7.2.5 (Gestão de dados médicos: alergias e condições médicas).              |
| **Membro 2** | 7.2.6, 7.2.7, 7.2.14 (Interação com prontuários: atualização, busca, e visualização).            |
| **Membro 3** | 7.2.8, 7.2.9, 7.2.10 (Agendamento e gestão de salas).                                           |
| **Membro 4** | 7.2.11, 7.2.12, 7.2.13, 7.2.15 (Gestão de especializações: adicionar, editar, listar/remover).   |

---

## **7.3...7.6**
## **Membro 1 - Planejamento Automático de Cirurgias**
1. **7.3.1** - Como Admin, quero um método automático para atribuir operações (cirurgias) a várias salas de operação.
2. **7.3.2** - Como Admin, quero agendar cirurgias em várias salas utilizando Algoritmos Genéticos (parâmetros precisam ser ajustados).

---

## **Membro 2 - Escolha de Método e Pesquisa sobre Tecnologias**
1. **7.3.3** - Como Gerente Hospitalar, quero um estudo sobre o estado da arte em Robótica ou Visão Computacional no contexto de cirurgias.
2. **7.3.4** - Como Admin, quero que o sistema escolha adequadamente o método de agendamento (gerar todos e selecionar melhor; heurístico; Algoritmo Genético) com base na dimensão do problema e tempo disponível para gerar a solução.

---

## **Membro 3 - Continuidade de Negócios**
1. **7.4.1** - Como Administrador da Organização, quero um plano de recuperação de desastres que atenda ao MBCO definido no Sprint B.
2. **7.4.2** - Como Administrador da Organização, quero justificativas para mudanças na infraestrutura, garantindo um MTD (Máximo Tempo de Inatividade Tolerável) de 20 minutos.
3. **7.4.3** - Como Administrador de Sistema, quero criar um script para copiar backups do banco de dados para um ambiente na nuvem com um formato específico.

---

## **Membro 4 - Automação e Segurança em Backups**
1. **7.4.4** - Como Administrador de Sistema, quero gerenciar os backups do banco de dados com o seguinte cronograma:
    - 1 backup por mês (últimos 12 meses);
    - 1 backup por semana (último mês);
    - 1 backup por dia (última semana).
2. **7.4.5** - Como Administrador de Sistema, quero que o processo de backup seja registrado no log do Linux, com alertas em caso de falhas graves.
3. **7.4.6** - Como Administrador de Sistema, quero que os backups tenham um tempo de vida de no máximo 7 dias, exceto para retenções mensais e anuais.

---

## **Resumo Consolidado**

| **Membro**   | **Tarefas**                                                                                      |
|--------------|--------------------------------------------------------------------------------------------------|
| **Membro 1** | 7.3.1, 7.3.2 (Planejamento automático de cirurgias com Algoritmos Genéticos).                    |
| **Membro 2** | 7.3.3, 7.3.4 (Pesquisa de tecnologias e escolha de métodos de agendamento).                      |
| **Membro 3** | 7.4.1, 7.4.2, 7.4.3 (Continuidade de negócios e plano de recuperação de desastres).              |
| **Membro 4** | 7.4.4, 7.4.5, 7.4.6 (Automação e segurança em backups de banco de dados).                        |

---

## **7.5...7.6**

## **Membro 1 - Interação com o Ambiente 3D**
1. **7.5.1** - Como membro da equipe de saúde, quero selecionar uma sala clicando na mesa cirúrgica correspondente. A câmera deve mover-se horizontalmente, apontando para o centro da sala.
2. **7.5.2** - Como membro da equipe de saúde, ao pressionar a tecla “i”, quero exibir/ocultar um overlay com informações atualizadas sobre a sala selecionada.

---

## **Membro 2 - Movimentação e Animações no Ambiente 3D**
1. **7.5.3** - Como membro da equipe de saúde, ao selecionar uma sala diferente, quero que um holofote siga a câmera, apontando para o centro da mesa cirúrgica.
2. **7.5.4** - Como membro da equipe de saúde, ao selecionar uma sala diferente, quero que a câmera (e o holofote, se aplicável) mova-se suavemente em vez de instantaneamente, usando APIs como tween.js.

---

## **Membro 3 - Direitos e Solicitações de Pacientes (GDPR)**
1. **7.6.1** - Como paciente, quero baixar meu histórico médico em um formato portátil e seguro, para transferi-lo facilmente para outro provedor de saúde.
2. **7.6.2** - Como paciente, quero solicitar a exclusão de meus dados pessoais, para exercer meu direito de ser esquecido conforme o GDPR.
3. **7.6.4** - Como paciente, quero saber por quanto tempo meus dados pessoais serão mantidos, acessando as políticas de retenção de dados no sistema.

---

## **Membro 4 - Transparência e Políticas de Privacidade (GDPR)**
1. **7.6.3** - Como paciente, quero saber quais dados serão processados, de que maneira, e como posso exercer meus direitos, por meio de uma política de privacidade acessível e em conformidade com os artigos 13 e 14 do GDPR.
2. **7.6.4** - Como paciente, quero saber por quanto tempo meus dados pessoais serão mantidos, acessando as políticas de retenção de dados no sistema.

---

## **Resumo Consolidado**

| **Membro**   | **Tarefas**                                                                                      |
|--------------|--------------------------------------------------------------------------------------------------|
| **Membro 1** | 7.5.1, 7.5.2 (Interação com o ambiente 3D: seleção e overlay de informações).                    |
| **Membro 2** | 7.5.3, 7.5.4 (Movimentação e animações no ambiente 3D).                                          |
| **Membro 3** | 7.6.1, 7.6.2, 7.6.4 (Solicitações de pacientes: download, exclusão de dados e políticas).        |
| **Membro 4** | 7.6.3, 7.6.4 (Transparência e políticas de privacidade em conformidade com GDPR).                |

---

Essa divisão considera a complexidade de cada tarefa, separando as funcionalidades 3D das relacionadas à privacidade de dados (GDPR), promovendo equilíbrio no esforço entre os membros do grupo.
