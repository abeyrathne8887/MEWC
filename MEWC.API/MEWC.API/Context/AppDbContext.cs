using MEWC.API.Doamin.Application;
using MEWC.API.Doamin.Module;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace MEWC.API.Context
{
    public class AppDbContext:DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {}

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var assembly = typeof(User).GetTypeInfo().Assembly;
            modelBuilder.AddEntityConfigurationsFromAssembly(assembly);
            modelBuilder.AddEntityConfigurationsFromAssembly(GetType().GetTypeInfo().Assembly);
           
        }
      
    }
}
