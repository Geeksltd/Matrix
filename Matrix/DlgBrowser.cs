using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Matrix
{
    public partial class DlgBrowser : Form
    {
        public string HtmlFilePath
        {
            set
            {
                webBrowser1.Url = new Uri(value);
            }
        }

        public DlgBrowser()
        {
            InitializeComponent();
        }
    }
}
