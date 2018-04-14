﻿using Matrix.Logic;
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
            GenerateSample(null);
        }
        #endregion

        #region Fields
        const string ALL = "-All-";
        public event PropertyChangedEventHandler PropertyChanged;
        int _generationCount = 5;
        ObservableCollection<TestResult> _results = new ObservableCollection<TestResult>();
        object _selectedObject;
        bool _isParamsVisible;
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
        //TODO : Impeliment Visibility
        public bool ConstructorVisibility { get; set; }
        public bool SetVisibility { get; set; }
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
        #endregion

        #region methods

        private void UpdateVisibilities()
        {
            System.Diagnostics.Debug.WriteLine(SelectedObject.ToString() != ALL);
            System.Diagnostics.Debug.WriteLine(SelectedObject.ToString());
            System.Diagnostics.Debug.WriteLine(ALL);
            if (SelectedObject.ToString() == ALL)
                IsParamsVisible = false;
            else
                IsParamsVisible = true;

            System.Diagnostics.Debug.WriteLine(IsParamsVisible);
        }
        private void GenerateSample(object parameter) => Results.ConvertReplace(new TestResultPresenter().GenerateSamples(GenerationCount, MyMethod));
        private IEnumerable<object> GetResultObjectOptions()
        {
            var objs = new List<object>(Results.Select(x => x.Result));
            SelectedObject = ALL;
            objs.Add(ALL);
            return objs;
        }
        #endregion
    }
}
