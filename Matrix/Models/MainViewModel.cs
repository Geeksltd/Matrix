using Matrix.Logic;
using Matrix.Utils;
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
        #region Ctor
        public MainViewModel()
        {
            SampleGeneration = Command.RegisterCommand(GenerateSample);
            MyMethod = new MethodPresenter().PresentMethod();
            GenerateSample(null);
        }
        #endregion

        #region Fields
        public event PropertyChangedEventHandler PropertyChanged;
        int _generationCount = 5;
        ObservableCollection<TestResult> _results = new ObservableCollection<TestResult>();
        #endregion

        #region Properties
        public int GenerationCount
        {
            get => _generationCount;
            set => PropertyChanged.ChangeAndNotify(ref _generationCount, value, () => GenerationCount);
        }
        public Command SampleGeneration { get; set; }
        public IEnumerable<object> Objects { get => this.Results.Select(x => x.Object); }
        public Method MyMethod { get; set; }
        public ObservableCollection<TestResult> Results
        {
            get => _results;
            set { PropertyChanged.ChangeAndNotify(ref _results, value, () => Results); System.Diagnostics.Debug.WriteLine(Results.Count); }
        }
        #endregion

        #region methods
        private void GenerateSample(object parameter) => Results.ConvertReplace(new TestResultPresenter().GenerateSamples(GenerationCount, MyMethod));

        #endregion
    }
}
