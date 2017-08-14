using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TintoreriaWPF.Model;

namespace TintoreriaWPF.ViewModel
{
    class DeliveryViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public RelayCommand DeliverSelectedGarmentCommand { get; set; }
        public RelayCommand DeliverAllGarmentsCommand { get; set; }
        public RelayCommand PerformPaymentCommand { get; set; }
        public RelayCommand ShowTicketCommand { get; set; }
        public RelayCommand LoadGarmentsCommand { get; set; }

        private ObservableCollection<Garment> _garments;
        private ObservableCollection<Ticket> _tickets;

        public int PhoneToSearch { get; set; }
        public double Payment {get; set;}
        public Ticket SelectedTicket { get; set; }
        public Garment SelectedGarment { get; set; }

        public ObservableCollection<Garment> Garments
        {
            get
            {
                return _garments;
            }
            set
            {
                _garments = value;
                OnPropertyChanged("Garments");
            }
        }

        public ObservableCollection<Ticket> Tickets
        {
            get
            {
                return _tickets;
            }
            set
            {
                _tickets = value;
                OnPropertyChanged("Tickets");
            }
        }


        public DeliveryViewModel()
        {
            DeliverAllGarmentsCommand = new RelayCommand(DeliverAllGarments);
            DeliverSelectedGarmentCommand = new RelayCommand(DeliverSelectedGarment);
            ShowTicketCommand = new RelayCommand(ShowTicket);
            PerformPaymentCommand = new RelayCommand(PerformPayment);
            LoadGarmentsCommand = new RelayCommand(LoadGarments);
        }

        private void LoadGarments()
        {
            if (SelectedTicket != null)
            {
                _garments = new ObservableCollection<Garment>();
                LaundryDb db;
                using (db = new LaundryDb())
                {
                    var garments = db.Garments.Where(g => g.TicketId == SelectedTicket.Id);
                    foreach (var g in garments)
                    {
                        _garments.Add(g);
                    }
                }
                OnPropertyChanged("Garments");
            }
            else
            {
                MessageBox.Show("Please, pick a Ticket first");
            }
        }

        private void PerformPayment()
        {
            if (SelectedTicket!=null)
            {
                if (Payment<0)
                {
                    MessageBox.Show("Please, input a non-negative value");
                }
                else
                {
                    LaundryDb db;
                    double payment = Payment;
                    double remanent = SelectedTicket.TotalAmount - SelectedTicket.PaidAmount;
                    if (payment >= remanent)
                        payment = remanent;
                    using (db = new LaundryDb())
                    {
                        db.Tickets.Find(SelectedTicket.Id).PaidAmount += payment;
                        db.SaveChanges();
                    }
                    ShowTicket();
                }
            }
            else
            {
                MessageBox.Show("Please, pick a Ticket first");
            }
        }

        private void ShowTicket()
        {
            _tickets = new ObservableCollection<Ticket>();
            LaundryDb db;
            using( db = new LaundryDb())
            {
                var ticketsWithPhone = db.Tickets.Where(t => t.Phone == PhoneToSearch);
                foreach (var t in ticketsWithPhone)
                {
                    _tickets.Add(t);
                }
            }
            OnPropertyChanged("Tickets");
        }

        private void DeliverSelectedGarment()
        {
            if (SelectedGarment == null)
            {
                MessageBox.Show("Please, click on a garment on the list first");
            }
            else
            {
                if (SelectedGarment.Paid)
                {
                    MessageBox.Show("The selected garment has already been delivered");
                }
                else
                {
                    LaundryDb db;
                    using (db = new LaundryDb())
                    {
                        db.Garments.Find(SelectedGarment.Id).Paid = true;
                        db.SaveChanges();
                    }
                    _garments.Remove(SelectedGarment);
                    OnPropertyChanged("Garments");
                }
            }
        }

        private void DeliverAllGarments()
        {
            if (SelectedTicket == null)
            {
                MessageBox.Show("Please, pick a ticket first");
            }
            else
            {
                LaundryDb db;
                using(db=new LaundryDb())
                {
                    var garmentsToFlagAsPaid = db.Garments.Where(g => g.TicketId == SelectedTicket.Id);
                    foreach (var g in garmentsToFlagAsPaid)
                        g.Paid = true;
                    db.SaveChanges();
                }
                _garments.Clear();
                OnPropertyChanged("Garments");
            }
        }
    }
}
