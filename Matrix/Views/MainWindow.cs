using System;
using System.Windows.Forms;

namespace Matrix
{
    public partial class MainWindow : Form
    {
        public string HtmlFilePath
        {
            set
            {
                //webBrowser1.Url = new Uri(value);
            }
        }

        public MainWindow()
        {
            InitializeComponent();
        }
    }
}
