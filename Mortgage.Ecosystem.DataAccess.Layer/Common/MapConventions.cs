using Microsoft.EntityFrameworkCore;

namespace Mortgage.Ecosystem.DataAccess.Layer.Common
{
    // The primary key convention, treat the attribute Id as the database primary key
    public class PrimaryKeyConvention
    {
        public static void SetPrimaryKey(ModelBuilder modelBuilder, string entityName)
        {
            modelBuilder.Entity(entityName).HasKey("Id");
        }
    }
}