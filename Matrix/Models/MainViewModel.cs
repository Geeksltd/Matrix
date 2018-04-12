using Matrix.Logic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrix.Models
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {
            var presenter = new ValuePresenter();
            MyMethod = presenter.PresentMethod();
        }
        public Method MyMethod { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
