using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;

namespace POS.Test
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        // Metodo para poder simular la aplicacion en tiempo de ejecucion:
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            // Acceder al archivo appsettings.json para poder acceder a las capas
            builder.ConfigureAppConfiguration(configurationBuilder =>
            {
                var integrationConfiguration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();

                // Anadir la configuracion:
                configurationBuilder.AddConfiguration(integrationConfiguration);
            });
        }
    }
}
