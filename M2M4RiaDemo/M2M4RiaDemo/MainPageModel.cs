using System;
using System.ComponentModel;
using System.Linq;
using System.ServiceModel.DomainServices.Client;
using System.Windows.Input;
using M2M4RiaDemo.Web.Model;
using M2M4RiaDemo.Web.Service;

namespace M2M4RiaDemo.Models
{
    public abstract class MyCommand : ICommand
    {
        protected bool CanExecuteStatus { get; set; }

        abstract protected bool CheckCanExecute(object parameter);

        public bool CanExecute(object parameter)
        {
            bool oldValue = CanExecuteStatus;
            CanExecuteStatus = CheckCanExecute(parameter);
            if (CanExecuteStatus != oldValue)
                RaiseCanExecuteChanged();
            return CanExecuteStatus;
        }

        public event EventHandler CanExecuteChanged;

        protected virtual void OnCanExecuteChanged(EventArgs e)
        {
            var canExecuteChanged = CanExecuteChanged;

            if (canExecuteChanged != null)
                canExecuteChanged(this, e);
        }

        public void RaiseCanExecuteChanged()
        {
            OnCanExecuteChanged(EventArgs.Empty);
        }

        public abstract void Execute(object parameter);
    }
    public class MainPageModel : INotifyPropertyChanged
    {
        public MainPageModel()
        {
            if (DesignerProperties.IsInDesignTool == false)
            {
                DogList = c.Dogs;
                TrainerList = c.Trainers;
                DogTrainers = c.EntityContainer.GetEntitySet<DogTrainer>();
            }
        }

        private bool _AutoSave = false;
        public bool AutoSave
        {
            get { return _AutoSave; }
            set
            {
                bool oldValue = _AutoSave;
                _AutoSave = value;
                if (oldValue != value)
                    RaisePropertyChanged("AutoSave");
            }
        }

        private Dog selectedDog;
        public Dog SelectedDog
        {
            get { return selectedDog; }
            set
            {
                selectedDog = value;
                RaisePropertyChanged("SelectedDog");
            }
        }

        private Trainer selectedDogTrainer;
        public Trainer SelectedDogTrainer
        {
            get { return selectedDogTrainer; }
            set { selectedDogTrainer = value; RaisePropertyChanged("SelectedDogTrainer"); }
        }

        private Trainer selectedTrainer;
        public Trainer SelectedTrainer
        {
            get { return selectedTrainer; }
            set { selectedTrainer = value; RaisePropertyChanged("SelectedTrainer"); }
        }

        private Dog selectedTrainerDog;
        public Dog SelectedTrainerDog
        {
            get { return selectedTrainerDog; }
            set { selectedTrainerDog = value; RaisePropertyChanged("SelectedTrainerDog"); }
        }

        private AddTrainerCommand _addTrainerCommand;
        public AddTrainerCommand AddTrainer
        {
            get
            {
                if (_addTrainerCommand == null)
                    _addTrainerCommand = new AddTrainerCommand(this);
                return _addTrainerCommand;
            }
        }

        private AddDogCommand _addDogCommand;
        public AddDogCommand AddDog
        {
            get
            {
                if (_addDogCommand == null)
                    _addDogCommand = new AddDogCommand(this);
                return _addDogCommand;
            }
            set { _addDogCommand = value; }
        }

        private DeleteTrainerCommand _deleteTrainerCommand;
        public DeleteTrainerCommand DeleteTrainer
        {
            get
            {
                if (_deleteTrainerCommand == null)
                    _deleteTrainerCommand = new DeleteTrainerCommand(this);
                return _deleteTrainerCommand;
            }
            set { _deleteTrainerCommand = value; }
        }

        private DeleteDogCommand _deleteDogCommand;
        public DeleteDogCommand DeleteDog
        {
            get
            {
                if (_deleteDogCommand == null)
                    _deleteDogCommand = new DeleteDogCommand(this);
                return _deleteDogCommand;
            }
            set { _deleteDogCommand = value; }
        }

