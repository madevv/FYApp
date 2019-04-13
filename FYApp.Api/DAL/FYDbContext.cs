using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using FYApp.Core.Models;
using Microsoft.Extensions.Logging;

namespace FYApp.Api.DAL
{
    public class FYDbContext : DbContext
    {
        public DbSet<Household> Households { get; set; }
        public DbSet<Bill> Bills {get;set;}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlite("Filename=FYPCore.db")
                //.UseSqlServer(@"Server=(localdb)\mssqllocaldb; Database = FYPCore; Trusted_Connection=True; ConnectRetryCount=0;")
                .UseLoggerFactory(GetConsoleLoggerFactory()); // enables logging of queries for debugging
        }

        // Convenience method to recreate the database thus ensuring  the new database takes 
        // account of any changes to the Models or DatabaseContext
        public void Initialise()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        // Creates a Sql Query console logger that can be added to context for debugging 
        private static ILoggerFactory GetConsoleLoggerFactory()
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging(builder => builder.AddConsole()
                .AddFilter(DbLoggerCategory.Query.Name, LogLevel.Information)
            );
            return serviceCollection.BuildServiceProvider()
                .GetService<ILoggerFactory>();
        }
    }
}
