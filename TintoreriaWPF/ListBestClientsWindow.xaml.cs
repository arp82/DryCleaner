using System.Windows;

namespace TintoreriaWPF
{
    public partial class ListBestClientsWindow : Window
    {
        public ListBestClientsWindow()
        {
            DataContext = new ListBestClientsWindowViewModel(this.Close);
            InitializeComponent();
        }

        public void InitAndShow()
        {
            Show();
        }
    }
}
