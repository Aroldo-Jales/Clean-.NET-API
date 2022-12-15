using Microsoft.EntityFrameworkCore;
using Prova1.Domain.Entities.Authentication;
using Prova1.Domain.Entities.Readings;

namespace Prova1.Infrastructure.Database
{
    public class AppDbContext : DbContext
    {
        // Authentication
        public DbSet<User>? Users { get; set; }
        public DbSet<UserValidationCode>? UserValidationCodes { get; set; }
        public DbSet<RefreshToken>? RefreshTokens { get; set; }

        // Reading
        public DbSet<Reading>? Readings { get; set; }
        public DbSet<Annotation>? Annotations { get; set; }
        public DbSet<Commentary>? Commentaries { get; set; }
        public DbSet<Like>? Likes { get; set; }
        public DbSet<Tag>? Tags { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // FOR APPLICATION RUNNING USE AppDirectories.getDatabasePath
            //
            //optionsBuilder.UseSqlite(connectionString: String.Format(@"DataSource={0}; Cache=Shared", AppDirectories.getDatabasePath));

            // FOR MIGRATIONS AND DATABASE UPDATES USE ABSOLUTE PATH
            //
            optionsBuilder.UseSqlite(connectionString: String.Format(@"DataSource={0}; Cache=Shared", @"C:\Users\Aroldo Jales\Documents\Code\Vscode\IFPI\PROVA1\Prova1\Prova1.Infrastructure\Database\SQLite\Database.db"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Authentication
            modelBuilder.Entity<UserValidationCode>().Property(u => u.Id).ValueGeneratedOnAdd();         
            modelBuilder.Entity<RefreshToken>().Property(r => r.Id).ValueGeneratedOnAdd();

            // Reading
            modelBuilder.Entity<Reading>().Property(r => r.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Annotation>().Property(a => a.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Commentary>().Property(c => c.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Like>().Property(l => l.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Tag>().Property(t => t.Id).ValueGeneratedOnAdd();
        }
    }
}

        /* - COMMANDS
                - MIGRATIONS
                    dotnet ef migrations add InitialMigration --project .\Prova1.Infrastructure\ -o Database/SQLite/AppMigrations

                - UPDATE DATABASE (AFTER EACH MIGRATION)
                    dotnet ef database update --project .\Prova1.Infrastructure\
        */
        
        // Authentication