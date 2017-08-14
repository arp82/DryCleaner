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
    public class SearchTicketByColorViewModel: INotifyPropertyChanged
    {

        public RelayCommand SearchTicketByColorCommand { get; set; }
        

        public int GarmentId { get; set; }
        public string GarmentColor { get; set; }
        public IList<string> ColorTickets { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public SearchTicketByColorViewModel()
        {
            this.SearchTicketByColorCommand = new RelayCommand(SearchTicketByColor);
        }

        private void SearchTicketByColor()
        {
            LaundryDb db;
            using (db = new LaundryDb())
            {
                var ticketsOfInterest = db.Tickets.Join(db.Garments,
                    t => t.Id,
                    g => g.TicketId,
                    (t, g) => new
                    {
                        TicketId = t.Id,
                        TicketPhone = t.Phone,
                        GarmentColor = g.Color
                    }).Where(t => t.GarmentColor.ToUpper() == this.GarmentColor.ToUpper());
               
                if (ticketsOfInterest.Count() != 0)
                {
                    ColorTickets = new List<string>();
                    foreach (var t in ticketsOfInterest)
                    {
                        string s = string.Format("Tickets {0} with color: {1}", t.TicketPhone, t.GarmentColor);

                        ColorTickets.Add(s);
                    }
                    RaisePropertyChanged("ColorTickets");
                }
                else
                {
                    MessageBox.Show("The Garment colour specified is not stored in the Database");
                }
            }
            
        }

    }
}
