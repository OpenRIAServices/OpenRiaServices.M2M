 

// RIAM2MShared.ttinclude has been located and loaded.

 

#pragma warning disable 618

#region Domain Service

namespace ClientTests.Web
{
	using System;
	using System.Linq;
	using System.Data;
	using System.Data.Objects;
	using ClientTests.Web;
	
	public partial class DemoService
	{
		[Obsolete("This method is only intended for use by the RIA M2M solution")]
		public IQueryable<AnimalVet> GetAnimalVetQuery()
		{
			throw new System.NotImplementedException();
		}
		
		[Obsolete("This method is only intended for use by the RIA M2M solution")]
		public void InsertAnimalVet(AnimalVet linkEntity)
		{
			// ** Process Animal end **
			Animal end1Entity;
			
			if (linkEntity.Animal != null)
			{
				// If a reference of Animal is in the linkEntity that has been passed to this method, then get it.
				end1Entity = linkEntity.Animal;
			}
            else
            {
                // If there is no reference to Animal in the linkEntity, then build Animal from the AnimalId that has been passed in the linkEntity.
				//
				// Note: In the situation where the entity that we are dealing with is abstract, then use one of it's 
				// derived entities to act as the concrete type for the end.  A derived entity is used in this situation 
				// because you can't instantiate an abstract entity.  The derived entity that you use is not important,
				// since all derived entities will posses the same many to many relationship from the base entity.
                end1Entity = new Animal() { AnimalId = linkEntity.AnimalId };
            }
			
			ObjectStateEntry end1StateEntry;
			
			// Check to see if Animal is already being tracked by they current entity framework object context.
            ObjectContext.ObjectStateManager.TryGetObjectStateEntry(end1Entity, out end1StateEntry);

			if (end1StateEntry != null && end1StateEntry.State != EntityState.Detached)
            {
                // If Animal is already being tracked by the object context, then use the instance of Animal that is being tracked instead of the current Animal
                end1Entity = end1StateEntry.Entity as Animal;
            }
            else
            {
                // If Animal is not being tracked by the object context, then attach it.
                ObjectContext.AttachTo("Animals", end1Entity);
            }
				
				
			// ** Process Vet **
            Vet end2Entity;

            if (linkEntity.Vet != null)
            {
                // If a reference of Vet is in the linkEntity that has been passed to this method, then get it.
                end2Entity = linkEntity.Vet;
            }
            else
            {
                // If there is no reference to Vet in the linkEntity, then build Vet from the VetId that has been passed in the linkEntity.
                //
				// Note: In the situation where the entity that we are dealing with is abstract, then use one of it's 
				// derived entities to act as the concrete type for the end.  A derived entity is used in this situation 
				// because you can't instantiate an abstract entity.  The derived entity that you use is not important,
				// since all derived entities will posses the same many to many relationship from the base entity.
				end2Entity = new Vet() { VetId = linkEntity.VetId };
            }
		
			ObjectStateEntry end2StateEntry;

            // Check to see if Vet is already being tracked by they current entity framework object context.
            ObjectContext.ObjectStateManager.TryGetObjectStateEntry(end2Entity, out end2StateEntry);

            if (end2StateEntry != null && end2StateEntry.State != EntityState.Detached)
            {
                // If Vet is already being tracked by the object context, then use the instance of Vet that is being tracked instead of the current Vet
                end2Entity = end2StateEntry.Entity as Vet;
            }
            else
            {
                // If Vet is not being tracked by the object context, then attach it.
                ObjectContext.AttachTo("Vets", end2Entity);
            }
	
            // ** Add Relationship **
			end1Entity.Vets.Add(end2Entity);
		}
		
