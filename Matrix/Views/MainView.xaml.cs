using Matrix.Views.ViewModels;
using System.Windows.Controls;

namespace Matrix.Views
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : UserControl
    {
        public MainView()
        {
            InitializeComponent();
            var vm = new MainViewModel();
            this.DataContext = vm;
        }
    }

}
