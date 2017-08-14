using System.Data.Entity;

namespace TintoreriaWPF.Model
{
    public class LaundryDb : DbContext
    {
        public DbSet<Garment> Garments { get; set; }
        public DbSet<Ticket> Tickets { get; set; }

        public LaundryDb()
            : base("Laundry")
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new GarmentConfigurator());
            modelBuilder.Configurations.Add(new TicketConfigurator());
            base.OnModelCreating(modelBuilder);
        }
    }
}