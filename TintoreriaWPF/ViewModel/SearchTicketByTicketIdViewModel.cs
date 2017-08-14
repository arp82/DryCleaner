using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TintoreriaWPF.Model;

namespace TintoreriaWPF.ViewModel
{
    public class SearchTicketByTicketIdViewModel : INotifyPropertyChanged
    {
        
        public RelayCommand SearchTicketByTicketIdCommand { get; set; }

        public int TicketId { get; set; }
        public IList<string> IdTickets { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public SearchTicketByTicketIdViewModel()
        {
            this.SearchTicketByTicketIdCommand = new RelayCommand(SearchTicketByTicketId);
        }

        private void SearchTicketByTicketId()
        {
            LaundryDb db;
            using (db = new LaundryDb())
            {
                var tickets = db.Tickets.Join(db.Garments,
                    t => t.Id,
                    g => g.TicketId,
                    (t, g) => new
                    {
                        TicketId = t.Id,
                        TicketPhone = t.Phone,
                        GarmentName = g.Name
                    }).Where(t => t.TicketId == this.TicketId);

                if (tickets.Count() != 0)
                {
                    IdTickets = new List<string>();
                    foreach (var t in tickets)
                    {
                        string s = string.Format("Garment Name:{0} with ticket Id:{1} and Phone:{2}", t.GarmentName, t.TicketId, t.TicketPhone);
                        IdTickets.Add(s);                     
                    }
                    RaisePropertyChanged("IdTickets");
                }
                else
                {
                    MessageBox.Show("The Ticket identifier specified is not stored in the Database");
                }

            }
        }
    }
}
