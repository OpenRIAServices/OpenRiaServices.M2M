

 

// RIAM2MShared.ttinclude has been located and loaded.

 

// Instruct compiler not to warn about usage of obsolete members, because using them is intended.
#pragma warning disable 618

#region Domain Service

namespace M2M4RiaDemo.Web.Service
{
	using System;
	using System.Linq;
	using System.Data;
	using System.Data.Objects;
	using System.Data.Objects.DataClasses;
	
	using M2M4RiaDemo.Web.Model;
	
	public partial class M2M4RiaDemoService
	{
	[Obsolete("This method is only intended for use by the RIA M2M solution")]
	public void InsertDogTrainer(DogTrainer linkEntity)
	{
			// ** Process Dog end **
			Dog dog;
			
			if (linkEntity.Dog != null)
			{
				// If a reference of Dog is in the linkEntity that has been passed to this method, then get it.
				dog = linkEntity.Dog;
			}
			else
			{
				// If there is no reference to Dog in the linkEntity, then build Dog 
				// from the DogId that has been passed in the linkEntity.
				//
				// Note: In the situation where the entity that we are dealing with is abstract, then use one of it's 
				// derived entities to act as the concrete type for the end.  A derived entity is used in this situation 
				// because you can't instantiate an abstract entity.  The derived entity that you use is not important,
				// since all derived entities will posses the same many to many relationship from the base entity.
				Dog dogStubEntity = new Dog() { DogId = linkEntity.DogId };
				dog = GetEntityByKey<Dog>(ObjectContext, "Dogs", dogStubEntity);
			}							
			// ** Process Trainer end **
			Trainer trainer;
			
			if (linkEntity.Trainer != null)
			{
				// If a reference of Trainer is in the linkEntity that has been passed to this method, then get it.
				trainer = linkEntity.Trainer;
			}
			else
			{
				// If there is no reference to Trainer in the linkEntity, then build Trainer 
				// from the TrainerId that has been passed in the linkEntity.
				//
				// Note: In the situation where the entity that we are dealing with is abstract, then use one of it's 
				// derived entities to act as the concrete type for the end.  A derived entity is used in this situation 
				// because you can't instantiate an abstract entity.  The derived entity that you use is not important,
				// since all derived entities will posses the same many to many relationship from the base entity.
				Trainer trainerStubEntity = new Trainer() { TrainerId = linkEntity.TrainerId };
				trainer = GetEntityByKey<Trainer>(ObjectContext, "Trainers", trainerStubEntity);
			}							
		dog.Trainers.Add(trainer);
	}
	[Obsolete("This method is only intended for use by the RIA M2M solution")]
	public void DeleteDogTrainer(DogTrainer linkEntity)
	{
			// ** Process Dog end **
			Dog dog;
			
			if (linkEntity.Dog != null)
			{
				// If a reference of Dog is in the linkEntity that has been passed to this method, then get it.
				dog = linkEntity.Dog;
			}
			else
			{
				// If there is no reference to Dog in the linkEntity, then build Dog 
				// from the DogId that has been passed in the linkEntity.
				//
				// Note: In the situation where the entity that we are dealing with is abstract, then use one of it's 
				// derived entities to act as the concrete type for the end.  A derived entity is used in this situation 
				// because you can't instantiate an abstract entity.  The derived entity that you use is not important,
				// since all derived entities will posses the same many to many relationship from the base entity.
				Dog dogStubEntity = new Dog() { DogId = linkEntity.DogId };
				dog = GetEntityByKey<Dog>(ObjectContext, "Dogs", dogStubEntity);
			}							
			// ** Process Trainer end **
			Trainer trainer;
			
			if (linkEntity.Trainer != null)
			{
				// If a reference of Trainer is in the linkEntity that has been passed to this method, then get it.
				trainer = linkEntity.Trainer;
			}
			else
			{
				// If there is no reference to Trainer in the linkEntity, then build Trainer 
				// from the TrainerId that has been passed in the linkEntity.
				//
				// Note: In the situation where the entity that we are dealing with is abstract, then use one of it's 
				// derived entities to act as the concrete type for the end.  A derived entity is used in this situation 
				// because you can't instantiate an abstract entity.  The derived entity that you use is not important,
				// since all derived entities will posses the same many to many relationship from the base entity.
				Trainer trainerStubEntity = new Trainer() { TrainerId = linkEntity.TrainerId };
				trainer = GetEntityByKey<Trainer>(ObjectContext, "Trainers", trainerStubEntity);
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
        private static T GetEntityByKey<T>(ObjectContext ctx, string qualifiedEntitySetName, T stubEntity) where T : EntityObject
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


