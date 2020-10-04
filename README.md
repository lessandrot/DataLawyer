# Data Lawyer
Desafio para a implementação de web scraping

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
- Para testar pode usar ferramentas como Postman ou Swagger.