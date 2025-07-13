-- Crear la base de datos
CREATE DATABASE EmpresaDB;
GO

USE EmpresaDB;
GO

-- Crear un login a nivel de servidor
CREATE LOGIN EmpleadoUserLogin WITH PASSWORD = 'Password123!';
GO

-- Crear usuario en esta base de datos
CREATE USER EmpleadoUser FOR LOGIN EmpleadoUserLogin;
GO

-- Darle permisos necesarios
ALTER ROLE db_owner ADD MEMBER EmpleadoUser;
GO