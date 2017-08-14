using System.Windows;

namespace TintoreriaWPF
{
    public partial class ListDailyEarningsWindow : Window
    {
        public ListDailyEarningsWindow()
        {
            DataContext = new ListDailyEarningsWindowViewModel(this.Close);
            InitializeComponent();
        }

        public void InitAndShow()
        {
            Show();
        }
    }
}
