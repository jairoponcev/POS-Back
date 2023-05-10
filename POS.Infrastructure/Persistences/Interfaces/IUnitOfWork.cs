using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Infrastructure.Persistences.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        // Declaracion o matricula de nuestras interfaces a nivel de repository
        ICategoryRepository Category { get; }
        IUserRepository User { get; }
        void SaveChanges();
        Task SaveChangesAsync();
    }
}
