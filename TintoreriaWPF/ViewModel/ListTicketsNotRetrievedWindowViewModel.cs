using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using TintoreriaWPF.Model;

namespace TintoreriaWPF
{
    public class ListTicketsNotRetrievedWindowViewModel : INotifyPropertyChanged
    {
        public RelayCommand ExitCommand { get; set; }
        public RelayCommand PerformSearchCommand { get; set; }

        public string DeliveryDate { get; set; }
        public IList<string> TicketsNotRetrieved { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public ListTicketsNotRetrievedWindowViewModel(Action closeAction)
        {
            ExitCommand = new RelayCommand(closeAction);
            PerformSearchCommand = new RelayCommand(ListTicketsNotRetrieved);
        }

        private void ListTicketsNotRetrieved()
        {
            LaundryDb db;
            using (db = new LaundryDb())
            {
                string deliveryDate = DeliveryDate.Substring(0, 10);
                var ticketsNotRetrieved = db.Tickets.Select(t => new
                    {
                        TicketPhone = t.Phone,
                        TicketDeliveryDate = t.DeliveryDate
                    })
                    .AsEnumerable()
                    .Where(t => HasExpired(t.TicketDeliveryDate, deliveryDate));

                TicketsNotRetrieved = new List<string>();
                foreach (var t in ticketsNotRetrieved)
                {
                    string s = string.Format("Ticket with Phone {0} should have been retrieved on {1}", t.TicketPhone, t.TicketDeliveryDate);
                    TicketsNotRetrieved.Add(s);
                }
                RaisePropertyChanged("TicketsNotRetrieved");
            }
        }



        private bool HasExpired(string dateToCheck, string currentDate)
        {
            if ((dateToCheck == null) || (currentDate == null))
                return false;
            int day = int.Parse(dateToCheck.Substring(0, 2));
            int month = int.Parse(dateToCheck.Substring(3, 2));
            int year = int.Parse(dateToCheck.Substring(6, 4));
            int dayR = int.Parse(currentDate.Substring(0, 2));
            int monthR = int.Parse(currentDate.Substring(3, 2));
            int yearR = int.Parse(currentDate.Substring(6, 4));
            if (year < yearR)
                return true;
            else if ((year == yearR) && (month < monthR))
                return true;
            else if ((year == yearR) && (month == monthR) && (day < dayR))
                return true;
            return false;
        }
    }
}
