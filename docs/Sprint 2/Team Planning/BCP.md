# Relatório de Reunião: Módulo de Continuidade de Negócios (6.4)

**Data:** 13 de novembro de 2024  

**Objetivo:** Discutir os requisitos e abordagens para a implementação do Módulo de Continuidade de Negócios (6.4) e as respectivas funcionalidades.

---

## 1. Introdução

Esta reunião teve como objetivo a revisão dos requisitos do **Módulo de Continuidade de Negócios (6.4)**. Durante a discussão, abordamos os requisitos relacionados à implantação, segurança, backup, gerenciamento de riscos e controle de acessos. As estratégias a serem seguidas foram detalhadas para garantir a continuidade dos negócios, segurança de dados e eficiência na administração do sistema.

---

## 2. Discussão sobre os Subtópicos

### 2.1 Implantação de Módulos (6.4.1)

- **Objetivo:** Como Administrador do Sistema, a implantação de um dos módulos RFP na VM DEI deve ser sistemática, validando-a com o plano de testes em uma base agendada.

- **Abordagem sugerida:** A implantação será automatizada utilizando scripts de deployment que também acionam o plano de testes programado. A verificação de sucesso de cada implantação será feita através de um relatório gerado automaticamente.

- **Próximos Passos:** Desenvolver scripts de deployment e agendar a execução automatizada dos testes numa base contínua. Definir a frequência das validações (ex: semanal ou mensal).

### 2.2 Acesso Restrito à Rede Interna (6.4.2)

- **Objetivo:** Como Administrador do Sistema, apenas clientes da rede interna da DEI (via cabo ou VPN) poderão acessar a solução.

- **Abordagem sugerida:** A segurança será configurada no firewall, garantindo que apenas endereços IP da rede interna (incluindo VPN) tenham permissão para acessar os módulos. Autenticações também poderão ser feitas via VPN para reforçar a segurança.

- **Próximos Passos:** Configurar regras no firewall e VPN para restringir acessos. Validar com a equipa se todas as políticas de segurança são aplicadas corretamente.

### 2.3 Configuração Simples de Clientes (6.4.3)

- **Objetivo:** Como Administrador do Sistema, a lista de clientes do requisito 6.3.2 deve poder ser configurada simplesmente alterando um arquivo de texto.

- **Abordagem sugerida:** A lista de clientes será armazenada num arquivo de configuração simples, com formato legível (ex: JSON ou XML), onde os administradores podem adicionar ou remover clientes diretamente. O sistema irá carregar essa lista automaticamente durante a inicialização ou ao verificar alterações no arquivo.

- **Próximos Passos:** Definir o formato do arquivo e a maneira como o sistema irá detectar e carregar as alterações feitas. Testar a facilidade de uso e a segurança dessa configuração.

### 2.4 Identificação e Quantificação de Riscos (6.4.4)

- **Objetivo:** Como Administrador, o sistema deve permitir a identificação e quantificação dos riscos envolvidos na solução recomendada.

- **Abordagem sugerida:** Será necessário realizar uma análise de risco utilizando frameworks como FMEA (Failure Mode and Effects Analysis) ou análise SWOT (Strengths, Weaknesses, Opportunities, Threats).
- **Próximos Passos:** Desenvolver a matriz de risco e definir um processo de revisão periódica. A equipa de segurança e gestão de riscos será envolvida para garantir que todas as ameaças e vulnerabilidades sejam identificadas.

### 2.5 Definição do MBCO (6.4.5)

- **Objetivo:** Como Administrador do Sistema, deve-se definir o MBCO (Minimum Business Continuity Objective) a ser proposto aos stakeholders.

- **Abordagem sugerida:** O MBCO será definido com base nas necessidades de continuidade das operações essenciais da empresa. Será necessário envolver stakeholders-chave para entender as prioridades e estabelecer os parâmetros de recuperação.

- **Próximos Passos:** Documentar e validar as prioridades de recuperação de negócios.

### 2.6 Estratégia de Backup (6.4.6)

- **Objetivo:** Como Administrador, uma estratégia de backup deve ser proposta, justificada e implementada, visando minimizar o RPO (Recovery Point Objective) e WRT (Work Recovery Time).

- **Abordagem sugerida:** A estratégia de backup incluirá backups regulares e incrementais, com armazenamento tanto local quanto em nuvem. A estratégia será orientada para garantir a recuperação rápida e minimizar a perda de dados. Será necessário definir a frequência dos backups e a infraestrutura necessária para garantir a alta disponibilidade.

- **Próximos Passos:** Definir a frequência de backups e os tipos de backup (completo, incremental, diferencial). Configurar o sistema para realizar backups automáticos e garantir sua recuperação.

### 2.7 Criação de Pasta Pública (6.4.7)

- **Objetivo:** Como Administrador, será criada uma pasta pública para todos os usuários registados no sistema, onde poderão ler os conteúdos nela colocados.

- **Abordagem sugerida:** A pasta será criada em um servidor centralizado, com permissões de leitura para todos os utilizadores, mas sem a possibilidade de edição ou exclusão. O acesso à pasta será monitorado para garantir que as permissões de leitura sejam mantidas.

- **Próximos Passos:** Criar e configurar a pasta pública. Garantir que os utilizadores tenham acesso adequado e monitorar as permissões de leitura.

### 2.8 Monitoramento de Acessos Incorretos (6.4.8)

- **Objetivo:** Como Administrador, o sistema deve identificar utilizadores com mais de 3 tentativas de acesso incorretas.

- **Abordagem sugerida:** Será configurado um sistema de monitoramento de tentativas de login. Caso o número de tentativas incorretas ultrapasse 3. Isso será feito com logs de autenticação.

- **Próximos Passos:** Configurar a contagem de tentativas de login.

---

## 3. Considerações Finais e Próximos Passos

- **Avaliação de Segurança e Continuidade:** A equipa concorda que a implementação das políticas de segurança e continuidade deve ser feita de forma gradual, com testes contínuos para garantir que o sistema esteja sempre disponível e seguro.

- **Ação de Seguimento:** Planear a implementação das soluções discutidas, incluindo automação de testes, configuração de rede interna, análise de riscos, e estratégias de backup.

