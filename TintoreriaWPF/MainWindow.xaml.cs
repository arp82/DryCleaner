using System.Windows;
using TintoreriaWPF.ViewModel;

namespace TintoreriaWPF
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainViewModel();
        }
    }
}