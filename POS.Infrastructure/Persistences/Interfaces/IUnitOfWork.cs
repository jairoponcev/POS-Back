using POS.Infrastructure.FileStorage;

namespace POS.Infrastructure.Persistences.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        // Declaracion o matricula de nuestras interfaces a nivel de repository
        ICategoryRepository Category { get; }

        IUserRepository User { get; }

        IAzureStorage Storage { get; }

        void SaveChanges();

        Task SaveChangesAsync();
    }
}
