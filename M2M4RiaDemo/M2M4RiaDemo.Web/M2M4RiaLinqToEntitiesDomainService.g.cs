


// M2M4RiaShared.ttinclude has been located and loaded.


// Instruct compiler not to warn about usage of obsolete members, because using them is intended.
#pragma warning disable 618

#region Domain Service

namespace M2M4RiaDemo.Web.Service
{
    using System;
    using System.Data;
    using System.Data.Objects;
    using System.Data.Objects.DataClasses;
	using System.Linq;
    using M2M4RiaDemo.Web.Model;
	
    /// <summary>
    /// This class defines Create and Delete operations for the following many-2-many relation(s):
    ///
    ///   Dog <--> Trainer
    ///
    /// We use stub entities to represent entities for which only the foreign key property is available in join type objects.
    ///
    /// Note: If an entity type is abstract, we use one of its derived entities to act as the concrete type for the stub entity,
    /// because we can't instantiate the abstract type. The derived entity type that we use is not important, since all derived
    /// entities types will posses the same many to many relationship from the base entity.
    ///
    /// Note: We generate Update operations for the join types to deal with a WCF RIA bug (http://forums.silverlight.net/forums/p/201613/470578.aspx#470578).
    /// Update operations are really not needed and also not used.
    /// </summary>
    public partial class M2M4RiaDemoService
    {
        [Obsolete("This method is only intended for use by the RIA M2M solution")]
        public void UpdateDogTrainer(DogTrainer dogTrainer)
        {
            throw new NotSupportedException("Update operation on DogTrainer is not supported.");
        }
        [Obsolete("This method is only intended for use by the RIA M2M solution")]
        public void InsertDogTrainer(DogTrainer dogTrainer)
        {
            Dog dog = dogTrainer.Dog;
            if(dog == null)
            {
			   dog = ChangeSet.ChangeSetEntries.Select(cse => cse.Entity)
			      .OfType<Dog>()
				  .SingleOrDefault(e => e.DogId == dogTrainer.DogId );
			}
            if(dog == null)
            {
                Dog dogStubEntity = new Dog { DogId = dogTrainer.DogId };
                dog = GetEntityByKey<Dog>(ObjectContext, "Dogs", dogStubEntity);
            }
            Trainer trainer = dogTrainer.Trainer;
            if(trainer == null)
            {
			   trainer = ChangeSet.ChangeSetEntries.Select(cse => cse.Entity)
			      .OfType<Trainer>()
				  .SingleOrDefault(e => e.TrainerId == dogTrainer.TrainerId );
			}
            if(trainer == null)
            {
                Trainer trainerStubEntity = new Trainer { TrainerId = dogTrainer.TrainerId };
                trainer = GetEntityByKey<Trainer>(ObjectContext, "Trainers", trainerStubEntity);
            }
            dog.Trainers.Add(trainer);
        }
        [Obsolete("This method is only intended for use by the RIA M2M solution")]
        public void DeleteDogTrainer(DogTrainer dogTrainer)
        {
            Dog dog = dogTrainer.Dog;
            if(dog == null)
            {
			   dog = ChangeSet.ChangeSetEntries.Select(cse => cse.Entity)
			      .OfType<Dog>()
				  .SingleOrDefault(e => e.DogId == dogTrainer.DogId );
			}
            if(dog == null)
            {
                Dog dogStubEntity = new Dog { DogId = dogTrainer.DogId };
                dog = GetEntityByKey<Dog>(ObjectContext, "Dogs", dogStubEntity);
            }
            Trainer trainer = dogTrainer.Trainer;
            if(trainer == null)
            {
			   trainer = ChangeSet.ChangeSetEntries.Select(cse => cse.Entity)
			      .OfType<Trainer>()
				  .SingleOrDefault(e => e.TrainerId == dogTrainer.TrainerId );
			}
            if(trainer == null)
            {
                Trainer trainerStubEntity = new Trainer { TrainerId = dogTrainer.TrainerId };
                trainer = GetEntityByKey<Trainer>(ObjectContext, "Trainers", trainerStubEntity);
            }
            if(dog.Trainers.IsLoaded == false)
			{
			    dog.Trainers.Attach(trainer);
			}
            dog.Trainers.Remove(trainer);
        }

#region GetEntityByKey
        /// <summary>
        /// http://blogs.msdn.com/b/alexj/archive/2009/06/19/tip-26-how-to-avoid-database-queries-using-stub-entities.aspx
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ctx"></param>
        /// <param name="qualifiedEntitySetName"></param>
        /// <param name="stubEntity"></param>
        /// <returns></returns>
        private static T GetEntityByKey<T>(ObjectContext ctx, string qualifiedEntitySetName, T stubEntity)
        {
            ObjectStateEntry state;
            EntityKey key = ctx.CreateEntityKey(qualifiedEntitySetName, stubEntity);
            if (ctx.ObjectStateManager.TryGetObjectStateEntry(key, out state) == false)
            {
                ctx.AttachTo(qualifiedEntitySetName, stubEntity);
                return stubEntity;
            }
            else
            {
                return (T)state.Entity;
            }
        }
#endregion
    }
}

#endregion

// Restore compiler warnings when using obsolete methods
#pragma warning restore 618


