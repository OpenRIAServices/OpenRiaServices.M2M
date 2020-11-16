using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using OpenRiaServices.Client;
using OpenRiaServices.M2M.Demo.Web.Model;
using OpenRiaServices.M2M.Demo.Web.Service;

namespace OpenRiaServices.M2M.Demo
{
    public abstract class MyCommand : ICommand
    {
        #region Public Events

        public event EventHandler CanExecuteChanged;

        #endregion

        #region Properties

        protected bool CanExecuteStatus { get; set; }

        #endregion

        #region Public Methods and Operators

        public bool CanExecute(object parameter)
        {
            var oldValue = CanExecuteStatus;
            CanExecuteStatus = CheckCanExecute(parameter);
            if(CanExecuteStatus != oldValue)
            {
                RaiseCanExecuteChanged();
            }
            return CanExecuteStatus;
        }

        public abstract void Execute(object parameter);

        public void RaiseCanExecuteChanged()
        {
            OnCanExecuteChanged(EventArgs.Empty);
        }

        #endregion

        #region Methods

        protected abstract bool CheckCanExecute(object parameter);

        protected virtual void OnCanExecuteChanged(EventArgs e)
        {
            var canExecuteChanged = CanExecuteChanged;

            if(canExecuteChanged != null)
            {
                canExecuteChanged(this, e);
            }
        }

        #endregion
    }

    public class MainPageModel : INotifyPropertyChanged
    {
        #region Constants and Fields

        private bool _autoSave;

        private AddDogCommand _addDogCommand;

        private AddTrainerCommand _addTrainerCommand;

        private M2M4RiaDemoContext _c;

        private CreateDogCommand _createDogCommand;

        private CreateTrainerCommand _createTrainerCommand;

        private DeleteDogCommand _deleteDogCommand;

        private DeleteTrainerCommand _deleteTrainerCommand;

        private SaveCommand _saveCommand;

        private Dog _selectedDog;

        private Trainer _selectedDogTrainer;

        private Trainer _selectedTrainer;

        private Dog _selectedTrainerDog;

        #endregion

        #region Constructors and Destructor

        public MainPageModel()
        {
            if(DesignerProperties.IsInDesignTool == false)
            {
                DogList = C.Dogs;
                TrainerList = C.Trainers;
                DogTrainers = C.EntityContainer.GetEntitySet<DogTrainer>();
            }
        }

        #endregion

        #region Public Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Public Properties

        public AddDogCommand AddDog
        {
            get { return _addDogCommand ?? (_addDogCommand = new AddDogCommand(this)); }
            set { _addDogCommand = value; }
        }

        public AddTrainerCommand AddTrainer
        {
            get { return _addTrainerCommand ?? (_addTrainerCommand = new AddTrainerCommand(this)); }
        }

        public bool AutoSave
        {
            get { return _autoSave; }
            set
            {
                var oldValue = _autoSave;
                _autoSave = value;
                if(oldValue != value)
                {
                    RaisePropertyChanged("AutoSave");
                }
            }
        }

        public CreateDogCommand CreateDog
        {
            get { return _createDogCommand ?? (_createDogCommand = new CreateDogCommand(this)); }
            set { _createDogCommand = value; }
        }

        public CreateTrainerCommand CreateTrainer
        {
            get { return _createTrainerCommand ?? (_createTrainerCommand = new CreateTrainerCommand(this)); }
            set { _createTrainerCommand = value; }
        }

        public DeleteDogCommand DeleteDog
        {
            get { return _deleteDogCommand ?? (_deleteDogCommand = new DeleteDogCommand(this)); }
            set { _deleteDogCommand = value; }
        }

        public DeleteTrainerCommand DeleteTrainer
        {
            get { return _deleteTrainerCommand ?? (_deleteTrainerCommand = new DeleteTrainerCommand(this)); }
            set { _deleteTrainerCommand = value; }
        }

        public EntitySet<Dog> DogList { get; set; }

        public EntitySet<DogTrainer> DogTrainers { get; set; }

        public SaveCommand Save
        {
            get { return _saveCommand ?? (_saveCommand = new SaveCommand(this)); }
        }

        public Dog SelectedDog
        {
            get { return _selectedDog; }
            set
            {
                _selectedDog = value;
                RaisePropertyChanged("SelectedDog");
            }
        }