        private CreateTrainerCommand _createTrainerCommand;
        public CreateTrainerCommand CreateTrainer
        {
            get
            {
                if (_createTrainerCommand == null)
                    _createTrainerCommand = new CreateTrainerCommand(this);
                return _createTrainerCommand;
            }
            set { _createTrainerCommand = value; }
        }

        private CreateDogCommand _createDogCommand;
        public CreateDogCommand CreateDog
        {
            get
            {
                if (_createDogCommand == null)
                    _createDogCommand = new CreateDogCommand(this);
                return _createDogCommand;
            }
            set { _createDogCommand = value; }
        }


        public EntitySet<Dog> DogList { get; set; }
        public EntitySet<Trainer> TrainerList { get; set; }
        public EntitySet<DogTrainer> DogTrainers { get; set; }

        private SaveCommand _saveCommand;
        public SaveCommand Save
        {
            get
            {
                if (_saveCommand == null)
                    _saveCommand = new SaveCommand(this);
                return _saveCommand;
            }
        }
        private M2M4RiaDemoContext _c;
        public M2M4RiaDemoContext c
        {
            get
            {
                if (_c == null)
                {
                    _c = new M2M4RiaDemoContext();
                    _c.CreateDataBase(
                        callback =>
                        {
                            if (callback.HasError == false)
                            {
                                _c.Load(c.GetDogsQuery());
                                _c.Load(c.GetTrainersQuery());
                            }
                        }, null);
                }
                return _c;
            }
        }

        public void AutoSaveChanges()
        {
            if (AutoSave == true)
            {
                var changes = c.EntityContainer.GetChanges();
                c.SubmitChanges(callback =>
                {
                }, null);
            }
        }

        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
    public class SaveCommand : MyCommand
    {
        private MainPageModel mainPageModel;
        public SaveCommand(MainPageModel mainPageModel)
        {
            this.mainPageModel = mainPageModel;
            mainPageModel.c.PropertyChanged += CheckCanExecute;
            mainPageModel.PropertyChanged += CheckCanExecute;
            mainPageModel.c.PropertyChanged += CheckCanExecute;
        }

        void CheckCanExecute(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "HasChanges" || e.PropertyName == "AutoSave" || e.PropertyName == "IsSubmitting")
            {
                CanExecute(null);
            }
        }

        public override void Execute(object parameter)
        {
            mainPageModel.c.SubmitChanges(callback =>
            {
                if (callback.HasError == true)
                    throw callback.Error;
            }, null);
        }

        protected override bool CheckCanExecute(object parameter)
        {
            return mainPageModel.c.HasChanges == true && mainPageModel.AutoSave == false &&
                mainPageModel.c.IsSubmitting == false;
        }
    }
    public class AddTrainerCommand : MyCommand
    {
        private MainPageModel mainPageModel;
        public AddTrainerCommand(MainPageModel mainPageModel)
        {
            this.mainPageModel = mainPageModel;
            mainPageModel.PropertyChanged += mainPageModel_PropertyChanged;
        }

        void mainPageModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SelectedDog" || e.PropertyName == "SelectedTrainer")
            {
                CanExecute(null);
            }
        }

        public override void Execute(object parameter)
        {
            mainPageModel.SelectedDog.Trainers.Add(mainPageModel.SelectedTrainer);
            mainPageModel.AutoSaveChanges();
            CanExecute(parameter);
        }

