
namespace M2M4RiaDemo.Web.Service
{
    using System.Data;
    using System.Linq;
    using System.ServiceModel.DomainServices.EntityFramework;
    using System.ServiceModel.DomainServices.Hosting;
    using M2M4RiaDemo.Web.Model;
    using System.ServiceModel.DomainServices.Server;


    // Implements application logic using the M2M4RiaDemoModelContainer context.
    // TODO: Add your application logic to these methods or in additional methods.
    // TODO: Wire up authentication (Windows/ASP.NET Forms) and uncomment the following to disable anonymous access
    // Also consider adding roles to restrict access as appropriate.
    // [RequiresAuthentication]
    [EnableClientAccess()]
    public partial class M2M4RiaDemoService : LinqToEntitiesDomainService<M2M4RiaDemoModelContainer>
    {
        /// <summary>
        /// Generate the demo database.
        /// </summary>
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
        // To support paging you will need to add ordering to the 'Dogs' query.
        public IQueryable<Dog> GetDogs()
        {
            return this.ObjectContext.Dogs.Include("Trainers");
        }

        public void InsertDog(Dog dog)
        {
            if ((dog.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(dog, EntityState.Added);
            }
            else
            {
                this.ObjectContext.Dogs.AddObject(dog);
            }
        }

        public void UpdateDog(Dog currentDog)
        {
            this.ObjectContext.Dogs.AttachAsModified(currentDog, this.ChangeSet.GetOriginal(currentDog));
        }

        public void DeleteDog(Dog dog)
        {
            if ((dog.EntityState == EntityState.Detached))
            {
                this.ObjectContext.Dogs.Attach(dog);
            }
            this.ObjectContext.Dogs.DeleteObject(dog);
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Trainers' query.
        public IQueryable<Trainer> GetTrainers()
        {
            return this.ObjectContext.Trainers.Include("Dogs");
        }

        public void InsertTrainer(Trainer trainer)
        {
            if ((trainer.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(trainer, EntityState.Added);
            }
            else
            {
                this.ObjectContext.Trainers.AddObject(trainer);
            }
        }

        public void UpdateTrainer(Trainer currentTrainer)
        {
            this.ObjectContext.Trainers.AttachAsModified(currentTrainer, this.ChangeSet.GetOriginal(currentTrainer));
        }

        public void DeleteTrainer(Trainer trainer)
        {
            if ((trainer.EntityState == EntityState.Detached))
            {
                this.ObjectContext.Trainers.Attach(trainer);
            }
            this.ObjectContext.Trainers.DeleteObject(trainer);
        }
    }
}


