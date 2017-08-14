using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using TintoreriaWPF.Model;

namespace TintoreriaWPF
{
    public class ListTicketsNotFullyPaidWindowViewModel : INotifyPropertyChanged
    {
        public RelayCommand ExitCommand { get; set; }

        public IList<string> TicketsNotYetPaid { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public ListTicketsNotFullyPaidWindowViewModel(Action closeAction)
        {
            ExitCommand = new RelayCommand(closeAction);
            PerformSearch();
        }

        private void PerformSearch()
        {
            LaundryDb db;
            using (db = new LaundryDb())
            {
                var ticketsNotFullyPaid = db.Tickets.Where(t => t.PaidAmount < t.TotalAmount);

                TicketsNotYetPaid = new List<string>();
                foreach (var t in ticketsNotFullyPaid)
                {
                    string s = string.Format("Ticket ID {0} with Phone {1} still has an unpaid amount of {2}", t.Id, t.Phone, t.TotalAmount - t.PaidAmount);
                    TicketsNotYetPaid.Add(s);
                }
                RaisePropertyChanged("TicketsNotYetPaid");
            }
        }
    }
}
