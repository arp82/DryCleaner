using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace TintoreriaWPF.Model
{
    public class TicketConfigurator : EntityTypeConfiguration<Ticket>
    {
        public TicketConfigurator()
        {
            ToTable("Tickets");
            HasKey(t => t.Id);
            Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}
       
