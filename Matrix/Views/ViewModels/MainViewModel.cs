using Matrix.Logic;
using Matrix.Models;
using Matrix.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace Matrix.Views.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        #region Ctor
        public MainViewModel()
        {
            SampleGeneration = Command.RegisterCommand(GenerateSample);
            MyMethod = new MethodPresenter().PresentMethod();
            Params = new ObservableCollection<Parameter>(MyMethod.MethodInformation.GetParameters().ToParamaters());
            GenerateSample(null);
        }
        #endregion

        #region Consts
        const string ALL = "-All-";
        const string CUSTOM = "-Custom...-";
        #endregion

        #region Fields
        public event PropertyChangedEventHandler PropertyChanged;
        int _generationCount = 5;
        ObservableCollection<TestResult> _results = new ObservableCollection<TestResult>();
        object _selectedObject;
        bool _isParamsVisible;
        bool _isCtorAvailable;
        bool _isGeneratedItemCountVisible;
        #endregion

        #region Properties
        public int GenerationCount
        {
            get => _generationCount;
            set => PropertyChanged.ChangeAndNotify(ref _generationCount, value, () => GenerationCount);
        }
        public Command SampleGeneration { get; set; }
        public IEnumerable<object> ResultObjects { get => GetResultObjectOptions(); }
        public object SelectedObject
        {
            get => _selectedObject;
            set
            {
                PropertyChanged.ChangeAndNotify(ref _selectedObject, value, () => SelectedObject);
                UpdateVisibilities();
            }
        }
        public Method MyMethod { get; set; }
        public bool IsCtorVisible
        {
            get => _isCtorAvailable;
            set
            {
                _isCtorAvailable = value;
                OnPropertyChange("IsCtorVisible");
            }
        }
        public bool IsGeneratedItemCountVisible
        {
            get => _isGeneratedItemCountVisible;
            set
            {
                _isGeneratedItemCountVisible = value;
                OnPropertyChange("IsGeneratedItemCountVisible");
            }
        }
        public bool IsParamsVisible
        {
            get => _isParamsVisible;
            set
            {
                _isParamsVisible = value;
                OnPropertyChange("IsParamsVisible");
            }
        }
        private void OnPropertyChange(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public ObservableCollection<TestResult> Results
        {
            get => _results;
            set => PropertyChanged.ChangeAndNotify(ref _results, value, () => Results);
        }
        public ObservableCollection<Parameter> Params { get; set; }
        #endregion

        #region methods

        private void UpdateVisibilities()
        {
            if (SelectedObject.ToString() == ALL)
            {
                IsParamsVisible = false;
                IsCtorVisible = false;
                IsGeneratedItemCountVisible = true;
            }
            else if (SelectedObject.ToString() == CUSTOM)
            {
                IsCtorVisible = true;
                IsParamsVisible = true;
                IsGeneratedItemCountVisible = false;
            }
            else
            {
                IsCtorVisible = false;
                IsParamsVisible = true;
                IsGeneratedItemCountVisible = false;
            }
        }
        private void GenerateSample(object parameter)
        {
            if (IsParamsVisible)
                Results.ConvertReplace(new TestResultPresenter().GenerateSample(MyMethod, Params.Select(x => (object)double.Parse(x.Value.ToString()))));
            else
                Results.ConvertReplace(new TestResultPresenter().GenerateSamples(GenerationCount, MyMethod));
        }
        private void GenerateSample() => Results.ConvertReplace(new TestResultPresenter().GenerateSamples(GenerationCount, MyMethod));
        private IEnumerable<object> GetResultObjectOptions()
        {
            var objs = new List<object>(Results.Select(x => x.Result));
            SelectedObject = ALL;
            objs.Add(ALL);
            objs.Add(CUSTOM);
            return objs;
        }
        #endregion
    }
}
