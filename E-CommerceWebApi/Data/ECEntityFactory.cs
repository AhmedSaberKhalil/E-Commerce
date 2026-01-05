using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace E_CommerceWebApi.Data
{
    public class ECEntityFactory : IDesignTimeDbContextFactory<ECEntity>
    {
        public ECEntity CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ECEntity>();

            // This connection string is a dummy! 
            // It only tells EF "I am using SQL Server" so it can generate the right SQL syntax.
            optionsBuilder.UseSqlServer("Server=localhost;Database=Dummy;User Id=sa;Password=Password;TrustServerCertificate=True;");

            return new ECEntity(optionsBuilder.Options);
        }
    }
}