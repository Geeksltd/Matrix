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

        public SamplePresenter sp;
        public string FormCaption = "";
        void sampleCmd_Click(object sender, RoutedEventArgs e)
        {
            var sampleFilePath = System.IO.Path.GetTempPath() + "present.html";

            if (File.Exists(sampleFilePath))
            {
                var dlgBrowser = new DlgBrowser
                {
                    HtmlFilePath = sampleFilePath,
                    Text = FormCaption
                };
                dlgBrowser.Show();
            }
        }
    }
}
