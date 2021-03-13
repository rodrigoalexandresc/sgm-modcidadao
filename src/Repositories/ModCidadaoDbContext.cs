using Microsoft.EntityFrameworkCore;
using ModCidadao.Models;

namespace ModCidadao.Repositories {
    public class ModCidadaoDbContext: DbContext {
        public ModCidadaoDbContext(DbContextOptions<ModCidadaoDbContext> options) : base(options)
        {
            
        }

        public DbSet<IPTU> IPTUs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
            
            new IPTUMap(modelBuilder.Entity<IPTU>());
        }
    }
}