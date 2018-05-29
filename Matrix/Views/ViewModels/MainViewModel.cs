using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Matrix.Infrustructure;
using Matrix.Logic;
using Matrix.Models;
using Matrix.Utils;

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
            Results = new ObservableCollection<TestResult>();
            SelectedCtor = Ctors.FirstOrDefault();
            SetProperties = MyMethod.ClassInstance.GetType().GetProps().ToCaption();
            System.Diagnostics.Debug.WriteLine(SetProperties);
            GenerateSample();
        }
        #endregion

        #region Fields
        public event PropertyChangedEventHandler PropertyChanged;
        int generationCount = 5;
        IEnumerable<KeyValuePair<object, string>> resultObjects;
        KeyValuePair<object, string> selectedObject;
        KeyValuePair<object, string> selectedCtor;
        bool isParamsVisible;
        bool isCtorAvailable;
        bool isGeneratedItemCountVisible;
        string setProperties;
        #endregion

        #region Properties
        public int GenerationCount
        {
            get => generationCount;
            set => PropertyChanged.ChangeAndNotify(ref generationCount, value, () => GenerationCount);
        }
        [EscapeGCop("It's not applicable because I didn't get this")]
        public bool IsSetEnabled
        {
            get => !(SetProperties == string.Empty);
        }
        public Command SampleGeneration { get; set; }
        public IEnumerable<KeyValuePair<object, string>> ResultObjects
        {
            get => resultObjects;
            set
            {
                resultObjects = value;
                OnPropertyChange("ResultObjects");
            }
        }
        public string SetProperties
        {
            get => setProperties;
            set
            {
                setProperties = value;
                OnPropertyChange("SetProperties");
            }
        }
        public KeyValuePair<object, string> SelectedObject
        {
            get => selectedObject;
            set
            {
                selectedObject = value;
                OnPropertyChange("SelectedObject");
                UpdateVisibilities();
            }
        }
        public Method MyMethod { get; set; }
        public bool IsCtorVisible
        {
            get => isCtorAvailable;
            set
            {
                isCtorAvailable = value;
                OnPropertyChange("IsCtorVisible");
            }
        }
        public bool IsGeneratedItemCountVisible
        {
            get => isGeneratedItemCountVisible;
            set
            {
                isGeneratedItemCountVisible = value;
                OnPropertyChange("IsGeneratedItemCountVisible");
            }
        }
        public bool IsParamsVisible
        {
            get => isParamsVisible;
            set
            {
                isParamsVisible = value;
                OnPropertyChange("IsParamsVisible");
            }
        }
        public ObservableCollection<TestResult> Results { get; set; }
        public ObservableCollection<Parameter> Params { get; set; }
        public ObservableCollection<KeyValuePair<object, string>> Ctors { get; set; }
        public KeyValuePair<object, string> SelectedCtor
        {
            get => selectedCtor;
            set
            {
                selectedCtor = value;
                try
                {
                    SelectedCtorParameters.ConvertReplace(((Constructor)SelectedCtor.Key).Params);
                }
                catch
                {
                    // No logging is needed
                    SelectedCtorParameters.Clear();
                }

                OnPropertyChange("SelectedCtor");
            }
        }
        public ObservableCollection<Parameter> SelectedCtorParameters { get; set; }
        #endregion

        #region methods

        void UpdateVisibilities()
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

        void GenerateSample()
        {
            var examples = Current.DesignedExamples.Where(x => x.Type == MyMethod.ClassInstance.GetType() && x.MethodName == MyMethod.MethodName);
            if (examples.Any())
                Results.ConvertReplace(TestResultPresenter.GenerateSamples(MyMethod, examples));
            else
                Results.ConvertReplace(TestResultPresenter.GenerateSamples(GenerationCount, MyMethod));

            SetResultObjectOptions();
        }

        [EscapeGCop("It's not applicable because of MVVM Command pattern")]
        void GenerateSample(object parameter)
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

            SetResultObjectOptions();
        }

        void SetResultObjectOptions()
        {
            var results = Results.Select(x => x.Result).GetSelectList(ALL, CUSTOM);
            SelectedObject = results.FirstOrDefault(x => x.Value == ALL);
            ResultObjects = results;
        }

        void OnPropertyChange(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        #endregion
    }
}