#!/bin/bash
set -e

echo "Ensuring Database [$DB_NAME] exists"

/opt/mssql-tools18/bin/sqlcmd -S "$DB_HOST,1433" -U "$DB_USER" -P "$DB_PASS" -C -N o \
  -Q "IF DB_ID('$DB_NAME') IS NULL CREATE DATABASE [$DB_NAME];"

echo "--- Step 2: Running Schema Migrations inside [$DB_NAME] ---"

/opt/mssql-tools18/bin/sqlcmd -S "$DB_HOST,1433" -U "$DB_USER" -P "$DB_PASS" \
  -d "$DB_NAME" -i migrate.sql -C -N o

echo "All Migrations Complete"