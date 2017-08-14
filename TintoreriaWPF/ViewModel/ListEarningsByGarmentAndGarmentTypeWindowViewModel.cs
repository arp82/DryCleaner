using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using TintoreriaWPF.Model;

namespace TintoreriaWPF
{
    public class ListEarningsByGarmentAndGarmentTypeWindowViewModel : INotifyPropertyChanged
    {
        public RelayCommand ExitCommand { get; set; }

        public IList<string> EarningsByGarment { get; private set; }
        public IList<string> EarningsByGarmentType { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public ListEarningsByGarmentAndGarmentTypeWindowViewModel(Action closeAction)
        {
            ExitCommand = new RelayCommand(closeAction);
            PerformSearch();
        }

        private void PerformSearch()
        {
            LaundryDb db;
            using (db = new LaundryDb())
            {
                var garmentsDelivered = db.Garments.Where(g => g.Paid);
                var garmentsDeliveredGrouped = garmentsDelivered.GroupBy(g => g.GarmentType)
                    .Select(gr => new 
                    {
                        GarmentType = gr.Key,
                        TotalEarnings = gr.Sum(g=> g.PriceTag)
                    });

                EarningsByGarment = new List<string>();
                foreach (var g in garmentsDelivered)
                {
                    string s = string.Format("Garment {1}, with id {0} generated an income of {2}", g.Id, g.Name, g.PriceTag);
                    EarningsByGarment.Add(s);
                }
                RaisePropertyChanged("EarningsByGarment");

                EarningsByGarmentType = new List<string>();
                foreach (var gr in garmentsDeliveredGrouped)
                {
                    string s = string.Format("Garment Type {0} generated an income of {1}", gr.GarmentType, gr.TotalEarnings);
                    EarningsByGarmentType.Add(s);
                }
                RaisePropertyChanged("EarningsByGarmentType");
            }
        }
    }
}
