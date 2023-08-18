# Data Lawyer
Desafio para a implementação de um web crawler

## Arquitetura
### Camada de Domínio
- Responsável pelos objetos de negócio da aplicação, suas associações e validações.

### Camada de Persistência
- Responsável pela persistência da camada de domínio.

### Camada de Serviço
- Responsável pela orquestração da camada de domínio e da aplicação, a qual ofereçe uma interface de serviços.

### Camada de Rastreamento
- Responsável pela implementação dos motores de busca para o rastreamento e raspagem de dados (Crawler, Web Scraping).

### Camada de API
- Responsável em fornecer para a aplicação cliente os endpoints de serviços da aplicação (cadastros, consultas, processamentos etc).

## Observações
### Testes de Unidades e Serviços
- Para testar a aplicação basta rodar os testes pelo Visual Studio, não precisa instalar nenhuma extensão, compile e rode.

### Testes das APIs
- Compile e execute o projeto DataLawyer.Api
- Pode usar ferramentas como Postman ou Swagger.

    ### API de Rastreamento    
    - Consulta Processo do 2º Grau do Tribunal de Justiça da Bahia
        - GET: api/rastreamentos/tjba/segundograu/{numeroDoProcesso}

    - Consulta Movimentações de Processo do 2º Grau do Tribunal de Justiça da Bahia
        - GET: api/rastreamentos/tjba/segundograu/movimentos/{numeroDoProcesso}

    - Consulta e Atualiza Processo (com suas movimentações) do 2º Grau do Tribunal de Justiça da Bahia
        - POST: api/rastreamentos/tjba/segundograu/{numeroDoProcesso}