        protected override bool CheckCanExecute(object parameter)
        {
            return mainPageModel.SelectedTrainer != null && mainPageModel.SelectedDog != null &&
                mainPageModel.SelectedDog.Trainers.Contains(mainPageModel.SelectedTrainer) == false;
        }
    }
    public class DeleteTrainerCommand : MyCommand
    {
        MainPageModel mainPageModel;
        public DeleteTrainerCommand(MainPageModel mainPageModel)
        {
            this.mainPageModel = mainPageModel;
            mainPageModel.PropertyChanged += mainPageModel_PropertyChanged;
        }

        void mainPageModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SelectedDog" || e.PropertyName == "SelectedDogTrainer")
                CanExecute(null);
        }
        protected override bool CheckCanExecute(object parameter)
        {
            return mainPageModel.SelectedDog != null && mainPageModel.SelectedDogTrainer != null;
        }

        public override void Execute(object parameter)
        {
            mainPageModel.SelectedDog.Trainers.Remove(mainPageModel.SelectedDogTrainer);
            mainPageModel.AutoSaveChanges();
            CanExecute(parameter);
        }
    }
    public class CreateTrainerCommand : MyCommand
    {
        MainPageModel mainPageModel;
        public CreateTrainerCommand(MainPageModel mainPageModel)
        {
            this.mainPageModel = mainPageModel;
            mainPageModel.c.PropertyChanged += CheckCanExecute;
        }

        void CheckCanExecute(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsSubmitting")
                CanExecute(null);
        }
        protected override bool CheckCanExecute(object parameter)
        {
            return mainPageModel.c.IsSubmitting == false;
        }

        private int trainerCount;
        public override void Execute(object parameter)
        {
            mainPageModel.c.Trainers.Add(new Trainer { Name = "Trainer" + trainerCount++ });
            mainPageModel.AutoSaveChanges();
        }
    }
    public class AddDogCommand : MyCommand
    {
        MainPageModel mainPageModel;
        public AddDogCommand(MainPageModel mainPageModel)
        {
            this.mainPageModel = mainPageModel;
            mainPageModel.PropertyChanged += mainPageModel_PropertyChanged;
        }

        void mainPageModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SelectedTrainer" || e.PropertyName == "SelectedDog")
                CanExecute(null);
        }
        protected override bool CheckCanExecute(object parameter)
        {
            return mainPageModel.SelectedTrainer != null && mainPageModel.SelectedDog != null &&
                mainPageModel.SelectedTrainer.Dogs.Contains(mainPageModel.SelectedDog) == false;
        }

        public override void Execute(object parameter)
        {
            mainPageModel.SelectedTrainer.Dogs.Add(mainPageModel.SelectedDog);
            mainPageModel.AutoSaveChanges();
            CanExecute(parameter);
        }
    }
    public class DeleteDogCommand : MyCommand
    {
        MainPageModel mainPageModel;
        public DeleteDogCommand(MainPageModel mainPageModel)
        {
            this.mainPageModel = mainPageModel;
            mainPageModel.PropertyChanged += mainPageModel_PropertyChanged;
        }

        void mainPageModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SelectedTrainer" || e.PropertyName == "SelectedTrainerDog")
                CanExecute(null);
        }
        protected override bool CheckCanExecute(object parameter)
        {
            return mainPageModel.SelectedTrainer != null && mainPageModel.SelectedTrainerDog != null;
        }

        public override void Execute(object parameter)
        {
            mainPageModel.SelectedTrainer.Dogs.Remove(mainPageModel.SelectedTrainerDog);
            mainPageModel.AutoSaveChanges();
            CanExecute(parameter);
        }
    }
    public class CreateDogCommand : MyCommand
    {
        MainPageModel mainPageModel;
        public CreateDogCommand(MainPageModel mainPageModel)
        {
            this.mainPageModel = mainPageModel;
            mainPageModel.c.PropertyChanged += CheckCanExecute;
        }

        void CheckCanExecute(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsSubmitting")
                CanExecute(null);
        }
        protected override bool CheckCanExecute(object parameter)
        {
            return mainPageModel.c.IsSubmitting == false;
        }

        private int dogCount;
        public override void Execute(object parameter)
        {
            mainPageModel.c.Dogs.Add(new Dog { Name = "Dog" + dogCount++ });
            mainPageModel.AutoSaveChanges();
        }
    }
}
