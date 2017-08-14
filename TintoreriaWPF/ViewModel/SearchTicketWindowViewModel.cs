using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TintoreriaWPF.Model;

namespace TintoreriaWPF.ViewModel
{
    public class SearchTicketsWindowViewModel
    {
        public RelayCommand SearchTicketsCommand { get; set; }
        public RelayCommand SearchColorsCommand { get; set; }
        public RelayCommand SearchClientsCommand { get; set; }
        public RelayCommand SearchTicketsIdCommand { get; set; }

        public SearchTicketsWindowViewModel()
        {
            this.SearchTicketsCommand = new RelayCommand(this.OpenTicketSearchWindow);
            this.SearchColorsCommand = new RelayCommand(this.OpenColorsSearchWindow);
            this.SearchClientsCommand = new RelayCommand(this.OpenClientsSearchWindow);
            this.SearchTicketsIdCommand = new RelayCommand(this.OpenTicketsIdWindow);
        }

        private void OpenTicketSearchWindow()
        {          
            SearchTicketByGarmentWindow window = new SearchTicketByGarmentWindow();
            window.InitAndShow();
        }

        private void OpenColorsSearchWindow() 
        {
            SearchTicketByColourWindow window = new SearchTicketByColourWindow();
            window.Show();
        }

        private void OpenClientsSearchWindow()
        {
            SearchTicketByClientWindow window = new SearchTicketByClientWindow();
            window.Show();
        }

        private void OpenTicketsIdWindow()
        {
            SearchTicketByTicketIdWindow window = new SearchTicketByTicketIdWindow();
            window.Show();
        }

    }
}