		[Obsolete("This method is only intended for use by the RIA M2M solution")]
		public void DeleteAnimalVet(AnimalVet linkEntity)
		{
			// ** Process Animal end **
			Animal end1Entity;
			
			if (linkEntity.Animal != null)
			{
				// If a reference of Animal is in the linkEntity that has been passed to this method, then get it.
				end1Entity = linkEntity.Animal;
			}
            else
            {
                // If there is no reference to Animal in the linkEntity, then build Animal from the AnimalId that has been passed in the linkEntity.
                //
				// Note: In the situation where the entity that we are dealing with is abstract, then use one of it's 
				// derived entities to act as the concrete type for the end.  A derived entity is used in this situation 
				// because you can't instantiate an abstract entity.  The derived entity that you use is not important,
				// since all derived entities will posses the same many to many relationship from the base entity.
				end1Entity = new Animal() { AnimalId = linkEntity.AnimalId };
            }
			
			ObjectStateEntry end1StateEntry;
			
            EntityKey end1Key = ObjectContext.CreateEntityKey("Animals", end1Entity);
			
			// Check to see if Animal is already being tracked by they current entity framework object context.
            ObjectContext.ObjectStateManager.TryGetObjectStateEntry(end1Key, out end1StateEntry);

			if (end1StateEntry != null && end1StateEntry.State != EntityState.Detached)
            {
                // If Animal is already being tracked by the object context, then use the instance of Animal that is being tracked instead of the current Animal
                end1Entity = end1StateEntry.Entity as Animal;
            }
				
			// ** Process Vet **
            Vet end2Entity;

            if (linkEntity.Vet != null)
            {
                // If a reference of Vet is in the linkEntity that has been passed to this method, then get it.
                end2Entity = linkEntity.Vet;
            }
            else
            {
                // If there is no reference to Vet in the linkEntity, then build Vet from the VetId that has been passed in the linkEntity.
                //
				// Note: In the situation where the entity that we are dealing with is abstract, then use one of it's 
				// derived entities to act as the concrete type for the end.  A derived entity is used in this situation 
				// because you can't instantiate an abstract entity.  The derived entity that you use is not important,
				// since all derived entities will posses the same many to many relationship from the base entity.
				end2Entity = new Vet() { VetId = linkEntity.VetId };
            }
		
			ObjectStateEntry end2StateEntry;
            
			EntityKey end2Key = ObjectContext.CreateEntityKey("Vets", end2Entity);

            // Check to see if Vet is already being tracked by they current entity framework object context.
            ObjectContext.ObjectStateManager.TryGetObjectStateEntry(end2Key, out end2StateEntry);

            if (end2StateEntry != null && end2StateEntry.State != EntityState.Detached)
            {
                // If Vet is already being tracked by the object context, then use the instance of Vet that is being tracked instead of the current Vet
                end2Entity = end2StateEntry.Entity as Vet;
            }
	
			// ** Attach Animal to the Object Context if it wasnt already attached **
            if (end1StateEntry == null || end1StateEntry.State == EntityState.Detached)
            {
                // Build many to many relationship between Animal and Vet so it can be removed after being attached.
                end1Entity.Vets.Add(end2Entity);

                // Attach Animal (Vet will be attached indrectly through this method)
                ObjectContext.AttachTo("Animals", end1Entity);
            }

            // ** Remove Relationship **
            end1Entity.Vets.Remove(end2Entity);
		
		}
		
		[Obsolete("This method is only intended for use by the RIA M2M solution")]
		public IQueryable<DogFireHydrant> GetDogFireHydrantQuery()
		{
			throw new System.NotImplementedException();
		}
		
