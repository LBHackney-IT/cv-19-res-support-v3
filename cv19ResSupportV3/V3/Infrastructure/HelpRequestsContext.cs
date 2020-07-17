using cv19ResRupportV3.V3.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace cv19ResRupportV3.V3.Infrastructure
{
    public class HelpRequestsContext : DbContext
    {
        public HelpRequestsContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<HelpRequestEntity> HelpRequestEntities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HelpRequestEntity>()
                .HasKey(helpRequest => new
                {
                    helpRequest.Id
                });
        }
    }
}
