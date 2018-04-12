using Matrix.Logic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            var presenter = new MethodPresenter();
            MyMethod = presenter.PresentMethod();
            Results = new ObservableCollection<TestResult>();
        }
        public Method MyMethod { get; set; }
        public ObservableCollection<TestResult> Results { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
