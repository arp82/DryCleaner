using System.Windows;

namespace TintoreriaWPF
{
    public partial class ListEarningsByGarmentAndGarmentTypeWindow : Window
    {
        public ListEarningsByGarmentAndGarmentTypeWindow()
        {
            DataContext = new ListEarningsByGarmentAndGarmentTypeWindowViewModel(this.Close);
            InitializeComponent();
        }

        public void InitAndShow()
        {
            Show();
        }
    }
}
