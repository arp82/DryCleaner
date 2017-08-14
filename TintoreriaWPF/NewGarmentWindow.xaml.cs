using System.Windows;
using TintoreriaWPF.ViewModel;

namespace TintoreriaWPF
{
    public partial class NewGarmentWindow : Window
    {
        public NewGarmentWindow(CollectViewModel collectViewModel)
        {
            this.DataContext = new NewGarmentViewModel(this.Close, collectViewModel);
            InitializeComponent();
        }

        public void InitAndShow()
        {
            this.Show();
        }

    }
}