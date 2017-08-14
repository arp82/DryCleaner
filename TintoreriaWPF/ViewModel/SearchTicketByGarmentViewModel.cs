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
    public class SearchTicketByGarmentViewModel
    {
        public RelayCommand SearchTicketByGarmentCommand { get; set; }
        public RelayCommand ExitCommand { get; set; }
        public int GarmentId { get; set; }

        public SearchTicketByGarmentViewModel(Action closeAction)
        {
            SearchTicketByGarmentCommand = new RelayCommand(SearchTicketByGarment);
            ExitCommand = new RelayCommand(closeAction);
        }

        private void SearchTicketByGarment()
        {
            LaundryDb db;
            using (db = new LaundryDb())
            {
                Garment garment = db.Garments.Find(GarmentId);
                if (garment != null)
                {
                    var ticket = db.Tickets.Find(garment.TicketId);
                    MessageBox.Show(string.Format("Garment name:\t '{0}'.\n Ticket phone:\t '{1}'",garment.Name, ticket.Phone));
                }
                else
                {
                    MessageBox.Show(string.Format("The Garment Identifier specified is not stored in the Database"));
                }
            }
        }
    }
}
