using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace Matrix
{
    /// <summary>
    /// Interaction logic for LinkButton.xaml
    /// </summary>
    public partial class LinkButton : UserControl
    {
        public LinkButton()
        {
            InitializeComponent();
        }
        public string FormCaption = "";
        void sampleCmd_Click(object sender, RoutedEventArgs e)
        {
            var window = new MainWindow
            {
                Text = FormCaption
            };
            window.Show();
        }
    }
}
