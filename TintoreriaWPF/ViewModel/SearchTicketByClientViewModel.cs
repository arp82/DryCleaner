using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
using System.Windows;
using TintoreriaWPF.Model;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Collections.Generic;



namespace TintoreriaWPF.ViewModel
{
    public class SearchTicketByClientViewModel : INotifyPropertyChanged
    {
        
        public RelayCommand SearchTicketByClientCommand { get; set; }

        public int GarmentId { get; set; }
        public int TicketPhone { get; set; }
        public IList<string> PhoneTickets { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public SearchTicketByClientViewModel()
        {
            this.SearchTicketByClientCommand = new RelayCommand(SearchTicketByClient);
        }

        private void SearchTicketByClient()
        {
            LaundryDb db;
            using (db = new LaundryDb())
            {
                var ticketsByClient = db.Tickets.Join(db.Garments,
                    t => t.Id,
                    g => g.TicketId,
                    (t, g) => new
                    {
                        TicketId = t.Id,
                        TicketPhone = t.Phone,
                        GarmentName = g.Name
                    }).Where(t => t.TicketPhone == this.TicketPhone);
               
                if (ticketsByClient.Count() != 0)
                {
                    PhoneTickets = new List<string>();
                    foreach (var t in ticketsByClient)
                    {
                        string s = string.Format("Garment {0} with phone {1} ",t.GarmentName, t.TicketPhone);
                        PhoneTickets.Add(s);
                    }
                    RaisePropertyChanged("PhoneTickets");
                }
                else
                {
                    MessageBox.Show("The Garment specified is not stored in the Database");
                }
            }
            
        }
    }
}
