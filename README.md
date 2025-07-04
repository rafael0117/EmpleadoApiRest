<h2>🗃️ Descripción del Proyecto: <strong>EmpleadoApiRest</strong></h2>

<p>
  <strong>EmpleadoApiRest</strong> es una <strong>API REST</strong> sencilla desarrollada en <code>C# con .NET</code>, diseñada para ofrecer servicios básicos de gestión de empleados.
  Este backend se comunica directamente con un frontend desarrollado en <code>Angular</code> mediante peticiones HTTP.
</p>

<h3>📦 Entidades principales</h3>
<ul>
  <li>
    <strong>Departamento:</strong> contiene los campos <code>IdDepartamento</code>, <code>Nombre</code> y <code>FechaCreacion</code>.
  </li>
  <li>
    <strong>Empleado:</strong> contiene los campos <code>IdEmpleado</code>, <code>NombreCompleto</code>, <code>IdDepartamento</code>, <code>Sueldo</code>, <code>FechaContrato</code> y <code>FechaCreacion</code>. Se relaciona con la tabla <strong>Departamento</strong> mediante una clave foránea.
  </li>
</ul>

<h3>🛠️ Operaciones CRUD</h3>
<ul>
  <li>Crear, leer, actualizar y eliminar registros de empleados y departamentos.</li>
  <li>Interfaz RESTful con endpoints organizados por entidad.</li>
  <li>Conexión a base de datos por cadena de conexión SQL Server.</li>
</ul>

<h3>🔒 Manejo de errores</h3>
<ul>
  <li>Validación de datos y control de excepciones.</li>
  <li>Manejo de errores en conexiones SQL y servicios (por ejemplo, método <code>GetList()</code>).</li>
</ul>

<h3>📄 Swagger UI integrado</h3>
<ul>
  <li>Permite probar todos los endpoints disponibles desde el navegador.</li>
  <li>Rutas como <code>/api/Empleado</code> y <code>/api/Departamento</code>.</li>
</ul>

<h3>📂 Estructura limpia y modular</h3>
<ul>
  <li>Separación de lógica en capas: <code>Service</code>, <code>Repository</code>, <code>Controller</code>.</li>
  <li>Modelo de entidad mapeado a base de datos.</li>
  <li>Fácil de mantener y escalar.</li>
</ul>

<h3>🧩 Base de datos (SQL Server)</h3>

<pre><code>
CREATE DATABASE DBEmpleado;
USE DBEmpleado;

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
</code></pre>

<h3>🔗 Conexión con Angular</h3>
<ul>
  <li>El frontend en Angular se conecta a esta API usando servicios HTTP.</li>
  <li>Permite visualizar, agregar, editar y eliminar empleados desde la interfaz web.</li>
</ul>
