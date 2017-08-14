using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using TintoreriaWPF.Model;

namespace TintoreriaWPF
{
    public class ListDailyEarningsWindowViewModel : INotifyPropertyChanged
    {
        public RelayCommand ExitCommand { get; set; }
        public RelayCommand PerformSearchCommand { get; set; }

        public string DeliveryDate { get; set; }
        public IList<string> DailyEarnings { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public ListDailyEarningsWindowViewModel(Action closeAction)
        {
            ExitCommand = new RelayCommand(closeAction);
            PerformSearchCommand = new RelayCommand(ListByEarnings);
        }

        private void ListByEarnings()
        {
            LaundryDb db;
            using (db = new LaundryDb())
            {
                string deliveryDate = DeliveryDate.Substring(0, 10);
                var dailyEarningsList = db.Tickets.Where(t => t.DeliveryDate == deliveryDate)
                    .Select(t => new
                    {
                        TicketPhone = t.Phone,
                        Income = t.PaidAmount
                    });

                DailyEarnings = new List<string>();
                foreach (var e in dailyEarningsList)
                {
                    string s = string.Format("Earnings for ticket with Phone {0}: {1}", e.TicketPhone, e.Income);
                    DailyEarnings.Add(s);
                }
                RaisePropertyChanged("DailyEarnings");
            }
        }
    }
}
