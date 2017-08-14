using System.Collections.Generic;

namespace TintoreriaWPF.Model
{
    public class Ticket
    {
        public int Id { get; set; }
        public int Phone { get; set; }
        public string DeliveryDate { get; set; }
        public double PaidAmount { get; set; }
        public double TotalAmount { get; set; }
        public IList<Garment> Garments { get; set; }
    }
}
       