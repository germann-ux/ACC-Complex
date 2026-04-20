using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ACC.Data;

public class ACCDbContextFactory : IDesignTimeDbContextFactory<ACCDbContext>
{
    public ACCDbContext CreateDbContext(string[] args)
    {
        var connectionString = Environment.GetEnvironmentVariable("ACC_DESIGNTIME_CONNECTION");
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            connectionString = "Server=(localdb)\\mssqllocaldb;Database=ACCAcademicDesignTime;Trusted_Connection=True;TrustServerCertificate=True;";
        }

        var optionsBuilder = new DbContextOptionsBuilder<ACCDbContext>();
        optionsBuilder.UseSqlServer(connectionString);

        return new ACCDbContext(optionsBuilder.Options);
    }
}
