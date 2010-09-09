using System;
using System.ComponentModel;
using System.Linq;
using System.ServiceModel.DomainServices.Client;
using System.Windows.Input;
using M2M4RiaDemo.Web;
using M2M4RiaDemo.Web.Service;
using System.Collections.Generic;
using M2M4RiaDemo.Web.Model;

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
    public class HomeModel : INotifyPropertyChanged
    {
        public HomeModel()
        {
            if (DesignerProperties.IsInDesignTool == false)
            {
                DogList = c.Animals;
                TrainerList = c.Trainers;
                DogTrainers = c.EntityContainer.GetEntitySet<DogTrainer>();
                c.Load(c.GetDogsQuery());
                c.Load(c.GetTrainersQuery());
            }
        }
        public void CreateDatabase(Action action)
        {
            c.CreateDataBase(callback => action(), null);
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


        public EntitySet<Animal> DogList { get; set; }
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
        private DemoContext _c;
        public DemoContext c
        {
            get
            {
                if (_c == null)
                {
                    _c = new DemoContext();
                    _c.CreateDataBase();
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
        private HomeModel homeModel;
        public SaveCommand(HomeModel homeModel)
        {
            this.homeModel = homeModel;
            homeModel.c.PropertyChanged += CheckCanExecute;
            homeModel.PropertyChanged += CheckCanExecute;
            homeModel.c.PropertyChanged += CheckCanExecute;
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
            homeModel.c.SubmitChanges(callback =>
            {
                if (callback.HasError == true)
                    throw callback.Error;
            }, null);
        }

        protected override bool CheckCanExecute(object parameter)
        {
            return homeModel.c.HasChanges == true && homeModel.AutoSave == false &&
                homeModel.c.IsSubmitting == false;
        }
    }
    public class AddTrainerCommand : MyCommand
    {
        private HomeModel homeModel;
        public AddTrainerCommand(HomeModel homeModel)
        {
            this.homeModel = homeModel;
            homeModel.PropertyChanged += homeModel_PropertyChanged;
        }

        void homeModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SelectedDog" || e.PropertyName == "SelectedTrainer")
            {
                CanExecute(null);
            }
        }

        public override void Execute(object parameter)
        {
            homeModel.SelectedDog.Trainers.Add(homeModel.SelectedTrainer);
            homeModel.AutoSaveChanges();
            CanExecute(parameter);
        }

        protected override bool CheckCanExecute(object parameter)
        {
            return homeModel.SelectedTrainer != null && homeModel.SelectedDog != null &&
                homeModel.SelectedDog.Trainers.Contains(homeModel.SelectedTrainer) == false;
        }
    }
    public class DeleteTrainerCommand : MyCommand
    {
        HomeModel homeModel;
        public DeleteTrainerCommand(HomeModel homeModel)
        {
            this.homeModel = homeModel;
            homeModel.PropertyChanged += homeModel_PropertyChanged;
        }

        void homeModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SelectedDog" || e.PropertyName == "SelectedDogTrainer")
                CanExecute(null);
        }
        protected override bool CheckCanExecute(object parameter)
        {
            return homeModel.SelectedDog != null && homeModel.SelectedDogTrainer != null;
        }

        public override void Execute(object parameter)
        {
            homeModel.SelectedDog.Trainers.Remove(homeModel.SelectedDogTrainer);
            homeModel.AutoSaveChanges();
            CanExecute(parameter);
        }
    }
    public class CreateTrainerCommand : MyCommand
    {
        HomeModel homeModel;
        public CreateTrainerCommand(HomeModel homeModel)
        {
            this.homeModel = homeModel;
            homeModel.c.PropertyChanged += CheckCanExecute;
        }

        void CheckCanExecute(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsSubmitting")
                CanExecute(null);
        }
        protected override bool CheckCanExecute(object parameter)
        {
            return homeModel.c.IsSubmitting == false;
        }

        private int trainerCount;
        public override void Execute(object parameter)
        {
            homeModel.c.Trainers.Add(new Trainer { Name = "Trainer" + trainerCount++ });
            homeModel.AutoSaveChanges();
        }
    }
    public class AddDogCommand : MyCommand
    {
        HomeModel homeModel;
        public AddDogCommand(HomeModel homeModel)
        {
            this.homeModel = homeModel;
            homeModel.PropertyChanged += homeModel_PropertyChanged;
        }

        void homeModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SelectedTrainer" || e.PropertyName == "SelectedDog")
                CanExecute(null);
        }
        protected override bool CheckCanExecute(object parameter)
        {
            return homeModel.SelectedTrainer != null && homeModel.SelectedDog != null &&
                homeModel.SelectedTrainer.Dogs.Contains(homeModel.SelectedDog) == false;
        }

        public override void Execute(object parameter)
        {
            homeModel.SelectedTrainer.Dogs.Add(homeModel.SelectedDog);
            homeModel.AutoSaveChanges();
            CanExecute(parameter);
        }
    }
    public class DeleteDogCommand : MyCommand
    {
        HomeModel homeModel;
        public DeleteDogCommand(HomeModel homeModel)
        {
            this.homeModel = homeModel;
            homeModel.PropertyChanged += homeModel_PropertyChanged;
        }

        void homeModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SelectedTrainer" || e.PropertyName == "SelectedTrainerDog")
                CanExecute(null);
        }
        protected override bool CheckCanExecute(object parameter)
        {
            return homeModel.SelectedTrainer != null && homeModel.SelectedTrainerDog != null;
        }

        public override void Execute(object parameter)
        {
            homeModel.SelectedTrainer.Dogs.Remove(homeModel.SelectedTrainerDog);
            homeModel.AutoSaveChanges();
            CanExecute(parameter);
        }
    }
    public class CreateDogCommand : MyCommand
    {
        HomeModel homeModel;
        public CreateDogCommand(HomeModel homeModel)
        {
            this.homeModel = homeModel;
            homeModel.c.PropertyChanged += CheckCanExecute;
        }

        void CheckCanExecute(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsSubmitting")
                CanExecute(null);
        }
        protected override bool CheckCanExecute(object parameter)
        {
            return homeModel.c.IsSubmitting == false;
        }

        private int dogCount;
        public override void Execute(object parameter)
        {
            homeModel.c.Animals.Add(new Dog { Name = "Dog" + dogCount++ });
            homeModel.AutoSaveChanges();
        }
    }
}
