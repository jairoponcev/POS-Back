using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using POS.Application.Dtos.Request;
using POS.Application.Interfaces;
using POS.Utilities.Static;

namespace POS.Test.Category
{
    [TestClass] // Declarar la clase como una clase para tests
    public class CategoryApplicationTest
    {
        // Declarar variables para ayudarnos con la inyeccion de dependencias:
        private static WebApplicationFactory<Program>? _factory = null;
        private static IServiceScopeFactory? _scopeFactory = null;

        // Metodo que se inicializara antes de todos los metodos de pueba:
        // Ininicializara todos los servicios
        [ClassInitialize]
        public static void Initialize(TestContext _testContext)
        {
            _factory = new CustomWebApplicationFactory();
            _scopeFactory = _factory.Services.GetRequiredService<IServiceScopeFactory>();
        }

        // Metodos para probar la aplicacion. Se puede usar la siguiente convencion:
        // MetodoAProbar_EscenarioAProbar_RespuestaEsperada
        [TestMethod]
        public async Task RegisterCategory_WhenSendingNullValueOrEmpty_ValidationError()
        {
            using var scope = _scopeFactory?.CreateScope();
            var context = scope?.ServiceProvider.GetService<ICategoryApplication>();

            // Arrange: Indica que vamos a preparar una solicitud
            var name = "";
            var description = "";
            var state = 1;
            var expected = ReplyMessage.MESSAGE_VALIDATE;

            // Act: Cuando se envia la solicitud y se recibe la respuesta
            var result = await context!.RegisterCategory(new CategoryRequestDto()
            {
                Name = name,
                Description = description,
                State = state
            });
            var current = result.Message;

            // Assert: Se va a evaluar si la respuesta es la esperada
            Assert.AreEqual(expected, current);
        }

        [TestMethod]
        public async Task RegisterCategory_WhenSendingCorrectValues_RegisteredSuccessfully()
        {
            using var scope = _scopeFactory?.CreateScope();
            var context = scope?.ServiceProvider.GetService<ICategoryApplication>();

            // Arrange:
            var name = "Nuevo registro";
            var description = "Nueva descripcion";
            var state = 1;
            var expected = ReplyMessage.MESSAGE_SAVE;

            // Act:
            var result = await context!.RegisterCategory(new CategoryRequestDto()
            {
                Name = name,
                Description = description,
                State = state
            });
            var current = result.Message;

            // Assert:
            Assert.AreEqual(expected, current);
        }
    }
}
