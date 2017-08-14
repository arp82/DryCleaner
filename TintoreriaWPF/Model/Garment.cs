namespace TintoreriaWPF.Model
{
    public class Garment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ServiceType { get; set; }
        public string Color { get; set; }
        public double PriceTag { get; set; }
        public string GarmentType { get; set; }
        public Ticket Ticket { get; set; }
        public int TicketId { get; set; }
        public bool Paid { get; set; }
    }
}