        public Trainer SelectedDogTrainer
        {
            get { return _selectedDogTrainer; }
            set
            {
                _selectedDogTrainer = value;
                RaisePropertyChanged("SelectedDogTrainer");
            }
        }

        public Trainer SelectedTrainer
        {
            get { return _selectedTrainer; }
            set
            {
                _selectedTrainer = value;
                RaisePropertyChanged("SelectedTrainer");
            }
        }

        public Dog SelectedTrainerDog
        {
            get { return _selectedTrainerDog; }
            set
            {
                _selectedTrainerDog = value;
                RaisePropertyChanged("SelectedTrainerDog");
            }
        }

        public EntitySet<Trainer> TrainerList { get; set; }

        public M2M4RiaDemoContext C
        {
            get
            {
                if(_c == null)
                {
                    _c = new M2M4RiaDemoContext();
                    _c.Load(C.GetDogsQuery());
                    _c.Load(C.GetTrainersQuery());
                }
                return _c;
            }
        }

        #endregion

        #region Public Methods and Operators

        public void AutoSaveChanges()
        {
            if(AutoSave)
            {
                C.SubmitChanges(callback => { }, null);
            }
        }

        #endregion

        #region Methods

        private void RaisePropertyChanged(string propertyName)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }

    public class SaveCommand : MyCommand
    {
        #region Constants and Fields

        private readonly MainPageModel _mainPageModel;

        #endregion

        #region Constructors and Destructor

        public SaveCommand(MainPageModel mainPageModel)
        {
            _mainPageModel = mainPageModel;
            mainPageModel.C.PropertyChanged += CheckCanExecute;
            mainPageModel.PropertyChanged += CheckCanExecute;
            mainPageModel.C.PropertyChanged += CheckCanExecute;
        }

        #endregion

        #region Public Methods and Operators

        public override void Execute(object parameter)
        {
            _mainPageModel.C.SubmitChanges(
                callback =>
                    {
                        if(callback.HasError)
                        {
                            throw callback.Error;
                        }
                        _mainPageModel.DogTrainers = _mainPageModel.C.EntityContainer.GetEntitySet<DogTrainer>();
                    },
                null);
        }

        #endregion

        #region Methods

        protected override bool CheckCanExecute(object parameter)
        {
            return _mainPageModel.C.HasChanges && _mainPageModel.AutoSave == false
                   && _mainPageModel.C.IsSubmitting == false;
        }

        private void CheckCanExecute(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == "HasChanges" || e.PropertyName == "AutoSave" || e.PropertyName == "IsSubmitting")
            {
                CanExecute(null);
            }
        }

