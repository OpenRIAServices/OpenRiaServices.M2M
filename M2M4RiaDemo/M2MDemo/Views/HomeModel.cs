using System;
using System.ComponentModel;
using System.Linq;
using System.ServiceModel.DomainServices.Client;
using System.Windows.Input;
using M2MDemo.Web;

namespace M2MDemo.Models
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
                PatientList = c.Patients;
                DoctorList = c.Doctors;
                PatientDoctors = c.EntityContainer.GetEntitySet<PatientDoctor>();
                c.Load(c.GetPatientsQuery());
                c.Load(c.GetDoctorsQuery());
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

        private Patient selectedPatient;
        public Patient SelectedPatient
        {
            get { return selectedPatient; }
            set
            {
                selectedPatient = value;
                RaisePropertyChanged("SelectedPatient");
            }
        }

        private Doctor selectedPatientDoctor;
        public Doctor SelectedPatientDoctor
        {
            get { return selectedPatientDoctor; }
            set { selectedPatientDoctor = value; RaisePropertyChanged("SelectedPatientDoctor"); }
        }

        private Doctor selectedDoctor;
        public Doctor SelectedDoctor
        {
            get { return selectedDoctor; }
            set { selectedDoctor = value; RaisePropertyChanged("SelectedDoctor"); }
        }

        private Patient selectedDoctorPatient;
        public Patient SelectedDoctorPatient
        {
            get { return selectedDoctorPatient; }
            set { selectedDoctorPatient = value; RaisePropertyChanged("SelectedDoctorPatient"); }
        }

        private AddDoctorCommand _addDoctorCommand;
        public AddDoctorCommand AddDoctor
        {
            get
            {
                if (_addDoctorCommand == null)
                    _addDoctorCommand = new AddDoctorCommand(this);
                return _addDoctorCommand;
            }
        }

        private AddPatientCommand _addPatientCommand;
        public AddPatientCommand AddPatient
        {
            get
            {
                if (_addPatientCommand == null)
                    _addPatientCommand = new AddPatientCommand(this);
                return _addPatientCommand;
            }
            set { _addPatientCommand = value; }
        }

        private DeleteDoctorCommand _deleteDoctorCommand;
        public DeleteDoctorCommand DeleteDoctor
        {
            get
            {
                if (_deleteDoctorCommand == null)
                    _deleteDoctorCommand = new DeleteDoctorCommand(this);
                return _deleteDoctorCommand;
            }
            set { _deleteDoctorCommand = value; }
        }

        private DeletePatientCommand _deletePatientCommand;
        public DeletePatientCommand DeletePatient
        {
            get
            {
                if (_deletePatientCommand == null)
                    _deletePatientCommand = new DeletePatientCommand(this);
                return _deletePatientCommand;
            }
            set { _deletePatientCommand = value; }
        }

        private CreateDoctorCommand _createDoctorCommand;
        public CreateDoctorCommand CreateDoctor
        {
            get
            {
                if (_createDoctorCommand == null)
                    _createDoctorCommand = new CreateDoctorCommand(this);
                return _createDoctorCommand;
            }
            set { _createDoctorCommand = value; }
        }

        private CreatePatientCommand _createPatientCommand;
        public CreatePatientCommand CreatePatient
        {
            get
            {
                if (_createPatientCommand == null)
                    _createPatientCommand = new CreatePatientCommand(this);
                return _createPatientCommand;
            }
            set { _createPatientCommand = value; }
        }


        public EntitySet<Patient> PatientList { get; set; }
        public EntitySet<Doctor> DoctorList { get; set; }
        public EntitySet<PatientDoctor> PatientDoctors { get; set; }

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
        private M2MDemoDomainContext _c;
        public M2MDemoDomainContext c
        {
            get
            {
                if (_c == null)
                {
                    _c = new M2MDemoDomainContext();
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
                    if (callback.HasError == false)
                    {
                        Patient ps = c.Patients.First();
                    }
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
            var x = homeModel.c.EntityContainer.GetChanges();
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
    public class AddDoctorCommand : MyCommand
    {
        private HomeModel homeModel;
        public AddDoctorCommand(HomeModel homeModel)
        {
            this.homeModel = homeModel;
            homeModel.PropertyChanged += homeModel_PropertyChanged;
        }

        void homeModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SelectedPatient" || e.PropertyName == "SelectedDoctor")
            {
                CanExecute(null);
            }
        }

        public override void Execute(object parameter)
        {
            homeModel.SelectedPatient.DoctorSet.Add(homeModel.SelectedDoctor);
            homeModel.AutoSaveChanges();
            CanExecute(parameter);
        }

        protected override bool CheckCanExecute(object parameter)
        {
            return homeModel.SelectedDoctor != null && homeModel.SelectedPatient != null &&
                homeModel.SelectedPatient.DoctorSet.Contains(homeModel.SelectedDoctor) == false;
        }
    }
    public class DeleteDoctorCommand : MyCommand
    {
        HomeModel homeModel;
        public DeleteDoctorCommand(HomeModel homeModel)
        {
            this.homeModel = homeModel;
            homeModel.PropertyChanged += homeModel_PropertyChanged;
        }

        void homeModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SelectedPatient" || e.PropertyName == "SelectedPatientDoctor")
                CanExecute(null);
        }
        protected override bool CheckCanExecute(object parameter)
        {
            return homeModel.SelectedPatient != null && homeModel.SelectedPatientDoctor != null;
        }

        public override void Execute(object parameter)
        {
            homeModel.SelectedPatient.DoctorSet.Remove(homeModel.SelectedPatientDoctor);
            homeModel.AutoSaveChanges();
            CanExecute(parameter);
        }
    }
    public class CreateDoctorCommand : MyCommand
    {
        HomeModel homeModel;
        public CreateDoctorCommand(HomeModel homeModel)
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

        private int doctorCount;
        public override void Execute(object parameter)
        {
            homeModel.c.Doctors.Add(new Doctor { Name = "Doctor" + doctorCount++ });
            homeModel.AutoSaveChanges();
        }
    }
    public class AddPatientCommand : MyCommand
    {
        HomeModel homeModel;
        public AddPatientCommand(HomeModel homeModel)
        {
            this.homeModel = homeModel;
            homeModel.PropertyChanged += homeModel_PropertyChanged;
        }

        void homeModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SelectedDoctor" || e.PropertyName == "SelectedPatient")
                CanExecute(null);
        }
        protected override bool CheckCanExecute(object parameter)
        {
            return homeModel.SelectedDoctor != null && homeModel.SelectedPatient != null &&
                homeModel.SelectedDoctor.PatientSet.Contains(homeModel.SelectedPatient) == false;
        }

        public override void Execute(object parameter)
        {
            homeModel.SelectedDoctor.PatientSet.Add(homeModel.SelectedPatient);
            homeModel.AutoSaveChanges();
            CanExecute(parameter);
        }
    }
    public class DeletePatientCommand : MyCommand
    {
        HomeModel homeModel;
        public DeletePatientCommand(HomeModel homeModel)
        {
            this.homeModel = homeModel;
            homeModel.PropertyChanged += homeModel_PropertyChanged;
        }

        void homeModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SelectedDoctor" || e.PropertyName == "SelectedDoctorPatient")
                CanExecute(null);
        }
        protected override bool CheckCanExecute(object parameter)
        {
            return homeModel.SelectedDoctor != null && homeModel.SelectedDoctorPatient != null;
        }

        public override void Execute(object parameter)
        {
            homeModel.SelectedDoctor.PatientSet.Remove(homeModel.SelectedDoctorPatient);
            homeModel.AutoSaveChanges();
            CanExecute(parameter);
        }
    }
    public class CreatePatientCommand : MyCommand
    {
        HomeModel homeModel;
        public CreatePatientCommand(HomeModel homeModel)
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

        private int patientCount;
        public override void Execute(object parameter)
        {
            homeModel.c.Patients.Add(new Patient { Name = "Patient" + patientCount++ });
            homeModel.AutoSaveChanges();
        }
    }
}
