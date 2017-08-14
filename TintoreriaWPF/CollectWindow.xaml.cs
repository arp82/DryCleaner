using System.Windows;
using TintoreriaWPF.ViewModel;

namespace TintoreriaWPF
{
    public partial class CollectWindow : Window
    {
        public CollectWindow()
        {
            InitializeComponent();
            this.DataContext = new CollectViewModel();
        }
    }
}
