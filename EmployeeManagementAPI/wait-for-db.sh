#!/bin/bash
set -e

host="$DB_HOST"
user="root"
password="rootpassword"

# Aguarda o MySQL ficar disponível
until mysql -h "$host" -u "$user" -p"$password" -e "SHOW DATABASES;" > /dev/null 2>&1; do
  echo "Aguardando o banco de dados em $host..."
  sleep 2
done

# Após o banco de dados estar disponível, inicia a aplicação
echo "Banco de dados disponível. Iniciando a aplicação."
exec dotnet EmployeeManagement.Api.dll
