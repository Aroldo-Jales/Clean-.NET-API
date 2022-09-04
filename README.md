# Programação para Internet 2

+ [Rest API (ASP.NET 6)](#rest-api)
+ [Requisitos](#requistos)
+ [Como executar o projeto](#como-executar-o-projeto)
+ [Estutura do projeto](#estrutura-do-projeto)
    + [Auth](#auth)
        - [SignUp](#signup)
            - [SignUp Request](#signup-request)
            - [SignUp Response](#signup-response)
        - [SignIn](#signin)
            - [SignIn Request](#signin-request)
            - [SignIn Response](#signin-response)
# Requisitos
    
- [.NET 6.0 SDK](https://dotnet.microsoft.com/en-us/download)

# Como executar o projeto
    
- Entrar no diretório do projeto e executar o comando:``` > dotnet run --project .\Prova1.Api\```

<br>

# Estrutura do projeto

<br>

## Auth

### SignUp

```js
POST {{host}}/auth/SignUp
```

#### SignUp Request

```json
{
    "firstName": "Foo",
    "lastName": "Bar",
    "email": "foobar@email.com",
    "password": "Passwd#FooBar42",
}
```

#### SignUp Response

```js
200 OK
```

```json
{
    "id": "d89c2d9a-eb3e-4075-95ff-b920b55aa104",
    "firstName": "Foo",
    "lastName": "Bar",
    "email": "foobar@email.com",
    "password": "Passwd#FooBar42",
    "token": "eyJhb..hbbQ"
}
```

### SignIn

```js
POST {{host}}/auth/SignIn
```

#### SignIn Request

```json
{        
    "email": "Foo@Bar.com",
    "password": "Passwd#FooBar42",    
}
```

#### SignIn Response

```js
200 OK
```

```json
{
  "id": "d89c2d9a-eb3e-4075-95ff-b920b55aa104",
  "firstName": "Foo",
  "lastName": "Bar",
  "email": "foobar@email.com",
  "token": "eyJhb..hbbQ"
}
```
