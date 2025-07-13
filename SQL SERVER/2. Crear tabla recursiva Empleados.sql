USE EmpresaDB;
GO
CREATE TABLE Puesto (
    CodigoPuesto INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL
);


CREATE TABLE Empleado (
    Codigo INT IDENTITY(1,1) PRIMARY KEY,
    CodigoPuesto INT NOT NULL,
    Nombre VARCHAR(100) NOT NULL,
    CodigoJefe INT NULL,
    FOREIGN KEY (CodigoJefe) REFERENCES Empleado(Codigo),
    FOREIGN KEY (CodigoPuesto) REFERENCES Puesto(CodigoPuesto)
);


INSERT INTO Puesto (Nombre) VALUES 
('Gerente'),
('Sub Gerente'),
('Supervisor')

INSERT INTO Empleado (CodigoPuesto, Nombre, CodigoJefe) VALUES 
(1, 'Pedro', NULL),
