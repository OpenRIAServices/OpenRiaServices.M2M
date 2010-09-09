

 

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
	
	public partial class DemoService
	{
	[Obsolete("This method is only intended for use by the RIA M2M solution")]
	public void InsertAnimalVet(AnimalVet linkEntity)
	{
			// ** Process Animal end **
			Animal animal;
			
			if (linkEntity.Animal != null)
			{
				// If a reference of Animal is in the linkEntity that has been passed to this method, then get it.
				animal = linkEntity.Animal;
			}
			else
			{
				// If there is no reference to Animal in the linkEntity, then build Animal 
				// from the AnimalId that has been passed in the linkEntity.
				//
				// Note: In the situation where the entity that we are dealing with is abstract, then use one of it's 
				// derived entities to act as the concrete type for the end.  A derived entity is used in this situation 
				// because you can't instantiate an abstract entity.  The derived entity that you use is not important,
				// since all derived entities will posses the same many to many relationship from the base entity.
				Animal animalStubEntity = new Animal() { AnimalId = linkEntity.AnimalId };
				animal = GetEntityByKey<Animal>(ObjectContext, "Animals", animalStubEntity);
			}							
			// ** Process Vet end **
			Vet vet;
			
			if (linkEntity.Vet != null)
			{
				// If a reference of Vet is in the linkEntity that has been passed to this method, then get it.
				vet = linkEntity.Vet;
			}
			else
			{
				// If there is no reference to Vet in the linkEntity, then build Vet 
				// from the VetId that has been passed in the linkEntity.
				//
				// Note: In the situation where the entity that we are dealing with is abstract, then use one of it's 
				// derived entities to act as the concrete type for the end.  A derived entity is used in this situation 
				// because you can't instantiate an abstract entity.  The derived entity that you use is not important,
				// since all derived entities will posses the same many to many relationship from the base entity.
				Vet vetStubEntity = new Vet() { VetId = linkEntity.VetId };
				vet = GetEntityByKey<Vet>(ObjectContext, "Vets", vetStubEntity);
			}							
		animal.Vets.Add(vet);
	}
	[Obsolete("This method is only intended for use by the RIA M2M solution")]
	public void DeleteAnimalVet(AnimalVet linkEntity)
	{
			// ** Process Animal end **
			Animal animal;
			
			if (linkEntity.Animal != null)
			{
				// If a reference of Animal is in the linkEntity that has been passed to this method, then get it.
				animal = linkEntity.Animal;
			}
			else
			{
				// If there is no reference to Animal in the linkEntity, then build Animal 
				// from the AnimalId that has been passed in the linkEntity.
				//
				// Note: In the situation where the entity that we are dealing with is abstract, then use one of it's 
				// derived entities to act as the concrete type for the end.  A derived entity is used in this situation 
				// because you can't instantiate an abstract entity.  The derived entity that you use is not important,
				// since all derived entities will posses the same many to many relationship from the base entity.
				Animal animalStubEntity = new Animal() { AnimalId = linkEntity.AnimalId };
				animal = GetEntityByKey<Animal>(ObjectContext, "Animals", animalStubEntity);
			}							
			// ** Process Vet end **
			Vet vet;
			
			if (linkEntity.Vet != null)
			{
				// If a reference of Vet is in the linkEntity that has been passed to this method, then get it.
				vet = linkEntity.Vet;
			}
			else
			{
				// If there is no reference to Vet in the linkEntity, then build Vet 
				// from the VetId that has been passed in the linkEntity.
				//
				// Note: In the situation where the entity that we are dealing with is abstract, then use one of it's 
				// derived entities to act as the concrete type for the end.  A derived entity is used in this situation 
				// because you can't instantiate an abstract entity.  The derived entity that you use is not important,
				// since all derived entities will posses the same many to many relationship from the base entity.
				Vet vetStubEntity = new Vet() { VetId = linkEntity.VetId };
				vet = GetEntityByKey<Vet>(ObjectContext, "Vets", vetStubEntity);
			}							
			animal.Vets.Remove(vet);
		
		}
	[Obsolete("This method is only intended for use by the RIA M2M solution")]
	public void InsertDogFireHydrant(DogFireHydrant linkEntity)
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
				Dog dogStubEntity = new Dog() { AnimalId = linkEntity.DogId };
				dog = GetEntityByKey<Dog>(ObjectContext, "Animals", dogStubEntity);
			}							
			// ** Process FireHydrant end **
			FireHydrant fireHydrant;
			
			if (linkEntity.FireHydrant != null)
			{
				// If a reference of FireHydrant is in the linkEntity that has been passed to this method, then get it.
				fireHydrant = linkEntity.FireHydrant;
			}
			else
			{
				// If there is no reference to FireHydrant in the linkEntity, then build FireHydrant 
				// from the FireHydrantId that has been passed in the linkEntity.
				//
				// Note: In the situation where the entity that we are dealing with is abstract, then use one of it's 
				// derived entities to act as the concrete type for the end.  A derived entity is used in this situation 
				// because you can't instantiate an abstract entity.  The derived entity that you use is not important,
				// since all derived entities will posses the same many to many relationship from the base entity.
				FireHydrant fireHydrantStubEntity = new FireHydrant() { FireHydrantId = linkEntity.FireHydrantId };
				fireHydrant = GetEntityByKey<FireHydrant>(ObjectContext, "FireHydrants", fireHydrantStubEntity);
			}							
		dog.FireHydrants.Add(fireHydrant);
	}
	[Obsolete("This method is only intended for use by the RIA M2M solution")]
	public void DeleteDogFireHydrant(DogFireHydrant linkEntity)
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
				Dog dogStubEntity = new Dog() { AnimalId = linkEntity.DogId };
				dog = GetEntityByKey<Dog>(ObjectContext, "Animals", dogStubEntity);
			}							
			// ** Process FireHydrant end **
			FireHydrant fireHydrant;
			
			if (linkEntity.FireHydrant != null)
			{
				// If a reference of FireHydrant is in the linkEntity that has been passed to this method, then get it.
				fireHydrant = linkEntity.FireHydrant;
			}
			else
			{
				// If there is no reference to FireHydrant in the linkEntity, then build FireHydrant 
				// from the FireHydrantId that has been passed in the linkEntity.
				//
				// Note: In the situation where the entity that we are dealing with is abstract, then use one of it's 
				// derived entities to act as the concrete type for the end.  A derived entity is used in this situation 
				// because you can't instantiate an abstract entity.  The derived entity that you use is not important,
				// since all derived entities will posses the same many to many relationship from the base entity.
				FireHydrant fireHydrantStubEntity = new FireHydrant() { FireHydrantId = linkEntity.FireHydrantId };
				fireHydrant = GetEntityByKey<FireHydrant>(ObjectContext, "FireHydrants", fireHydrantStubEntity);
			}							
			dog.FireHydrants.Remove(fireHydrant);
		
		}
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
				Dog dogStubEntity = new Dog() { AnimalId = linkEntity.DogId };
				dog = GetEntityByKey<Dog>(ObjectContext, "Animals", dogStubEntity);
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
				Dog dogStubEntity = new Dog() { AnimalId = linkEntity.DogId };
				dog = GetEntityByKey<Dog>(ObjectContext, "Animals", dogStubEntity);
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


