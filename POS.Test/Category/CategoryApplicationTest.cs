using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using POS.Application.Dtos.Category.Request;
using POS.Application.Interfaces;
using POS.Infrastructure.Persistences.Contexts;
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
            var name = "Nueva categoría prueba";
            var description = "Descripción nueva categoría prueba";
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

        [TestMethod]
        public async Task EditCategory_WhenSendingNullValueOrEmpty_ValidationError()
        {
            using var scope = _scopeFactory?.CreateScope();
            var context = scope?.ServiceProvider.GetService<ICategoryApplication>();

            // Arrange:
            var posContext = scope?.ServiceProvider.GetService<PosContext>();

            await posContext!.Categories.AddAsync(new Domain.Entities.Category
            {
                Name = "Edicion de categoria fallida",
                Description = "Edicion de categoria fallida",
                State = 1,
                AuditCreateDate = DateTime.Now,
                AuditCreateUser = 1
            });

            await posContext.SaveChangesAsync();

            var id = (await posContext.Categories.AsNoTracking().OrderByDescending(c => c.AuditCreateDate).FirstOrDefaultAsync())!.Id;
            var name = "";
            var description = "";
            var state = 1;
            var expected = ReplyMessage.MESSAGE_VALIDATE;

            // Act:
            var result = await context!.EditCategory(id, new CategoryRequestDto()
            {
                Name = name,
                Description = description,
                State = state
            });
            var current = result.Message;

            // Assert:
            Assert.AreEqual(expected, current);
        }

        [TestMethod]
        public async Task EditCategory_WhenSendingCorrectValues_EditedSuccessfully()
        {
            using var scope = _scopeFactory?.CreateScope();
            var context = scope?.ServiceProvider.GetService<ICategoryApplication>();

            // Arrange:
            var posContext = scope?.ServiceProvider.GetService<PosContext>();

            await posContext!.Categories.AddAsync(new Domain.Entities.Category
            {
                Name = "Edicion de categoria",
                Description = "Edicion de categoria",
                State = 1,
                AuditCreateDate = DateTime.Now,
                AuditCreateUser = 1
            });

            await posContext.SaveChangesAsync();

            var id = (await posContext.Categories.AsNoTracking().OrderByDescending(c => c.AuditCreateDate).FirstOrDefaultAsync())!.Id;

            var name = "Edición de categoria exitosa";
            var description = "Edición de categoria exitosa";
            var state = 1;
            var expected = ReplyMessage.MESSAGE_UPDATE;

            // Act:
            var result = await context!.EditCategory(id, new CategoryRequestDto()
            {
                Name = name,
                Description = description,
                State = state
            });
            var current = result.Message;

            // Assert:
            Assert.AreEqual(expected, current);
        }

        [TestMethod]
        public async Task RemoveCategory_WhenCategoryDoesNotExist_ErrorMessage()
        {
            using var scope = _scopeFactory?.CreateScope();
            var context = scope?.ServiceProvider.GetService<ICategoryApplication>();

            // Arrange:
            var posContext = scope?.ServiceProvider.GetService<PosContext>();

            await posContext!.Categories.AddAsync(new Domain.Entities.Category
            {
                Name = "Eliminacion de categoria fallida",
                Description = "Eliminacion de categoria fallida",
                State = 1,
                AuditCreateDate = DateTime.Now,
                AuditCreateUser = 1
            });

            await posContext.SaveChangesAsync();

            var id = (await posContext.Categories.AsNoTracking().OrderByDescending(c => c.AuditCreateDate).FirstOrDefaultAsync())!.Id + 1;
            var expected = ReplyMessage.MESSAGE_DOESNOT_EXIST;

            // Act:
            var result = await context!.RemoveCategory(id);
            var current = result.Message;

            // Arrange:
            Assert.AreEqual(expected, current);
        }

        [TestMethod]
        public async Task RemoveCategory_WhenSendingCorrectValues_RemovedSuccessfully()
        {
            using var scope = _scopeFactory?.CreateScope();
            var context = scope?.ServiceProvider.GetService<ICategoryApplication>();

            // Arrange:
            var posContext = scope?.ServiceProvider.GetService<PosContext>();

            await posContext!.Categories.AddAsync(new Domain.Entities.Category
            {
                Name = "Eliminacion de categoria exitosa",
                Description = "Eliminacion de categoria exitosa",
                State = 1,
                AuditCreateDate = DateTime.Now,
                AuditCreateUser = 1
            });

            await posContext.SaveChangesAsync();

            var id = (await posContext.Categories.AsNoTracking().OrderByDescending(c => c.AuditCreateDate).FirstOrDefaultAsync())!.Id;
            var expected = ReplyMessage.MESSAGE_DELETE;

            // Act:
            var result = await context!.RemoveCategory(id);
            var current = result.Message;

            // Assert:
            Assert.AreEqual(expected, current);
        }
    }
}
