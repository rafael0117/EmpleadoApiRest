using AutoMapper;
using BackEndApiSistema.DTOs;
using BackEndApiSistema.Models;
using BackEndApiSistema.Services.Contrato;
using BackEndApiSistema.Services.Implementacion;
using BackEndApiSistema.Utilidades;
using Microsoft.EntityFrameworkCore;

namespace APIangular
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            // Inyección de dependencias para servicios
            builder.Services.AddScoped<IDepartamentoService, DepartamentoService>();
            builder.Services.AddScoped<IEmpleadoService, EmpleadoService>();
      

            // Inyección de AutoMapper
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // Configuración de la cadena de conexión
            builder.Services.AddDbContext<DbempleadoContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("cadenaSQL"));
            });

            // Configure Swagger/OpenAPI
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("NuevaPolitica",app => {
                    app.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            }
            );

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();

            #region PETICIONES API REST
            app.MapGet("/departamento/lista", async (
                IDepartamentoService _departamentoServicio,
                IMapper _mapper
            ) =>
            {
                var listaDepartamento = await _departamentoServicio.GetList();
                var listaDepartamentoDTO = _mapper.Map<List<DepartamentoDTO>>(listaDepartamento);

                if (listaDepartamentoDTO.Any()) // Revisamos si hay elementos en la lista de DTO
                {
                    return Results.Ok(listaDepartamentoDTO);
                }
                else
                {
                    return Results.NotFound(); // Aseguramos un retorno explícito en esta rama.
                }
            });
            app.MapGet("/empleado/lista", async (
               IEmpleadoService _empleadoServicio,
               IMapper _mapper
           ) =>
            {
                var listaEmpleado = await _empleadoServicio.GetList();
                var listaEmpleadoDTO = _mapper.Map<List<EmpleadoDTO>>(listaEmpleado);

                if (listaEmpleadoDTO.Any()) // Revisamos si hay elementos en la lista de DTO
                {
                    return Results.Ok(listaEmpleadoDTO);
                }
                else
                {
                    return Results.NotFound(); // Aseguramos un retorno explícito en esta rama.
                }
            });

            app.MapPost("/empleado/guardar", async(
               EmpleadoDTO modelo,
               IEmpleadoService _empleadoServicio,
               IMapper _mapper
                ) => {
                var _empleado = _mapper.Map<Empleado>(modelo);
                    var _empleadoCreado = await _empleadoServicio.Add(_empleado);
            
                    if(_empleadoCreado.IdEmpleado != 0)
                        return Results.Ok(_mapper.Map<EmpleadoDTO>(_empleadoCreado));
                    else
                        return Results.StatusCode(StatusCodes.Status500InternalServerError);
            });
            app.MapPut("/empleado/actualizar/{idEmpleado}", async (
               int idEmpleado,
                EmpleadoDTO modelo,
               IEmpleadoService _empleadoServicio,
               IMapper _mapper) 
               => {
                   var _encontrado = await _empleadoServicio.Get(idEmpleado);
                   if(_encontrado is null) return Results.NotFound();
                   var _empleado =_mapper.Map<Empleado>(modelo);
                   _encontrado.NombreCompleto = _empleado.NombreCompleto;
                   _encontrado.IdDepartamento = _empleado.IdDepartamento;
                   _encontrado.Sueldo = _empleado.Sueldo;
                   _encontrado.FechaContrato = _empleado.FechaContrato;

                   var respuesta = await _empleadoServicio.Update(_encontrado);

                   if(respuesta)
                       return Results.Ok(_mapper.Map<EmpleadoDTO>(_encontrado));
                   else
                       return Results.StatusCode(StatusCodes.Status500InternalServerError);

               });
            app.MapDelete("/empleado/eliminar/{idEmpleado}", async (
               int idEmpleado,
               IEmpleadoService _empleadoServicio
                ) => {
                    var _encontrado = await _empleadoServicio.Get(idEmpleado);
                    if(_encontrado is null)return Results.NotFound();   
                    var respuesta = await _empleadoServicio.Delete(_encontrado);
                    if(respuesta)
                        return Results.Ok();
                    else
                        return Results.StatusCode(StatusCodes.Status500InternalServerError);
                });
            #endregion
            app.UseCors("NuevaPolitica");
            app.Run();
        }
    }
}
