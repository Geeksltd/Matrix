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
        #region Consts
        const string ALL = "-All-";
        const string CUSTOM = "-Custom...-";
        const string EMPTYCTOR = "()";
        #endregion

        #region Ctor
        public MainViewModel()
        {
            SampleGeneration = Command.RegisterCommand(GenerateSample);
            MyMethod = new MethodPresenter().PresentMethod();
            Params = new ObservableCollection<Parameter>(MyMethod.MethodInformation.GetParameters().ToParamaters());
            Ctors = new ObservableCollection<KeyValuePair<object, string>>(Constructor.GetCTors(MyMethod.ClassInstance.GetType()).GetSelectList(EMPTYCTOR));
            SelectedCtorParameters = new ObservableCollection<Parameter>();
            SelectedCtor = Ctors.FirstOrDefault();
            GenerateSample(null);
        }
        #endregion

        #region Fields
        public event PropertyChangedEventHandler PropertyChanged;
        int _generationCount = 5;
        ObservableCollection<TestResult> _results = new ObservableCollection<TestResult>();
        KeyValuePair<object, string> _selectedObject;
        KeyValuePair<object, string> _selectedCtor;
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
        public IEnumerable<KeyValuePair<object, string>> ResultObjects { get => GetResultObjectOptions(); }
        public KeyValuePair<object, string> SelectedObject
        {
            get => _selectedObject;
            set
            {
                _selectedObject = value;
                OnPropertyChange("SelectedObject");
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
        public ObservableCollection<TestResult> Results
        {
            get => _results;
            set => PropertyChanged.ChangeAndNotify(ref _results, value, () => Results);
        }
        public ObservableCollection<Parameter> Params { get; set; }
        public ObservableCollection<KeyValuePair<object, string>> Ctors { get; set; }
        public KeyValuePair<object, string> SelectedCtor
        {
            get => _selectedCtor;
            set
            {
                _selectedCtor = value;
                try
                {
                    SelectedCtorParameters.ConvertReplace(((Constructor)SelectedCtor.Key).Params);
                }
                catch
                {
                    SelectedCtorParameters.Clear();
                }
                OnPropertyChange("SelectedCtor");
            }
        }
        public ObservableCollection<Parameter> SelectedCtorParameters { get; set; }
        #endregion

        #region methods

        private void UpdateVisibilities()
        {
            if (SelectedObject.Value == ALL)
            {
                IsParamsVisible = false;
                IsCtorVisible = false;
                IsGeneratedItemCountVisible = true;
            }
            else if (SelectedObject.Value == CUSTOM)
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
            if (IsParamsVisible && IsCtorVisible)
                if (SelectedCtor.Value == EMPTYCTOR)
                    Results.ConvertReplace(TestResultPresenter.GenerateSample(MyMethod, Params, null, null));
                else
                    Results.ConvertReplace(TestResultPresenter.GenerateSample(MyMethod, Params, SelectedCtor.Key as Constructor, SelectedCtorParameters));
            else if (IsParamsVisible)
                Results.ConvertReplace(TestResultPresenter.GenerateSample(MyMethod, Params));
            else
                Results.ConvertReplace(TestResultPresenter.GenerateSamples(GenerationCount, MyMethod));
        }
        private void GenerateSample() => Results.ConvertReplace(TestResultPresenter.GenerateSamples(GenerationCount, MyMethod));
        private IEnumerable<KeyValuePair<object, string>> GetResultObjectOptions()
        {
            var results = Results.Select(x => x.Result).GetSelectList(ALL, CUSTOM);
            SelectedObject = results.Where(x => x.Value == ALL).FirstOrDefault();
            return results;
        }
        private void OnPropertyChange(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
