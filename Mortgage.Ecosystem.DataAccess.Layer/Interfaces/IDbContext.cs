using Microsoft.EntityFrameworkCore;

namespace Mortgage.Ecosystem.DataAccess.Layer.Interfaces
{
    public interface IDbContext : IDisposable
    {
        DbContext Instance { get; }
    }
}
