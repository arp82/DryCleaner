using System.Windows;
using GalaSoft.MvvmLight.Command;

namespace TintoreriaWPF.ViewModel
{
    public class MainViewModel
    {
        public RelayCommand CollectCommand { get; private set; }
        public RelayCommand DeliveryCommand { get; private set; }
        public RelayCommand SearchTicketsCommand { get; private set; }
        public RelayCommand OpenReportsCommand { get; private set; }

        public MainViewModel()
        {
            CollectCommand = new RelayCommand(ShowCollectWindow);
            DeliveryCommand = new RelayCommand(ShowDelivery);
            SearchTicketsCommand = new RelayCommand(ShowSearchTickets);
            OpenReportsCommand = new RelayCommand(ShowReports);
        }
        private void ShowCollectWindow()
        {
            CollectWindow collectWindow = new CollectWindow();
            collectWindow.Show();
        }
        private void ShowDelivery()
        {
            DeliveryWindow deliveryWindow = new DeliveryWindow();
            deliveryWindow.Show();
        }
        private void ShowSearchTickets()
        {
            SearchTicketsWindow searchTicketsWindow = new SearchTicketsWindow();
            searchTicketsWindow.InitAndShow();
        }
        private void ShowReports()
        {
            ReportsWindow reportsWindow = new ReportsWindow();
            reportsWindow.InitAndShow();
        }
    }
}