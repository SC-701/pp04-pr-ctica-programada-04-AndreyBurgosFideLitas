using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Flujo;
using DA;
using Flujo;
using Reglas;
using Servicios;
using Vehiculo.Abstracciones.Interfaces.DA;
using Vehiculo.Abstracciones.Interfaces.Flujo;
using Vehiculo.Abstracciones.Interfaces.Reglas;
using Vehiculo.Abstracciones.Interfaces.Servicios;
using Vehiculo.DA;
using Vehiculo.DA.Repositorios;
using Vehiculo.Flujo;

namespace Vehiculo.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddHttpClient();
            builder.Services.AddHttpClient("ServicioRevision");
            builder.Services.AddHttpClient("ServicioRegistro");

            builder.Services.AddScoped<IConfiguracion, Configuracion>();

            builder.Services.AddScoped<IRepositorioDapper, RepositorioDapper>();

            builder.Services.AddScoped<IMarcaFlujo, MarcaFlujo>();
            builder.Services.AddScoped<IMarcaDA, MarcaDA>();

            builder.Services.AddScoped<IModeloFlujo, ModeloFlujo>();
            builder.Services.AddScoped<IModeloDA, ModeloDA>();

            builder.Services.AddScoped<IRevisionServicio, RevisionServicio>();
            builder.Services.AddScoped<IRegistroServicio, RegistroServicio>();

            builder.Services.AddScoped<IRevisionReglas, RevisionReglas>();
            builder.Services.AddScoped<IRegistroReglas, RegistroReglas>();

            builder.Services.AddScoped<IVehiculoFlujo, VehiculoFlujo>();
            builder.Services.AddScoped<IVehiculoDA, VehiculoDA>();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
