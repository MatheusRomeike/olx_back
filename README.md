# Olx

Esse projeto foi feito utilizando a versão .net8.0.

## Configuração
Existe o arquivo .env (.env.local em DEBUG) para conectar-se ao banco de dados. 
Configurar seu path.
Assim que terminar a configuração, rodar o comando "update-database" já que o projeto foi feito com Migrations, você vai ter o banco inteiro criado.

## Adicionar Migrations
Caso precise alterar alguma estrutura de classes, essa mudança precisará ser refletida no banco de dados. 
Após alteração, rodar o comando "Add-Migrations {nome}" para criação da Migration.
Rodar o comando "update-database" para aplicar a Migration no banco de dados configurado de acordo com seu ambiente.
