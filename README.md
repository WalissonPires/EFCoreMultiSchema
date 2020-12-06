# EFCoreMultiSchema
 
Projeto de exemplo de uso de múltiplos esquemas de banco de dados, postgresql, no Entity Framework Core.

## Resumo 

Para que seja possível utilizar o mesmo DbContext e migrações para múltiplos esquemas é preciso configurar o parâmetro SearchPath da string de conexão com o nome do esquema que deseja útilizar e configurar o esquema da tabela de migrações.

**OBS.: A string de conexão usada para criar as migrações deve usar o schema padrão.**

```CSharp

 var schema = "MyUserSchema";
 var connectionString = "Host=localhost;Port=5432;Database=MyApp;User ID=postgres;Password=***;SearchPath" + schema;

 var options = new DbContextOptionsBuilder()
  .UseNpgsql(connectionString, builder => builder.MigrationsHistoryTable("__EFMigrationsHistory", schema))
  .Options;
```

## Exemplo

Esse projeto tem um site de exemplo que armazena uma lista de contatos de vários usuários. Onde cada usuário tem seu próprio esquema no banco de dados.

## Demo

![alt text](https://raw.githubusercontent.com/WalissonPires/EFCoreMultiSchema/main/demo.gif "Demonstração")
