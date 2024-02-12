# Aluraflix - Challenge back-end Alura

API REST para cadastro de vídeos, separados por categoria e que seja necessário se autenticar para acessar aos vídeos.
- Projeto do Alura Challenge Back-end 1ª Edição.

| :placard: Vitrine.Dev |     |
| -------------  | --- |
| :sparkles: Nome        | **Aluraflix API**
| :label: Tecnologias | C#, .NET 6, SQL Server
| :rocket: URL         | https://github.com/MPoleto/aluraflix-challenge-backend-one.git
| :fire: Desafio     | https://www.alura.com.br/challenges/back-end

  
![](./aluraflix-backend/aluraflix-swagger.gif#vitrinedev)

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
- Swagger

### Regras de negócio

- Todos os campos do vídeo devem ser obrigatórios e validados.  
- Uma nova categoria não pode ser criada caso tenha algum campo vazio. Caso em branco, informar: `O campo é obrigatório`.  
- A categoria com ID = 1 deve se chamar `LIVRE` e caso ela não seja especificada na criação do vídeo, vamos atribuir o ID = 1. 
- Adicione nas requisições GET em ambos os modelos, tanto vídeos como categorias uma paginação que retorne 5 itens por página: ```GET /videos/?page=2```  
- Criar endpoint com um número fixo de filmes disponível, sem a necessidade de autenticação: ```GET /videos/free```  

### Desenvolvimento
#### Semana 1
- Criação do banco de dados.
    - Classe `Context` para fazer a conexão com o banco de dados e criação da tabela `Videos`
    - Classe `VideoDAO` para acessar a tabela no banco de dados
- Elaboração da classe modelo `Video` com as informações: id, titulo, descrição, url. 
    - Classes `DTO` para mapear as propriedades da classe modelo. Nessas classes foi usado *`Data Annotations`* para determinar as propriedades como obrigatórias e fazer as validações.
        - Classe `VideoProfile` para fazer o mapeamento entre a classe modelo e as classes `DTO` por meio do `AutoMapper`.
- Controller com as rotas de requisição: 
    - Rotas GET:
        - Exibir todos os vídeos
        - `id` como endpoit para exibir um único vídeo.
    - POST - cadastrar um novo vídeo 
    - PUT - atualizar todas as informações
    - PATCH - atualização parcial
    - DELETE - deletar vídeo pelo id

#### Semana 2
- Classes modelo `Categoria` e `DTO` com as informações: id, titulo e cor.
- Armazenar no banco de dados informações sobre as categorias.
    - Classe `CategoriaDAO` para acesso ao bando de dados.
- Adicionar camada de serviço.
- Implementar relação entre vídeos e categorias, cada vídeo uma categoria.
    - Incluir campo categoriaId no modelo video;
- Adicionar à controller de `Videos`:
    - Buscar vídeos via `query parameters` - ```/videos/?search=termoBuscado```
- Controller para `Categorias` com as rotas de requisição: 
    - Rotas GET:
        - Exibir todos as categorias
        - `id` como endpoit para exibir uma única categoria.
        - Exibir vídeos por categoria ```categorias/:id/videos/```
    - POST: criar categoria.
    - PUT - atualizar todas as informações
    - PATCH - atualização parcial
    - DELETE - deletar categoria pelo id.
        - Caso a categoria possua vídeos, estes passam a ser da categoria 1.  

#### Semana 3 e 4
- Adicionar paginação nas requisições de vídeos e categorias.
- Adicionar método de autenticação.