using System.Data;
using System.Linq;
using System.ServiceModel.DomainServices.EntityFramework;
using System.ServiceModel.DomainServices.Hosting;
using System.ServiceModel.DomainServices.Server;

namespace M2MDemo.Web
{
    // Implements application logic using the M2MModelContainer context.
    // TODO: Add your application logic to these methods or in additional methods.
    // TODO: Wire up authentication (Windows/ASP.NET Forms) and uncomment the following to disable anonymous access
    // Also consider adding roles to restrict access as appropriate.
    // [RequiresAuthentication]
    [EnableClientAccess()]
    public partial class M2MDemoDomainService : LinqToEntitiesDomainService<M2MModelContainer>
    {
        protected override void OnError(System.ServiceModel.DomainServices.Server.DomainServiceErrorInfo errorInfo)
        {
            base.OnError(errorInfo);
        }

        [Invoke]
        public void CreateDataBase()
        {
            if (this.ObjectContext.DatabaseExists() == false)
            {
                this.ObjectContext.CreateDatabase();
            }
        }
        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Doctors' query.
        public IQueryable<Doctor> GetDoctors()
        {
            return this.ObjectContext.Doctors.Include("PatientSet");
        }
        public void InsertDoctor(Doctor doctor)
        {
            if ((doctor.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(doctor, EntityState.Added);
            }
            else
            {
                this.ObjectContext.Doctors.AddObject(doctor);
            }
        }

        public void UpdateDoctor(Doctor currentDoctor)
        {
            this.ObjectContext.Doctors.AttachAsModified(currentDoctor, this.ChangeSet.GetOriginal(currentDoctor));
        }

        public void DeleteDoctor(Doctor doctor)
        {
            if ((doctor.EntityState == EntityState.Detached))
            {
                this.ObjectContext.Doctors.Attach(doctor);
            }
            this.ObjectContext.Doctors.DeleteObject(doctor);
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Patients' query.
        public IQueryable<Patient> GetPatients()
        {
            return this.ObjectContext.Patients.Include("DoctorSet");
        }

        public void InsertPatient(Patient patient)
        {
            if ((patient.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(patient, EntityState.Added);
            }
            else
            {
                this.ObjectContext.Patients.AddObject(patient);
            }
        }

        public void UpdatePatient(Patient currentPatient)
        {
            this.ObjectContext.Patients.AttachAsModified(currentPatient, this.ChangeSet.GetOriginal(currentPatient));
        }

        public void DeletePatient(Patient patient)
        {
            if ((patient.EntityState == EntityState.Detached))
            {
                this.ObjectContext.Patients.Attach(patient);
            }
            this.ObjectContext.Patients.DeleteObject(patient);
        }
    }
}


