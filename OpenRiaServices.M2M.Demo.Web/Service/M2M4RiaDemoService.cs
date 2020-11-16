using System.Data.Entity;
using System.Linq;
using OpenRiaServices.EntityFramework;
using OpenRiaServices.Hosting;
using OpenRiaServices.Server;
using OpenRiaServices.FluentMetadata;
using OpenRiaServices.M2M.DbContext;
using OpenRiaServices.M2M.Demo.Web.Model;

namespace OpenRiaServices.M2M.Demo.Web.Service
{
    // Implements application logic using the DogTrainerModel context.
    [EnableClientAccess]
    [FluentMetadata(typeof(MetadataConfiguration))]
    public class M2M4RiaDemoService : DbDomainService<DogTrainerModel>
    {
        public M2M4RiaDemoService()
        {
            DbContext.Database.CreateIfNotExists();
        }

        #region Public Methods and Operators

        public void DeleteDog(Dog dog)
        {
            var entityEntry = DbContext.Entry(dog);
            if((entityEntry.State != EntityState.Deleted))
            {
                entityEntry.State = EntityState.Deleted;
            }
            else
            {
                DbContext.DogSet.Attach(dog);
                DbContext.DogSet.Remove(dog);
            }
        }

        public void DeleteDogTrainer(DogTrainer dogTrainer)
        {
            var dog = dogTrainer.FetchObject1(ChangeSet, DbContext);
            var trainer = dogTrainer.FetchObject2(ChangeSet, DbContext);
            DbContext.LoadM2M<Dog, Trainer, DogTrainer>(dog, trainer);

            dog.Trainers.Remove(trainer);
            DbContext.ChangeTracker.DetectChanges();
        }

        public void DeleteTrainer(Trainer trainer)
        {
            var entityEntry = DbContext.Entry(trainer);
            if((entityEntry.State != EntityState.Deleted))
            {
                entityEntry.State = EntityState.Deleted;
            }
            else
            {
                DbContext.TrainerSet.Attach(trainer);
                DbContext.TrainerSet.Remove(trainer);
            }
        }

        public IQueryable<Dog> GetDogs()
        {
            var result = DbContext.DogSet.Include(x => x.Trainers);
            return result;
        }

        public IQueryable<Trainer> GetTrainers()
        {
            return DbContext.TrainerSet.Include(x => x.Dogs);
        }
        public override void Initialize(DomainServiceContext context)
        {
            base.Initialize(context);
            DbContext.Database.CreateIfNotExists();
        }

        public void InsertDog(Dog dog)
        {
            var entityEntry = DbContext.Entry(dog);
            if((entityEntry.State != EntityState.Detached))
            {
                entityEntry.State = EntityState.Added;
            }
            else
            {
                DbContext.DogSet.Add(dog);
            }
        }

        public void InsertDogTrainer(DogTrainer dogTrainer)
        {
            var dog = dogTrainer.FetchObject1(ChangeSet, DbContext);
            var trainer = dogTrainer.FetchObject2(ChangeSet, DbContext);
            dog.Trainers.Add(trainer);
            DbContext.ChangeTracker.DetectChanges();
        }

        public void InsertTrainer(Trainer trainer)
        {
            var entityEntry = DbContext.Entry(trainer);
            if((entityEntry.State != EntityState.Detached))
            {
                entityEntry.State = EntityState.Added;
            }
            else
            {
                DbContext.TrainerSet.Add(trainer);
            }
        }

        public void UpdateDog(Dog currentDog)
        {
            DbContext.DogSet.AttachAsModified(currentDog, ChangeSet.GetOriginal(currentDog), DbContext);
        }

        public void UpdateTrainer(Trainer currentTrainer)
        {
            DbContext.TrainerSet.AttachAsModified(currentTrainer, ChangeSet.GetOriginal(currentTrainer), DbContext);
        }

        #endregion

        #region Methods

        protected override void OnError(DomainServiceErrorInfo errorInfo)
        {
            base.OnError(errorInfo);
        }

        #endregion
    }
}