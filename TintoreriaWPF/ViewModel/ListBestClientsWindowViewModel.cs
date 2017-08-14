using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using TintoreriaWPF.Model;

namespace TintoreriaWPF
{
    public class ListBestClientsWindowViewModel : INotifyPropertyChanged
    {
        public RelayCommand ExitCommand { get; set; }
        public RelayCommand ListByNumberOfGarmentsCommand { get; set; }
        public RelayCommand ListByPaidAmountCommand { get; set; }

        public IList<string> BestClients { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public ListBestClientsWindowViewModel(Action closeAction)
        {
            ExitCommand = new RelayCommand(closeAction);
            ListByNumberOfGarmentsCommand = new RelayCommand(ListByNumberOfGarments);
            ListByPaidAmountCommand = new RelayCommand(ListByPaidAmount);
        }

        private void ListByPaidAmount()
        {
            LaundryDb db;
            using (db = new LaundryDb())
            {
                var clientsOrdered = db.Tickets.OrderByDescending(t => t.PaidAmount)
                    .Select(t => new
                    {
                        ClientPhone = t.Phone,
                        ClientPaidAmount = t.PaidAmount
                    });

                BestClients = new List<string>();
                foreach (var c in clientsOrdered)
                {
                    string s = string.Format("Client with Phone {0} has paid us {1}", c.ClientPhone, c.ClientPaidAmount);
                    BestClients.Add(s);
                }
                RaisePropertyChanged("BestClients");
            }
        }

        private void ListByNumberOfGarments()
        {
            LaundryDb db;
            using (db = new LaundryDb())
            {
                var clientsOrdered = db.Tickets.Select(t => new
                    {
                        ClientPhone = t.Phone,
                        ClientNumberOfGarments = t.Garments.Count()
                    })
                    .OrderByDescending(c => c.ClientNumberOfGarments);

                BestClients = new List<string>();
                foreach (var c in clientsOrdered)
                {
                    string s = string.Format("Client with Phone {0} has registered {1} garments", c.ClientPhone, c.ClientNumberOfGarments);
                    BestClients.Add(s);
                }
                RaisePropertyChanged("BestClients");
            }
        }
    }
}