		[Obsolete("This method is only intended for use by the RIA M2M solution")]
		public void InsertDogFireHydrant(DogFireHydrant linkEntity)
		{
			// ** Process Dog end **
			Dog end1Entity;
			
			if (linkEntity.Dog != null)
			{
				// If a reference of Dog is in the linkEntity that has been passed to this method, then get it.
				end1Entity = linkEntity.Dog;
			}
            else
            {
                // If there is no reference to Dog in the linkEntity, then build Dog from the DogId that has been passed in the linkEntity.
				//
				// Note: In the situation where the entity that we are dealing with is abstract, then use one of it's 
				// derived entities to act as the concrete type for the end.  A derived entity is used in this situation 
				// because you can't instantiate an abstract entity.  The derived entity that you use is not important,
				// since all derived entities will posses the same many to many relationship from the base entity.
                end1Entity = new Dog() { AnimalId = linkEntity.DogId };
            }
			
			ObjectStateEntry end1StateEntry;
			
			// Check to see if Dog is already being tracked by they current entity framework object context.
            ObjectContext.ObjectStateManager.TryGetObjectStateEntry(end1Entity, out end1StateEntry);

			if (end1StateEntry != null && end1StateEntry.State != EntityState.Detached)
            {
                // If Dog is already being tracked by the object context, then use the instance of Dog that is being tracked instead of the current Dog
                end1Entity = end1StateEntry.Entity as Dog;
            }
            else
            {
                // If Dog is not being tracked by the object context, then attach it.
                ObjectContext.AttachTo("Animals", end1Entity);
            }
				
				
			// ** Process FireHydrant **
            FireHydrant end2Entity;

            if (linkEntity.FireHydrant != null)
            {
                // If a reference of FireHydrant is in the linkEntity that has been passed to this method, then get it.
                end2Entity = linkEntity.FireHydrant;
            }
            else
            {
                // If there is no reference to FireHydrant in the linkEntity, then build FireHydrant from the FireHydrantId that has been passed in the linkEntity.
                //
				// Note: In the situation where the entity that we are dealing with is abstract, then use one of it's 
				// derived entities to act as the concrete type for the end.  A derived entity is used in this situation 
				// because you can't instantiate an abstract entity.  The derived entity that you use is not important,
				// since all derived entities will posses the same many to many relationship from the base entity.
				end2Entity = new FireHydrant() { FireHydrantId = linkEntity.FireHydrantId };
            }
		
			ObjectStateEntry end2StateEntry;

            // Check to see if FireHydrant is already being tracked by they current entity framework object context.
            ObjectContext.ObjectStateManager.TryGetObjectStateEntry(end2Entity, out end2StateEntry);

            if (end2StateEntry != null && end2StateEntry.State != EntityState.Detached)
            {
                // If FireHydrant is already being tracked by the object context, then use the instance of FireHydrant that is being tracked instead of the current FireHydrant
                end2Entity = end2StateEntry.Entity as FireHydrant;
            }
            else
            {
                // If FireHydrant is not being tracked by the object context, then attach it.
                ObjectContext.AttachTo("FireHydrants", end2Entity);
            }
	
            // ** Add Relationship **
			end1Entity.FireHydrants.Add(end2Entity);
		}
		
		[Obsolete("This method is only intended for use by the RIA M2M solution")]
		public void DeleteDogFireHydrant(DogFireHydrant linkEntity)
		{
			// ** Process Dog end **
			Dog end1Entity;
			
			if (linkEntity.Dog != null)
			{
				// If a reference of Dog is in the linkEntity that has been passed to this method, then get it.
				end1Entity = linkEntity.Dog;
			}
            else
            {
                // If there is no reference to Dog in the linkEntity, then build Dog from the DogId that has been passed in the linkEntity.
                //
				// Note: In the situation where the entity that we are dealing with is abstract, then use one of it's 
				// derived entities to act as the concrete type for the end.  A derived entity is used in this situation 
				// because you can't instantiate an abstract entity.  The derived entity that you use is not important,
				// since all derived entities will posses the same many to many relationship from the base entity.
				end1Entity = new Dog() { AnimalId = linkEntity.DogId };
            }
			
			ObjectStateEntry end1StateEntry;
			
            EntityKey end1Key = ObjectContext.CreateEntityKey("Animals", end1Entity);
			
			// Check to see if Dog is already being tracked by they current entity framework object context.
            ObjectContext.ObjectStateManager.TryGetObjectStateEntry(end1Key, out end1StateEntry);

			if (end1StateEntry != null && end1StateEntry.State != EntityState.Detached)
            {
                // If Dog is already being tracked by the object context, then use the instance of Dog that is being tracked instead of the current Dog
                end1Entity = end1StateEntry.Entity as Dog;
            }
				
			// ** Process FireHydrant **
            FireHydrant end2Entity;

            if (linkEntity.FireHydrant != null)
            {
                // If a reference of FireHydrant is in the linkEntity that has been passed to this method, then get it.
                end2Entity = linkEntity.FireHydrant;
            }
            else
            {
                // If there is no reference to FireHydrant in the linkEntity, then build FireHydrant from the FireHydrantId that has been passed in the linkEntity.
                //
				// Note: In the situation where the entity that we are dealing with is abstract, then use one of it's 
				// derived entities to act as the concrete type for the end.  A derived entity is used in this situation 
				// because you can't instantiate an abstract entity.  The derived entity that you use is not important,
				// since all derived entities will posses the same many to many relationship from the base entity.
				end2Entity = new FireHydrant() { FireHydrantId = linkEntity.FireHydrantId };
            }
		
			ObjectStateEntry end2StateEntry;
            
			EntityKey end2Key = ObjectContext.CreateEntityKey("FireHydrants", end2Entity);

            // Check to see if FireHydrant is already being tracked by they current entity framework object context.
            ObjectContext.ObjectStateManager.TryGetObjectStateEntry(end2Key, out end2StateEntry);

            if (end2StateEntry != null && end2StateEntry.State != EntityState.Detached)
            {
                // If FireHydrant is already being tracked by the object context, then use the instance of FireHydrant that is being tracked instead of the current FireHydrant
                end2Entity = end2StateEntry.Entity as FireHydrant;
            }
	
			// ** Attach Dog to the Object Context if it wasnt already attached **
            if (end1StateEntry == null || end1StateEntry.State == EntityState.Detached)
            {
                // Build many to many relationship between Dog and FireHydrant so it can be removed after being attached.
                end1Entity.FireHydrants.Add(end2Entity);

                // Attach Dog (FireHydrant will be attached indrectly through this method)
                ObjectContext.AttachTo("Animals", end1Entity);
            }

            // ** Remove Relationship **
            end1Entity.FireHydrants.Remove(end2Entity);
		
		}
		
