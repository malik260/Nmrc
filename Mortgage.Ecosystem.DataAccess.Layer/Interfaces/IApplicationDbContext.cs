using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Mortgage.Ecosystem.DataAccess.Layer.Interfaces
{
    public interface IApplicationDbContext : IDbContext
    {
        DbSet<UserEntity>? User { get; set; }
    }
}
