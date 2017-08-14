using System.Windows;

namespace TintoreriaWPF
{
    public partial class ListTicketsNotRetrievedWindow : Window
    {
        public ListTicketsNotRetrievedWindow()
        {
            DataContext = new ListTicketsNotRetrievedWindowViewModel(this.Close);
            InitializeComponent();
        }

        public void InitAndShow()
        {
            Show();
        }
    }
}
