#!/bin/bash
set -e

host="$DB_HOST"
user="root"
password="rootpassword"

# Aguarda o MySQL ficar dispon�vel
until mysql -h "$host" -u "$user" -p"$password" -e "SHOW DATABASES;" > /dev/null 2>&1; do
  echo "Aguardando o banco de dados em $host..."
  sleep 2
done

# Ap�s o banco de dados estar dispon�vel, inicia a aplica��o
echo "Banco de dados dispon�vel. Iniciando a aplica��o."
exec dotnet EmployeeManagement.Api.dll