        #endregion
    }

    public class AddTrainerCommand : MyCommand
    {
        #region Constants and Fields

        private readonly MainPageModel _mainPageModel;

        #endregion

        #region Constructors and Destructor

        public AddTrainerCommand(MainPageModel mainPageModel)
        {
            _mainPageModel = mainPageModel;
            mainPageModel.PropertyChanged += MainPageModelPropertyChanged;
        }

        #endregion

        #region Public Methods and Operators

        public override void Execute(object parameter)
        {
            _mainPageModel.SelectedDog.Trainers.Add(_mainPageModel.SelectedTrainer);
            _mainPageModel.AutoSaveChanges();
            CanExecute(parameter);
        }

        #endregion

        #region Methods

        protected override bool CheckCanExecute(object parameter)
        {
            return _mainPageModel.SelectedTrainer != null && _mainPageModel.SelectedDog != null
                   && _mainPageModel.SelectedDog.Trainers.Contains(_mainPageModel.SelectedTrainer) == false;
        }

        private void MainPageModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == "SelectedDog" || e.PropertyName == "SelectedTrainer")
            {
                CanExecute(null);
            }
        }

        #endregion
    }

    public class DeleteTrainerCommand : MyCommand
    {
        #region Constants and Fields

        private readonly MainPageModel _mainPageModel;

        #endregion

        #region Constructors and Destructor

        public DeleteTrainerCommand(MainPageModel mainPageModel)
        {
            _mainPageModel = mainPageModel;
            mainPageModel.PropertyChanged += MainPageModelPropertyChanged;
        }

        #endregion

        #region Public Methods and Operators

        public override void Execute(object parameter)
        {
            _mainPageModel.SelectedDog.Trainers.Remove(_mainPageModel.SelectedDogTrainer);
            _mainPageModel.AutoSaveChanges();
            CanExecute(parameter);
        }

        #endregion

        #region Methods

        protected override bool CheckCanExecute(object parameter)
        {
            return _mainPageModel.SelectedDog != null && _mainPageModel.SelectedDogTrainer != null;
        }

        private void MainPageModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == "SelectedDog" || e.PropertyName == "SelectedDogTrainer")
            {
                CanExecute(null);
            }
        }

        #endregion
    }

    public class CreateTrainerCommand : MyCommand
    {
        #region Constants and Fields

        private readonly MainPageModel _mainPageModel;

        private int _trainerCount;

        #endregion

        #region Constructors and Destructor

        public CreateTrainerCommand(MainPageModel mainPageModel)
        {
            _mainPageModel = mainPageModel;
            mainPageModel.C.PropertyChanged += CheckCanExecute;
        }

        #endregion

        #region Public Methods and Operators

        public override void Execute(object parameter)
        {
            _mainPageModel.C.Trainers.Add(new Trainer {Name = "Trainer" + _trainerCount++});
            _mainPageModel.AutoSaveChanges();
        }

        #endregion

        #region Methods

        protected override bool CheckCanExecute(object parameter)
        {
            return _mainPageModel.C.IsSubmitting == false;
        }

        private void CheckCanExecute(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == "IsSubmitting")
            {
                CanExecute(null);
            }
        }

        #endregion
    }

    public class AddDogCommand : MyCommand
    {
        #region Constants and Fields

        private readonly MainPageModel _mainPageModel;

        #endregion

        #region Constructors and Destructor

        public AddDogCommand(MainPageModel mainPageModel)
        {
            _mainPageModel = mainPageModel;
            mainPageModel.PropertyChanged += MainPageModelPropertyChanged;
        }

        #endregion

        #region Public Methods and Operators

        public override void Execute(object parameter)
        {
            _mainPageModel.SelectedTrainer.Dogs.Add(_mainPageModel.SelectedDog);
            _mainPageModel.AutoSaveChanges();
            CanExecute(parameter);
        }

        #endregion

        #region Methods

        protected override bool CheckCanExecute(object parameter)
        {
            return _mainPageModel.SelectedTrainer != null && _mainPageModel.SelectedDog != null
                   && _mainPageModel.SelectedTrainer.Dogs.Contains(_mainPageModel.SelectedDog) == false;
        }

        private void MainPageModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == "SelectedTrainer" || e.PropertyName == "SelectedDog")
            {
                CanExecute(null);
            }
        }

        #endregion
    }

    public class DeleteDogCommand : MyCommand
    {
        #region Constants and Fields

        private readonly MainPageModel _mainPageModel;

        #endregion

        #region Constructors and Destructor

        public DeleteDogCommand(MainPageModel mainPageModel)
        {
            _mainPageModel = mainPageModel;
            mainPageModel.PropertyChanged += MainPageModelPropertyChanged;
        }

        #endregion

        #region Public Methods and Operators

        public override void Execute(object parameter)
        {
            _mainPageModel.SelectedTrainer.Dogs.Remove(_mainPageModel.SelectedTrainerDog);
            _mainPageModel.AutoSaveChanges();
            CanExecute(parameter);
        }

        #endregion

        #region Methods

        protected override bool CheckCanExecute(object parameter)
        {
            return _mainPageModel.SelectedTrainer != null && _mainPageModel.SelectedTrainerDog != null;
        }

        private void MainPageModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == "SelectedTrainer" || e.PropertyName == "SelectedTrainerDog")
            {
                CanExecute(null);
            }
        }

        #endregion
    }

    public class CreateDogCommand : MyCommand
    {
        #region Constants and Fields

        private readonly MainPageModel _mainPageModel;

        private int _dogCount;

        #endregion

        #region Constructors and Destructor

        public CreateDogCommand(MainPageModel mainPageModel)
        {
            _mainPageModel = mainPageModel;
            mainPageModel.C.PropertyChanged += CheckCanExecute;
        }

        #endregion

        #region Public Methods and Operators

        public override void Execute(object parameter)
        {
            _mainPageModel.C.Dogs.Add(new Dog {Name = "Dog" + _dogCount++});
            _mainPageModel.AutoSaveChanges();
        }

        #endregion

        #region Methods

        protected override bool CheckCanExecute(object parameter)
        {
            return _mainPageModel.C.IsSubmitting == false;
        }

        private void CheckCanExecute(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == "IsSubmitting")
            {
                CanExecute(null);
            }
        }

        #endregion
    }
}