		[Obsolete("This method is only intended for use by the RIA M2M solution")]
		public IQueryable<DogTrainer> GetDogTrainerQuery()
		{
			throw new System.NotImplementedException();
		}
		
		[Obsolete("This method is only intended for use by the RIA M2M solution")]
		public void InsertDogTrainer(DogTrainer linkEntity)
		{
			// ** Process Dog end **
			Dog end1Entity;
			
			if (linkEntity.Dog != null)
			{
				// If a reference of Dog is in the linkEntity that has been passed to this method, then get it.
				end1Entity = linkEntity.Dog;
			}
            else
            {
                // If there is no reference to Dog in the linkEntity, then build Dog from the DogId that has been passed in the linkEntity.
				//
				// Note: In the situation where the entity that we are dealing with is abstract, then use one of it's 
				// derived entities to act as the concrete type for the end.  A derived entity is used in this situation 
				// because you can't instantiate an abstract entity.  The derived entity that you use is not important,
				// since all derived entities will posses the same many to many relationship from the base entity.
                end1Entity = new Dog() { AnimalId = linkEntity.DogId };
            }
			
			ObjectStateEntry end1StateEntry;
			
			// Check to see if Dog is already being tracked by they current entity framework object context.
            ObjectContext.ObjectStateManager.TryGetObjectStateEntry(end1Entity, out end1StateEntry);

			if (end1StateEntry != null && end1StateEntry.State != EntityState.Detached)
            {
                // If Dog is already being tracked by the object context, then use the instance of Dog that is being tracked instead of the current Dog
                end1Entity = end1StateEntry.Entity as Dog;
            }
            else
            {
                // If Dog is not being tracked by the object context, then attach it.
                ObjectContext.AttachTo("Animals", end1Entity);
            }
				
				
			// ** Process Trainer **
            Trainer end2Entity;

            if (linkEntity.Trainer != null)
            {
                // If a reference of Trainer is in the linkEntity that has been passed to this method, then get it.
                end2Entity = linkEntity.Trainer;
            }
            else
            {
                // If there is no reference to Trainer in the linkEntity, then build Trainer from the TrainerId that has been passed in the linkEntity.
                //
				// Note: In the situation where the entity that we are dealing with is abstract, then use one of it's 
				// derived entities to act as the concrete type for the end.  A derived entity is used in this situation 
				// because you can't instantiate an abstract entity.  The derived entity that you use is not important,
				// since all derived entities will posses the same many to many relationship from the base entity.
				end2Entity = new Trainer() { TrainerId = linkEntity.TrainerId };
            }
		
			ObjectStateEntry end2StateEntry;

            // Check to see if Trainer is already being tracked by they current entity framework object context.
            ObjectContext.ObjectStateManager.TryGetObjectStateEntry(end2Entity, out end2StateEntry);

            if (end2StateEntry != null && end2StateEntry.State != EntityState.Detached)
            {
                // If Trainer is already being tracked by the object context, then use the instance of Trainer that is being tracked instead of the current Trainer
                end2Entity = end2StateEntry.Entity as Trainer;
            }
            else
            {
                // If Trainer is not being tracked by the object context, then attach it.
                ObjectContext.AttachTo("Trainers", end2Entity);
            }
	
            // ** Add Relationship **
			end1Entity.Trainers.Add(end2Entity);
		}
		
