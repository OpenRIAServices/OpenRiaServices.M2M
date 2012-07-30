using System;
using System.ComponentModel;
using System.Linq;
using System.ServiceModel.DomainServices.Client;
using System.Windows.Input;
using RIAServices.M2M.Demo.Web.Model;
using RIAServices.M2M.Demo.Web.Service;

namespace RIAServices.M2M.Demo
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

        private bool _AutoSave;

        private AddDogCommand _addDogCommand;

        private AddTrainerCommand _addTrainerCommand;

        private M2M4RiaDemoContext _c;

        private CreateDogCommand _createDogCommand;

        private CreateTrainerCommand _createTrainerCommand;

        private DeleteDogCommand _deleteDogCommand;

        private DeleteTrainerCommand _deleteTrainerCommand;

        private SaveCommand _saveCommand;

        private Dog selectedDog;

        private Trainer selectedDogTrainer;

        private Trainer selectedTrainer;

        private Dog selectedTrainerDog;

        #endregion

        #region Constructors and Destructors

        public MainPageModel()
        {
            if(DesignerProperties.IsInDesignTool == false)
            {
                DogList = c.Dogs;
                TrainerList = c.Trainers;
                DogTrainers = c.EntityContainer.GetEntitySet<DogTrainer>();
            }
        }

        #endregion

        #region Public Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Public Properties

        public AddDogCommand AddDog
        {
            get
            {
                if(_addDogCommand == null)
                {
                    _addDogCommand = new AddDogCommand(this);
                }
                return _addDogCommand;
            }
            set { _addDogCommand = value; }
        }

        public AddTrainerCommand AddTrainer
        {
            get
            {
                if(_addTrainerCommand == null)
                {
                    _addTrainerCommand = new AddTrainerCommand(this);
                }
                return _addTrainerCommand;
            }
        }

        public bool AutoSave
        {
            get { return _AutoSave; }
            set
            {
                var oldValue = _AutoSave;
                _AutoSave = value;
                if(oldValue != value)
                {
                    RaisePropertyChanged("AutoSave");
                }
            }
        }

        public CreateDogCommand CreateDog
        {
            get
            {
                if(_createDogCommand == null)
                {
                    _createDogCommand = new CreateDogCommand(this);
                }
                return _createDogCommand;
            }
            set { _createDogCommand = value; }
        }

        public CreateTrainerCommand CreateTrainer
        {
            get
            {
                if(_createTrainerCommand == null)
                {
                    _createTrainerCommand = new CreateTrainerCommand(this);
                }
                return _createTrainerCommand;
            }
            set { _createTrainerCommand = value; }
        }

        public DeleteDogCommand DeleteDog
        {
            get
            {
                if(_deleteDogCommand == null)
                {
                    _deleteDogCommand = new DeleteDogCommand(this);
                }
                return _deleteDogCommand;
            }
            set { _deleteDogCommand = value; }
        }

        public DeleteTrainerCommand DeleteTrainer
        {
            get
            {
                if(_deleteTrainerCommand == null)
                {
                    _deleteTrainerCommand = new DeleteTrainerCommand(this);
                }
                return _deleteTrainerCommand;
            }
            set { _deleteTrainerCommand = value; }
        }

        public EntitySet<Dog> DogList { get; set; }

        public EntitySet<DogTrainer> DogTrainers { get; set; }

        public SaveCommand Save
        {
            get
            {
                if(_saveCommand == null)
                {
                    _saveCommand = new SaveCommand(this);
                }
                return _saveCommand;
            }
        }

        public Dog SelectedDog
        {
            get { return selectedDog; }
            set
            {
                selectedDog = value;
                RaisePropertyChanged("SelectedDog");
            }
        }

        public Trainer SelectedDogTrainer
        {
            get { return selectedDogTrainer; }
            set
            {
                selectedDogTrainer = value;
                RaisePropertyChanged("SelectedDogTrainer");
            }
        }

        public Trainer SelectedTrainer
        {
            get { return selectedTrainer; }
            set
            {
                selectedTrainer = value;
                RaisePropertyChanged("SelectedTrainer");
            }
        }

        public Dog SelectedTrainerDog
        {
            get { return selectedTrainerDog; }
            set
            {
                selectedTrainerDog = value;
                RaisePropertyChanged("SelectedTrainerDog");
            }
        }

        public EntitySet<Trainer> TrainerList { get; set; }

        public M2M4RiaDemoContext c
        {
            get
            {
                if(_c == null)
                {
                    _c = new M2M4RiaDemoContext();
                    _c.Load(c.GetDogsQuery());
                    _c.Load(c.GetTrainersQuery());
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
                EntityChangeSet changes = c.EntityContainer.GetChanges();
                c.SubmitChanges(callback => { }, null);
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

        private readonly MainPageModel mainPageModel;

        #endregion

        #region Constructors and Destructors

        public SaveCommand(MainPageModel mainPageModel)
        {
            this.mainPageModel = mainPageModel;
            mainPageModel.c.PropertyChanged += CheckCanExecute;
            mainPageModel.PropertyChanged += CheckCanExecute;
            mainPageModel.c.PropertyChanged += CheckCanExecute;
        }

        #endregion

        #region Public Methods and Operators

        public override void Execute(object parameter)
        {
            mainPageModel.c.SubmitChanges(
                callback =>
                    {
                        if(callback.HasError)
                        {
                            throw callback.Error;
                        }
                        mainPageModel.DogTrainers = mainPageModel.c.EntityContainer.GetEntitySet<DogTrainer>();
                        var x = parameter;
                    },
                null);
        }

        #endregion

        #region Methods

        protected override bool CheckCanExecute(object parameter)
        {
            return mainPageModel.c.HasChanges && mainPageModel.AutoSave == false
                   && mainPageModel.c.IsSubmitting == false;
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

        private readonly MainPageModel mainPageModel;

        #endregion

        #region Constructors and Destructors

        public AddTrainerCommand(MainPageModel mainPageModel)
        {
            this.mainPageModel = mainPageModel;
            mainPageModel.PropertyChanged += mainPageModel_PropertyChanged;
        }

        #endregion

        #region Public Methods and Operators

        public override void Execute(object parameter)
        {
            mainPageModel.SelectedDog.Trainers.Add(mainPageModel.SelectedTrainer);
            mainPageModel.AutoSaveChanges();
            CanExecute(parameter);
        }

        #endregion

        #region Methods

        protected override bool CheckCanExecute(object parameter)
        {
            return mainPageModel.SelectedTrainer != null && mainPageModel.SelectedDog != null
                   && mainPageModel.SelectedDog.Trainers.Contains(mainPageModel.SelectedTrainer) == false;
        }

        private void mainPageModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
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

        private readonly MainPageModel mainPageModel;

        #endregion

        #region Constructors and Destructors

        public DeleteTrainerCommand(MainPageModel mainPageModel)
        {
            this.mainPageModel = mainPageModel;
            mainPageModel.PropertyChanged += mainPageModel_PropertyChanged;
        }

        #endregion

        #region Public Methods and Operators

        public override void Execute(object parameter)
        {
            mainPageModel.SelectedDog.Trainers.Remove(mainPageModel.SelectedDogTrainer);
            mainPageModel.AutoSaveChanges();
            CanExecute(parameter);
        }

        #endregion

        #region Methods

        protected override bool CheckCanExecute(object parameter)
        {
            return mainPageModel.SelectedDog != null && mainPageModel.SelectedDogTrainer != null;
        }

        private void mainPageModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
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

        private readonly MainPageModel mainPageModel;

        private int trainerCount;

        #endregion

        #region Constructors and Destructors

        public CreateTrainerCommand(MainPageModel mainPageModel)
        {
            this.mainPageModel = mainPageModel;
            mainPageModel.c.PropertyChanged += CheckCanExecute;
        }

        #endregion

        #region Public Methods and Operators

        public override void Execute(object parameter)
        {
            mainPageModel.c.Trainers.Add(new Trainer {Name = "Trainer" + trainerCount++});
            mainPageModel.AutoSaveChanges();
        }

        #endregion

        #region Methods

        protected override bool CheckCanExecute(object parameter)
        {
            return mainPageModel.c.IsSubmitting == false;
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

        private readonly MainPageModel mainPageModel;

        #endregion

        #region Constructors and Destructors

        public AddDogCommand(MainPageModel mainPageModel)
        {
            this.mainPageModel = mainPageModel;
            mainPageModel.PropertyChanged += mainPageModel_PropertyChanged;
        }

        #endregion

        #region Public Methods and Operators

        public override void Execute(object parameter)
        {
            mainPageModel.SelectedTrainer.Dogs.Add(mainPageModel.SelectedDog);
            mainPageModel.AutoSaveChanges();
            CanExecute(parameter);
        }

        #endregion

        #region Methods

        protected override bool CheckCanExecute(object parameter)
        {
            return mainPageModel.SelectedTrainer != null && mainPageModel.SelectedDog != null
                   && mainPageModel.SelectedTrainer.Dogs.Contains(mainPageModel.SelectedDog) == false;
        }

        private void mainPageModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
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

        private readonly MainPageModel mainPageModel;

        #endregion

        #region Constructors and Destructors

        public DeleteDogCommand(MainPageModel mainPageModel)
        {
            this.mainPageModel = mainPageModel;
            mainPageModel.PropertyChanged += mainPageModel_PropertyChanged;
        }

        #endregion

        #region Public Methods and Operators

        public override void Execute(object parameter)
        {
            mainPageModel.SelectedTrainer.Dogs.Remove(mainPageModel.SelectedTrainerDog);
            mainPageModel.AutoSaveChanges();
            CanExecute(parameter);
        }

        #endregion

        #region Methods

        protected override bool CheckCanExecute(object parameter)
        {
            return mainPageModel.SelectedTrainer != null && mainPageModel.SelectedTrainerDog != null;
        }

        private void mainPageModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
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

        private readonly MainPageModel mainPageModel;

        private int dogCount;

        #endregion

        #region Constructors and Destructors

        public CreateDogCommand(MainPageModel mainPageModel)
        {
            this.mainPageModel = mainPageModel;
            mainPageModel.c.PropertyChanged += CheckCanExecute;
        }

        #endregion

        #region Public Methods and Operators

        public override void Execute(object parameter)
        {
            mainPageModel.c.Dogs.Add(new Dog {Name = "Dog" + dogCount++});
            mainPageModel.AutoSaveChanges();
        }

        #endregion

        #region Methods

        protected override bool CheckCanExecute(object parameter)
        {
            return mainPageModel.c.IsSubmitting == false;
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