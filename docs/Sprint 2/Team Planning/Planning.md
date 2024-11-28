# Relatório de Reunião: Planeamento do Módulo de Agendamento de Cirurgias

**Data:** 13 de novembro de 2024  

**Objetivo:** Discutir a implementação das funcionalidades do Módulo de Planning (6.3) e as abordagens para otimização do agendamento de cirurgias.

---

## 1. Introdução

O objetivo desta reunião foi rever os requisitos do Módulo de Planeamento (seção 6.3), que envolve a otimização e a melhoria do agendamento de cirurgias em salas de operação. O foco principal foi discutir as abordagens para alcançar um agendamento eficiente e apresentar as considerações de complexidade, escalabilidade e tempo de resposta, conforme solicitado.

---

## 2. Discussão sobre os Subtópicos

### 2.1 Planeamento do Agendamento (6.3.1)

- **Objetivo:** Como Administrador, o sistema deve gerar o agendamento das cirurgias de forma que termine o mais cedo possível em um dia específico, respeitando as restrições de disponibilidade de pessoal (ex: número de médicos ou outros profissionais).

- **Abordagem sugerida:** A solução proposta inicialmente foi gerar todas as sequências possíveis de cirurgias e selecionar a melhor sequência. Esta abordagem é viável apenas até um certo número de cirurgias, pois a quantidade de possíveis combinações cresce exponencialmente. Por isso, deve-se considerar o uso de métodos de otimização eficientes para um número limitado de cirurgias.

- **Interface do Utilizador (UI):** A interface permitirá que o utilizador forneça parâmetros adicionais, como o número da sala de operação e a data do agendamento. O sistema gerará automaticamente o plano de agendamento e o exibirá para o utilizador. A interface pode bloquear temporariamente enquanto o módulo de planeamento está a processar a solução, o que é aceitável.

- **Próximos Passos:** Definir melhor a lógica de cálculo das sequências e como otimizar a escolha da melhor solução dentro das limitações computacionais. Avaliar algoritmos de otimização como Programação Linear ou Algoritmos Genéticos.

### 2.2 Complexidade do Problema e Limitações (6.3.2)

- **Objetivo:** Como Administrador, é necessário saber qual a quantidade máxima de cirurgias para o qual o sistema pode calcular uma solução ótima, ou seja, qual a "dimensão" máxima do problema que pode ser tratada.

- **Análise de Complexidade:** A solução de todas as combinações de sequências de cirurgias envolve que, à medida que o número de cirurgias aumenta, o tempo de processamento pode crescer rapidamente e tornar-se inviável. Um estudo detalhado sobre a escalabilidade do algoritmo precisa ser feito.

    - **Abordagem:** Realizar uma análise de complexidade com base no número de cirurgias e nas restrições de pessoal para determinar até qual ponto o sistema pode calcular uma solução ótima dentro de um tempo aceitável (ex: 10-15 minutos).

    - **Alternativas:** Caso o número de cirurgias exceda o limite viável para a solução ótima, alternativas como heurísticas ou algoritmos aproximados podem ser adotadas, priorizando um equilíbrio entre tempo de execução e qualidade da solução.

### 2.3 Solução Aproximada (6.3.3)

- **Objetivo:** Como Administrador, o sistema deve ser capaz de gerar um bom agendamento (não necessariamente ótimo), mas dentro de um tempo útil para ser adotado, sem comprometer a eficácia.

- **Abordagem sugerida:** Usar métodos heurísticos ou algoritmos informados, como o algoritmo ganancioso (greedy) ou regras baseadas em prioridades, para gerar um agendamento eficiente.

    - **Algoritmo Greedy:** Este algoritmo pode ser usado para alocar cirurgias nas janelas de tempo de forma sequencial, priorizando sempre a cirurgia que tem a menor duração ou a que libera a sala mais rapidamente.

    - **Regras Baseadas em Prioridades:** Criar uma lista de regras para a alocação de cirurgias, considerando prioridades como urgência, disponibilidade de pessoal e tempo de duração.

- **Interface do Utilizador (UI):** O utilizador poderá selecionar a heurística a ser usada (ex: Greedy, Baseada em Prioridade) e fornecer parâmetros como data e sala de operação. O sistema gerará o plano de agendamento de acordo com os parâmetros fornecidos e exibirá o plano gerado na tela, com um tempo de resposta desejado de até 30 segundos.

- **Próximos Passos:** Avaliar e testar diferentes heurísticas para garantir que o sistema gere um bom agendamento dentro do tempo estabelecido e com qualidade aceitável.

---

## 3. Considerações Finais e Próximos Passos

- **Avaliação da Viabilidade Técnica:** A equipa concorda que uma análise de complexidade detalhada é fundamental para determinar os limites do sistema quanto ao número de cirurgias. Uma solução ótima pode ser viável apenas até um número limitado de cirurgias, e, além disso, devem ser exploradas soluções aproximadas para casos mais complexos.

- **Desenvolvimento e Testes:** A implementação inicial deve focar numa solução de boa qualidade com tempo de resposta razoável, utilizando heurísticas eficientes. Em seguida, podemos realizar testes com números variados de cirurgias para observar a escalabilidade do sistema.

- **Ação de Seguimento:** Agendar uma nova reunião para discutir a implementação das primeiras abordagens heurísticas e realizar testes iniciais de desempenho.

---

**Encerramento:** A reunião foi concluída com os próximos passos bem definidos e com a responsabilidade de aprofundar as análises de complexidade e avaliar as soluções heurísticas para a geração de agendamentos eficientes.

