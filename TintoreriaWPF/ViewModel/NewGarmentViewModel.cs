using GalaSoft.MvvmLight.Command;
using System;
using System.ComponentModel;
using TintoreriaWPF.Model;

namespace TintoreriaWPF.ViewModel
{
    public class NewGarmentViewModel : INotifyPropertyChanged
    {
        public RelayCommand AddNewGarmentCommand { get; set; }
        public RelayCommand ExitCommand { get; set; }
        private Garment _garment;
        private CollectViewModel _collectViewModel;
        private string _errorMessage;

        public string GarmentName
        {
            get
            {
                return _garment.Name;
            }
            set
            {
                _garment.Name = value;
                OnPropertyChanged("GarmentName");
            }
        }

        public string GarmentColor
        {
            get
            {
                return _garment.Color;
            }
            set
            {
                _garment.Color = value;
                OnPropertyChanged("GarmentColor");
            }
        }

        public string GarmentServiceType
        {
            get
            {
                return _garment.ServiceType;
            }
            set
            {
                _garment.ServiceType = value;
                OnPropertyChanged("GarmentServiceType");
            }
        }

        public double GarmentPriceTag
        {
            get
            {
                return _garment.PriceTag;
            }
            set
            {
                _garment.PriceTag = value;
                OnPropertyChanged("GarmentPriceTag");
            }
        }

        public string[] GarmentServiceTypes
        {
            get { return _serviceTypes; }
        }

        public string GarmentType
        {
            get
            {
                return _garment.GarmentType;
            }
            set
            {
                _garment.GarmentType = value;
                OnPropertyChanged("GarmentType");
            }
        }

        public string[] GarmentTypes
        {
            get { return _garmentType; }
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

        private readonly string[] _serviceTypes = {"Full", "Ironing", "Laundering"};
        private readonly string[] _garmentType = {"Men", "Women", "Unisex", "Home"};

        public NewGarmentViewModel(Action closeAction, CollectViewModel collectViewModel)
        {
            AddNewGarmentCommand = new RelayCommand(AddNewGarment);
            ExitCommand = new RelayCommand(closeAction);
            _garment = new Garment();
            _collectViewModel = collectViewModel;
        }

        private void AddNewGarment()
        {
            if (IsValidate())
            {
                _garment.Paid = false;
                _collectViewModel.Garments.Add(_garment);
                _collectViewModel.CalculateTotalAmount();
                _garment = new Garment();
                Reset();
            }
        }

        private void Reset()
        {
            GarmentName = "";
            GarmentPriceTag = 0;
            GarmentServiceType = "";
            GarmentType = "";
            GarmentColor = "";
        }

        public bool IsValidate()
        {
            if (string.IsNullOrEmpty(GarmentName))
            {
                ErrorMessage = "Name is required.";
                return false;
            }
            else if (string.IsNullOrEmpty(GarmentColor))
            {
                ErrorMessage = "Color is required.";
                return false;
            }
            else if (string.IsNullOrEmpty(GarmentServiceType))
            {
                ErrorMessage = "Service is required.";
                return false;
            }
            else if (string.IsNullOrEmpty(GarmentType))
            {
                ErrorMessage = "Type is required.";
                return false;
            }
            else if (GarmentPriceTag == 0)
            {
                ErrorMessage = "Price is required.";
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