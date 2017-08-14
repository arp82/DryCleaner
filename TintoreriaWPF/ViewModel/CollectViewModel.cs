using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using GalaSoft.MvvmLight.Command;
using TintoreriaWPF.Model;

namespace TintoreriaWPF.ViewModel
{
    public class CollectViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Garment> _garments;
        private Ticket _ticket;
        private string _deliveries;
        private LaundryDb _db;
        private string _errorMessage;

        public RelayCommand SaveTicketCommand { get; private set; }
        public RelayCommand CreateNewGarmentCommand { get; set; }

        public CollectViewModel()
        {
            SaveTicketCommand = new RelayCommand(SaveTicket);
            CreateNewGarmentCommand = new RelayCommand(CreateNewGarment);
            Initialize();
        }

        public int TicketId
        {
            get
            {
                return _ticket.Id;
            }
            set
            {
                _ticket.Id = value;
                OnPropertyChanged("TicketId");
            }
        }

        public int Phone
        {
            get
            {
                return _ticket.Phone;
            }
            set
            {
                _ticket.Phone = value;
                OnPropertyChanged("Phone");
            }
        }

        public double PaidAmount
        {
            get
            {
                return _ticket.PaidAmount;
            }
            set
            {
                _ticket.PaidAmount = value;
                OnPropertyChanged("PaidAmount");
            }
        }

        public double TotalAmount
        {
            get
            {
                return _ticket.TotalAmount;
            }
            set
            {
                _ticket.TotalAmount = value;
                OnPropertyChanged("TotalAmount");
            }
        }

        public string Deliveries
        {
            get
            {
                return _deliveries;
            }
            set
            {
                _deliveries = value;
                OnPropertyChanged("Deliveries");
            }
        }

        public string DeliveryDate
        {
            get
            {
                return _ticket.DeliveryDate;
            }
            set
            {
                _ticket.DeliveryDate = value.Substring(0, 10);
                OnPropertyChanged("DeliveryDate");
                GetDeliveries();
            }
        }

        public string ErrorMessage
        {
            get
            {
                return _errorMessage;
            }
            set
            {
                _errorMessage = value;
                OnPropertyChanged("ErrorMessage");
            }
        }

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

        public void Initialize()
        {
            _garments = new ObservableCollection<Garment>();
            _ticket = new Ticket();

            using (_db = new LaundryDb())
            {
                TicketId = _db.Tickets.Count() + 1;
            }

            Phone = 0;
            TotalAmount = 0;
            PaidAmount = 0;
            Deliveries = "";
            DeliveryDate = "Select a date";
            Garments.Clear();
            OnPropertyChanged("Garments");
        }

        private void CreateNewGarment()
        {
            NewGarmentWindow newGarmentWindow = new NewGarmentWindow(this);
            newGarmentWindow.InitAndShow();
        }

        public void CalculateTotalAmount()
        {
            double total = _garments.Sum(t => t.PriceTag);
            TotalAmount = total;
        }
        
        public void GetDeliveries()
        {
            using (_db = new LaundryDb())
            {
                _deliveries = _db.Tickets.Count(t => t.DeliveryDate.Contains(_ticket.DeliveryDate)).ToString();
                Deliveries = _deliveries == "" ? "0" : _deliveries;
            }          
        }

        public void SaveTicket()
        {
            CalculateTotalAmount();
            if (IsValidate())
            {
                _ticket.Garments = _garments;
                using (_db = new LaundryDb())
                {
                    for (int i = 0; i < _garments.Count(); i++)
                    {
                        _db.Garments.Add(_garments[i]);
                    }
                    _db.Tickets.Add(_ticket);
                    _db.SaveChanges();
                }
                Initialize();
            }
        }

        public bool IsValidate()
        {
            if (_ticket.Phone==0)
            {
                ErrorMessage = "Phone is required.";
                return false;
            }
            else if (_ticket.DeliveryDate.Contains("Select"))
            {
                ErrorMessage = "Select a date.";
                return false;
            }
            else if (!_garments.Any())
            {
                ErrorMessage = "Add one garment at least.";
                return false;
            }
            else
            {
                return true;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}