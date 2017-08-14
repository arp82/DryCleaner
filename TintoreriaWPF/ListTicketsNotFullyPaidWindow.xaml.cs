using System.Windows;

namespace TintoreriaWPF
{
    public partial class ListTicketsNotFullyPaidWindow : Window
    {
        public ListTicketsNotFullyPaidWindow()
        {
            DataContext = new ListTicketsNotFullyPaidWindowViewModel(this.Close);
            InitializeComponent();
        }

        public void InitAndShow()
        {
            Show();
        }
    }
}
