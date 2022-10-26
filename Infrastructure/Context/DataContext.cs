using Microsoft.EntityFrameworkCore;
using Model.Entities;

namespace Infrastructure.Context
{
    public class DataContext : DbContext
    {
        public DataContext()
        {

        }
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }


        public DbSet<Faq> Faqs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //modelBuilder.Entity<AllMessage>().

            var cascadeFKs = modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
                fk.DeleteBehavior = DeleteBehavior.Restrict;
            modelBuilder.Entity<Faq>().HasData(new Faq()
            {
                Answer = "Answer",
                CreateDate = DateTime.Now,
                Id = Guid.NewGuid(),
                IsRemoved = false,
                Updatetime = DateTime.Now,
                Question = "Question"
            });

            modelBuilder.Entity<Faq>()
                .HasQueryFilter(u => !u.IsRemoved);

            base.OnModelCreating(modelBuilder);
        }
    }
}
