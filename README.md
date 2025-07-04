Create Database DBEmpleado;
Use DBEmpleado

CREATE TABLE Departamento (
  IdDepartamento INT IDENTITY(1,1) PRIMARY KEY,
  Nombre VARCHAR(50),
  FechaCreacion DATETIME DEFAULT GETDATE()
);

CREATE TABLE Empleado (
  IdEmpleado INT IDENTITY(1,1) PRIMARY KEY,
  NombreCompleto VARCHAR(50),
  IdDepartamento INT,
  Sueldo INT,
  FechaContrato DATETIME,
  FechaCreacion DATETIME DEFAULT GETDATE(),
  FOREIGN KEY (IdDepartamento) REFERENCES Departamento(IdDepartamento)
);
