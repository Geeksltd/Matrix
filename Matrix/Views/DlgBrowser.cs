using System;
using System.Windows.Forms;

namespace Matrix
{
    public partial class DlgBrowser : Form
    {
        public string HtmlFilePath
        {
            set
            {
                //webBrowser1.Url = new Uri(value);
            }
        }

        public DlgBrowser()
        {
            InitializeComponent();
        }
    }
}