		[Obsolete("This method is only intended for use by the RIA M2M solution")]
		public void DeleteDogTrainer(DogTrainer linkEntity)
		{
			// ** Process Dog end **
			Dog end1Entity;
			
			if (linkEntity.Dog != null)
			{
				// If a reference of Dog is in the linkEntity that has been passed to this method, then get it.
				end1Entity = linkEntity.Dog;
			}
            else
            {
                // If there is no reference to Dog in the linkEntity, then build Dog from the DogId that has been passed in the linkEntity.
                //
				// Note: In the situation where the entity that we are dealing with is abstract, then use one of it's 
				// derived entities to act as the concrete type for the end.  A derived entity is used in this situation 
				// because you can't instantiate an abstract entity.  The derived entity that you use is not important,
				// since all derived entities will posses the same many to many relationship from the base entity.
				end1Entity = new Dog() { AnimalId = linkEntity.DogId };
            }
			
			ObjectStateEntry end1StateEntry;
			
            EntityKey end1Key = ObjectContext.CreateEntityKey("Animals", end1Entity);
			
			// Check to see if Dog is already being tracked by they current entity framework object context.
            ObjectContext.ObjectStateManager.TryGetObjectStateEntry(end1Key, out end1StateEntry);

			if (end1StateEntry != null && end1StateEntry.State != EntityState.Detached)
            {
                // If Dog is already being tracked by the object context, then use the instance of Dog that is being tracked instead of the current Dog
                end1Entity = end1StateEntry.Entity as Dog;
            }
				
			// ** Process Trainer **
            Trainer end2Entity;

            if (linkEntity.Trainer != null)
            {
                // If a reference of Trainer is in the linkEntity that has been passed to this method, then get it.
                end2Entity = linkEntity.Trainer;
            }
            else
            {
                // If there is no reference to Trainer in the linkEntity, then build Trainer from the TrainerId that has been passed in the linkEntity.
                //
				// Note: In the situation where the entity that we are dealing with is abstract, then use one of it's 
				// derived entities to act as the concrete type for the end.  A derived entity is used in this situation 
				// because you can't instantiate an abstract entity.  The derived entity that you use is not important,
				// since all derived entities will posses the same many to many relationship from the base entity.
				end2Entity = new Trainer() { TrainerId = linkEntity.TrainerId };
            }
		
			ObjectStateEntry end2StateEntry;
            
			EntityKey end2Key = ObjectContext.CreateEntityKey("Trainers", end2Entity);

            // Check to see if Trainer is already being tracked by they current entity framework object context.
            ObjectContext.ObjectStateManager.TryGetObjectStateEntry(end2Key, out end2StateEntry);

            if (end2StateEntry != null && end2StateEntry.State != EntityState.Detached)
            {
                // If Trainer is already being tracked by the object context, then use the instance of Trainer that is being tracked instead of the current Trainer
                end2Entity = end2StateEntry.Entity as Trainer;
            }
	
			// ** Attach Dog to the Object Context if it wasnt already attached **
            if (end1StateEntry == null || end1StateEntry.State == EntityState.Detached)
            {
                // Build many to many relationship between Dog and Trainer so it can be removed after being attached.
                end1Entity.Trainers.Add(end2Entity);

                // Attach Dog (Trainer will be attached indrectly through this method)
                ObjectContext.AttachTo("Animals", end1Entity);
            }

            // ** Remove Relationship **
            end1Entity.Trainers.Remove(end2Entity);
		
		}
		

	}
}

#endregion

#pragma warning restore 618

