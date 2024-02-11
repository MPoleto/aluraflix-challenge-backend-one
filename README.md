# Aluraflix - Challenge back-end Alura

API REST para cadastro de vídeos, separados por categoria e que seja necessário se autenticar para acessar aos vídeos.
- Projeto do Alura Challenge Back-end 1ª Edição.

| :placard: Vitrine.Dev |     |
| -------------  | --- |
| :sparkles: Nome        | **API Aluraflix**
| :label: Tecnologias | C#, .NET 6, SQL Server
| :rocket: URL         | https://url-deploy.com.br
| :fire: Desafio     | https://www.alura.com.br/challenges/back-end

<!-- Inserir imagem com a #vitrinedev ao final do link -->
![](https://via.placeholder.com/1200x500.png?text=imagem+lindona+do+meu+projeto#vitrinedev)

## Detalhes do projeto

    Principais funcionalidades a serem implementadas são:

    1. API com rotas implementadas segundo o padrão REST;
    2. Validações feitas conforme as regras de negócio;
    3. Implementação de base de dados para persistência das informações;
    4. Serviço de autenticação para acesso às rotas GET, POST, PUT e DELETE.

### Tecnologias

- Linguagem: C#
- .NET 6
- ASP.NET Core
- SQLServer
- Entity Framework Core 6.0.24
- AutoMapper - mapeamento das classes modelo
- ASP.NET Core MVC Newtonsoft.Json 6.0.24 - para o método HTTP PATCH
- ASP.NET Core Identity
- JWT Bearer token
- Postman - para testar as requisições da API

> ### Regras de negócio
>
> Todos os campos do vídeo devem ser obrigatórios e validados.

#### Semana 1
- Criação do banco de dados.
    - Classe `Context` para fazer a conexão com o banco de dados e criação da tabela `Videos`
    - Classe `VideoDAO` para acessar a tabela no banco de dados
- Elaboração das classe modelo `Video` com as informações: id, titulo, descrição, url. 
    - Classes `DTO` para mapear as propriedades da classe modelo. Nessas classes foi usado *`Data Annotations`* para determinar as propriedades como obrigatórias e fazer as validações.
        - Classe `VideoPorfile` para fazer o mapeamento entre a classe modelo e as classes `DTO` por meio do `AutoMapper`.
- Controller com as rotas de requisição: 
    - Rotas GET:
        - Exibir todos os vídeos
        - `id` como endpoit para exibir um único vídeo.
    - POST - cadastrar um novo vídeo 
    - PUT - atualizar todas as informações
    - PATCH - atualização parcial
    - DELETE - deletar vídeo pelo id

#### Semana 2
