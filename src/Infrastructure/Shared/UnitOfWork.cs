using System.Threading.Tasks;
using Sempi5.Domain;
using Sempi5.Domain.Shared;
using Sempi5.Domain.TodoItem;
using Sempi5.Infrastructure.Databases;

namespace Sempi5.Infrastructure.Shared
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DBContext _context;

        public UnitOfWork(DBContext context)
        {
            this._context = context;
        }

        public async Task<int> CommitAsync()
        {
            return await this._context.SaveChangesAsync();
        }
    }
}