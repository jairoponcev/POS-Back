using Microsoft.EntityFrameworkCore;
using POS.Domain.Entities;
using POS.Infrastructure.Persistences.Contexts;
using POS.Infrastructure.Persistences.Interfaces;

namespace POS.Infrastructure.Persistences.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly PosContext _context;

        public UserRepository(PosContext context)
            :base(context)
        {
            _context = context;
        }

        public async Task<User> UserByEmail(string email)
        {
            var user = await _context.Users.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Email!.Equals(email));

            return user!;
        }
    }
}
