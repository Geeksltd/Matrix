using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using EnvDTE80;
using EnvDTE;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System.IO;

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
        private void sampleCmd_Click(object sender, RoutedEventArgs e)
        {
            string sampleFilePath = System.IO.Path.GetTempPath() + "present.html";

            if (File.Exists(sampleFilePath))
            {
                DlgBrowser dlgBrowser = new DlgBrowser();
                dlgBrowser.HtmlFilePath = sampleFilePath;
                dlgBrowser.Text = FormCaption;
                dlgBrowser.Show();
            }
        }
    }
}
