using Sempi5.Domain.Shared;
using Sempi5.Domain.User;
using Sempi5.Infrastructure.AccoutToDeleteRepository;

namespace Sempi5.Domain.AccoutToDelete;

public class AccoutToDelete:Entity<SystemUserId>, IAggregateRoot
{
    public SystemUserId Id { get; set; }
    public DateTime DateToDelete { get; set; }

    public AccoutToDelete(SystemUserId id, DateTime dateToDelete)
    {
        Id = id;
        DateToDelete = dateToDelete;
    }
}