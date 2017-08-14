using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TintoreriaWPF.ViewModel;

namespace TintoreriaWPF
{
    public class ReportsWindowViewModel
    {
        public RelayCommand ListTicketsNotRetrievedCommand { get; set; }
        public RelayCommand ListBestClientsCommand { get; set; }
        public RelayCommand ListDailyEarningsCommand { get; set; }
        public RelayCommand ListEarningsByGarmentAndGarmentTypeCommand { get; set; }
        public RelayCommand ListTicketsNotFullyPaidCommand { get; set; }

        public ReportsWindowViewModel()
        {
            ListTicketsNotRetrievedCommand = new RelayCommand(ListTicketsNotRetrieved);
            ListBestClientsCommand = new RelayCommand(ListBestClients);
            ListDailyEarningsCommand = new RelayCommand(ListDailyEarnings);
            ListEarningsByGarmentAndGarmentTypeCommand = new RelayCommand(ListEarningsByGarmentAndGarmentType);
            ListTicketsNotFullyPaidCommand = new RelayCommand(ListTicketsNotFullyPaid);
        }

        private void ListDailyEarnings()
        {
            var window = new ListDailyEarningsWindow();
            window.InitAndShow();
        }

        private void ListBestClients()
        {
            var window = new ListBestClientsWindow();
            window.InitAndShow();
        }

        private void ListTicketsNotRetrieved()
        {
            var window = new ListTicketsNotRetrievedWindow();
            window.InitAndShow();
        }

        private void ListEarningsByGarmentAndGarmentType()
        {
            var window = new ListEarningsByGarmentAndGarmentTypeWindow();
            window.InitAndShow();
        }

        private void ListTicketsNotFullyPaid()
        {
            var window = new ListTicketsNotFullyPaidWindow();
            window.InitAndShow();
        }
    }
}
