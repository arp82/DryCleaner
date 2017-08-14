using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace TintoreriaWPF.Model
{
    public class GarmentConfigurator : EntityTypeConfiguration<Garment>
    {
        public GarmentConfigurator()
        {
            ToTable("Garments");
            HasKey(c => c.Id);
            Property(c => c.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(c => c.Name).IsRequired();
            HasRequired(c => c.Ticket)
                .WithMany(t => t.Garments); 
        }
    }
}
       