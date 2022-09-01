using Microsoft.EntityFrameworkCore;
using Prova1.Api.Domain;
using Prova1.Api.Infrastructure.Database.Helpers;

namespace Prova1.Api.Infrastructure.Database
{
    public class AppDbContext : DbContext
    {
        /* - COMMANDS
                - MIGRATIONS
                    dotnet ef migrations add <MigrationName> --project .\Prova1.Api.Infrastructure\ -o DataBase/SQLite/Migrations

                - UPDATE DATABASE (AFTER EACH MIGRATION)
                    dotnet ef database update
        */
        public DbSet<User>? Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {                        
            optionsBuilder.UseSqlite(connectionString: String.Format(@"DataSource={0}; Cache=Shared", AppDirectories.getDatabasePath));
        }
    }
}