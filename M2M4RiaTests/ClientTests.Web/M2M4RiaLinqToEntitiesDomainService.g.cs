


// M2M4RiaShared.ttinclude has been located and loaded.


// Instruct compiler not to warn about usage of obsolete members, because using them is intended.
#pragma warning disable 618

#region Domain Service

namespace ClientTests.Web
{
    using System;
    using System.Data;
    using System.Data.Objects;
    using System.Data.Objects.DataClasses;
	using System.Linq;
	
    /// <summary>
    /// This class defines Create and Delete operations for the following many-2-many relation(s):
    ///
    ///   Animal &lt;--&gt; Vet
    ///   Dog &lt;--&gt; FireHydrant
    ///   Dog &lt;--&gt; Trainer
    ///   Dog &lt;--&gt; Dog
    ///   Cat &lt;--&gt; MarkedTerritory
    ///
    /// We use stub entities to represent entities for which only the foreign key property is available in join type objects.
    ///
    /// Note: If an entity type is abstract, we use one of its derived entities to act as the concrete type for the stub entity,
    /// because we can't instantiate the abstract type. The derived entity type that we use is not important, since all derived
    /// entities types will posses the same many to many relationship from the base entity.
    /// </summary>
    public partial class M2M4RiaTestService
    {
        [Obsolete("This method is only intended for use by the RIA M2M solution")]
        public void InsertAnimalVet(AnimalVet animalVet)
        {
            Animal animal = animalVet.Animal;
            if(animal == null)
            {
			   animal = ChangeSet.ChangeSetEntries.Select(cse => cse.Entity)
			      .OfType<Animal>()
				  .SingleOrDefault(e => e.AnimalId == animalVet.AnimalAnimalId );
			}
            if(animal == null)
            {
                Animal animalStubEntity = new Animal { AnimalId = animalVet.AnimalAnimalId };
                animal = GetEntityByKey<Animal>(ObjectContext, "Animals", animalStubEntity);
            }
            Vet vet = animalVet.Vet;
            if(vet == null)
            {
			   vet = ChangeSet.ChangeSetEntries.Select(cse => cse.Entity)
			      .OfType<Vet>()
				  .SingleOrDefault(e => e.VetId == animalVet.VetVetId );
			}
            if(vet == null)
            {
                Vet vetStubEntity = new Vet { VetId = animalVet.VetVetId };
                vet = GetEntityByKey<Vet>(ObjectContext, "Vets", vetStubEntity);
            }
            animal.Vets.Add(vet);
        }
        [Obsolete("This method is only intended for use by the RIA M2M solution")]
        public void DeleteAnimalVet(AnimalVet animalVet)
        {
            Animal animal = animalVet.Animal;
            if(animal == null)
            {
			   animal = ChangeSet.ChangeSetEntries.Select(cse => cse.Entity)
			      .OfType<Animal>()
				  .SingleOrDefault(e => e.AnimalId == animalVet.AnimalAnimalId );
			}
            if(animal == null)
            {
                Animal animalStubEntity = new Animal { AnimalId = animalVet.AnimalAnimalId };
                animal = GetEntityByKey<Animal>(ObjectContext, "Animals", animalStubEntity);
            }
            Vet vet = animalVet.Vet;
            if(vet == null)
            {
			   vet = ChangeSet.ChangeSetEntries.Select(cse => cse.Entity)
			      .OfType<Vet>()
				  .SingleOrDefault(e => e.VetId == animalVet.VetVetId );
			}
            if(vet == null)
            {
                Vet vetStubEntity = new Vet { VetId = animalVet.VetVetId };
                vet = GetEntityByKey<Vet>(ObjectContext, "Vets", vetStubEntity);
            }
            if(animal.Vets.IsLoaded == false)
            {
			    // We can't attach vet if animal is deleted. In that case we
				// temporarily reset the entity state of animal, then attach vet
				// and set the entity state of animal back to EntityState.Deleted.
                ObjectStateEntry stateEntry = ObjectContext.ObjectStateManager.GetObjectStateEntry(animal);
                EntityState state = stateEntry.State;

                if(state == EntityState.Deleted)
                {
                    stateEntry.ChangeState(EntityState.Unchanged);
                }
                animal.Vets.Attach(vet);
                if(stateEntry.State != state)
                {
                    stateEntry.ChangeState(state);
                }
            }
            animal.Vets.Remove(vet);
        }
        [Obsolete("This method is only intended for use by the RIA M2M solution")]
        public void InsertDogFireHydrant(DogFireHydrant dogFireHydrant)
        {
            Dog dog = dogFireHydrant.Dog;
            if(dog == null)
            {
			   dog = ChangeSet.ChangeSetEntries.Select(cse => cse.Entity)
			      .OfType<Dog>()
				  .SingleOrDefault(e => e.AnimalId == dogFireHydrant.DogAnimalId );
			}
            if(dog == null)
            {
                Dog dogStubEntity = new Dog { AnimalId = dogFireHydrant.DogAnimalId };
                dog = GetEntityByKey<Dog>(ObjectContext, "Animals", dogStubEntity);
            }
            FireHydrant fireHydrant = dogFireHydrant.FireHydrant;
            if(fireHydrant == null)
            {
			   fireHydrant = ChangeSet.ChangeSetEntries.Select(cse => cse.Entity)
			      .OfType<FireHydrant>()
				  .SingleOrDefault(e => e.FireHydrantId == dogFireHydrant.FireHydrantFireHydrantId );
			}
            if(fireHydrant == null)
            {
                FireHydrant fireHydrantStubEntity = new FireHydrant { FireHydrantId = dogFireHydrant.FireHydrantFireHydrantId };
                fireHydrant = GetEntityByKey<FireHydrant>(ObjectContext, "FireHydrants", fireHydrantStubEntity);
            }
            dog.FireHydrants.Add(fireHydrant);
        }
        [Obsolete("This method is only intended for use by the RIA M2M solution")]
        public void DeleteDogFireHydrant(DogFireHydrant dogFireHydrant)
        {
            Dog dog = dogFireHydrant.Dog;
            if(dog == null)
            {
			   dog = ChangeSet.ChangeSetEntries.Select(cse => cse.Entity)
			      .OfType<Dog>()
				  .SingleOrDefault(e => e.AnimalId == dogFireHydrant.DogAnimalId );
			}
            if(dog == null)
            {
                Dog dogStubEntity = new Dog { AnimalId = dogFireHydrant.DogAnimalId };
                dog = GetEntityByKey<Dog>(ObjectContext, "Animals", dogStubEntity);
            }
            FireHydrant fireHydrant = dogFireHydrant.FireHydrant;
            if(fireHydrant == null)
            {
			   fireHydrant = ChangeSet.ChangeSetEntries.Select(cse => cse.Entity)
			      .OfType<FireHydrant>()
				  .SingleOrDefault(e => e.FireHydrantId == dogFireHydrant.FireHydrantFireHydrantId );
			}
            if(fireHydrant == null)
            {
                FireHydrant fireHydrantStubEntity = new FireHydrant { FireHydrantId = dogFireHydrant.FireHydrantFireHydrantId };
                fireHydrant = GetEntityByKey<FireHydrant>(ObjectContext, "FireHydrants", fireHydrantStubEntity);
            }
            if(dog.FireHydrants.IsLoaded == false)
            {
			    // We can't attach fireHydrant if dog is deleted. In that case we
				// temporarily reset the entity state of dog, then attach fireHydrant
				// and set the entity state of dog back to EntityState.Deleted.
                ObjectStateEntry stateEntry = ObjectContext.ObjectStateManager.GetObjectStateEntry(dog);
                EntityState state = stateEntry.State;

                if(state == EntityState.Deleted)
                {
                    stateEntry.ChangeState(EntityState.Unchanged);
                }
                dog.FireHydrants.Attach(fireHydrant);
                if(stateEntry.State != state)
                {
                    stateEntry.ChangeState(state);
                }
            }
            dog.FireHydrants.Remove(fireHydrant);
        }
        [Obsolete("This method is only intended for use by the RIA M2M solution")]
        public void InsertDogTrainer(DogTrainer dogTrainer)
        {
            Dog dog = dogTrainer.Dog;
            if(dog == null)
            {
			   dog = ChangeSet.ChangeSetEntries.Select(cse => cse.Entity)
			      .OfType<Dog>()
				  .SingleOrDefault(e => e.AnimalId == dogTrainer.DogAnimalId );
			}
            if(dog == null)
            {
                Dog dogStubEntity = new Dog { AnimalId = dogTrainer.DogAnimalId };
                dog = GetEntityByKey<Dog>(ObjectContext, "Animals", dogStubEntity);
            }
            Trainer trainer = dogTrainer.Trainer;
            if(trainer == null)
            {
			   trainer = ChangeSet.ChangeSetEntries.Select(cse => cse.Entity)
			      .OfType<Trainer>()
				  .SingleOrDefault(e => e.TrainerId == dogTrainer.TrainerTrainerId );
			}
            if(trainer == null)
            {
                Trainer trainerStubEntity = new Trainer { TrainerId = dogTrainer.TrainerTrainerId };
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
				  .SingleOrDefault(e => e.AnimalId == dogTrainer.DogAnimalId );
			}
            if(dog == null)
            {
                Dog dogStubEntity = new Dog { AnimalId = dogTrainer.DogAnimalId };
                dog = GetEntityByKey<Dog>(ObjectContext, "Animals", dogStubEntity);
            }
            Trainer trainer = dogTrainer.Trainer;
            if(trainer == null)
            {
			   trainer = ChangeSet.ChangeSetEntries.Select(cse => cse.Entity)
			      .OfType<Trainer>()
				  .SingleOrDefault(e => e.TrainerId == dogTrainer.TrainerTrainerId );
			}
            if(trainer == null)
            {
                Trainer trainerStubEntity = new Trainer { TrainerId = dogTrainer.TrainerTrainerId };
                trainer = GetEntityByKey<Trainer>(ObjectContext, "Trainers", trainerStubEntity);
            }
            if(dog.Trainers.IsLoaded == false)
            {
			    // We can't attach trainer if dog is deleted. In that case we
				// temporarily reset the entity state of dog, then attach trainer
				// and set the entity state of dog back to EntityState.Deleted.
                ObjectStateEntry stateEntry = ObjectContext.ObjectStateManager.GetObjectStateEntry(dog);
                EntityState state = stateEntry.State;

                if(state == EntityState.Deleted)
                {
                    stateEntry.ChangeState(EntityState.Unchanged);
                }
                dog.Trainers.Attach(trainer);
                if(stateEntry.State != state)
                {
                    stateEntry.ChangeState(state);
                }
            }
            dog.Trainers.Remove(trainer);
        }
        [Obsolete("This method is only intended for use by the RIA M2M solution")]
        public void InsertDogDog(DogDog dogDog)
        {
            Dog dogAsPuppy = dogDog.DogAsPuppy;
            if(dogAsPuppy == null)
            {
			   dogAsPuppy = ChangeSet.ChangeSetEntries.Select(cse => cse.Entity)
			      .OfType<Dog>()
				  .SingleOrDefault(e => e.AnimalId == dogDog.DogAsPuppyAnimalId );
			}
            if(dogAsPuppy == null)
            {
                Dog dogAsPuppyStubEntity = new Dog { AnimalId = dogDog.DogAsPuppyAnimalId };
                dogAsPuppy = GetEntityByKey<Dog>(ObjectContext, "Animals", dogAsPuppyStubEntity);
            }
            Dog dogAsParent = dogDog.DogAsParent;
            if(dogAsParent == null)
            {
			   dogAsParent = ChangeSet.ChangeSetEntries.Select(cse => cse.Entity)
			      .OfType<Dog>()
				  .SingleOrDefault(e => e.AnimalId == dogDog.DogAsParentAnimalId );
			}
            if(dogAsParent == null)
            {
                Dog dogAsParentStubEntity = new Dog { AnimalId = dogDog.DogAsParentAnimalId };
                dogAsParent = GetEntityByKey<Dog>(ObjectContext, "Animals", dogAsParentStubEntity);
            }
            dogAsPuppy.Puppies.Add(dogAsParent);
        }
        [Obsolete("This method is only intended for use by the RIA M2M solution")]
        public void DeleteDogDog(DogDog dogDog)
        {
            Dog dogAsPuppy = dogDog.DogAsPuppy;
            if(dogAsPuppy == null)
            {
			   dogAsPuppy = ChangeSet.ChangeSetEntries.Select(cse => cse.Entity)
			      .OfType<Dog>()
				  .SingleOrDefault(e => e.AnimalId == dogDog.DogAsPuppyAnimalId );
			}
            if(dogAsPuppy == null)
            {
                Dog dogAsPuppyStubEntity = new Dog { AnimalId = dogDog.DogAsPuppyAnimalId };
                dogAsPuppy = GetEntityByKey<Dog>(ObjectContext, "Animals", dogAsPuppyStubEntity);
            }
            Dog dogAsParent = dogDog.DogAsParent;
            if(dogAsParent == null)
            {
			   dogAsParent = ChangeSet.ChangeSetEntries.Select(cse => cse.Entity)
			      .OfType<Dog>()
				  .SingleOrDefault(e => e.AnimalId == dogDog.DogAsParentAnimalId );
			}
            if(dogAsParent == null)
            {
                Dog dogAsParentStubEntity = new Dog { AnimalId = dogDog.DogAsParentAnimalId };
                dogAsParent = GetEntityByKey<Dog>(ObjectContext, "Animals", dogAsParentStubEntity);
            }
            if(dogAsPuppy.Puppies.IsLoaded == false)
            {
			    // We can't attach dogAsParent if dogAsPuppy is deleted. In that case we
				// temporarily reset the entity state of dogAsPuppy, then attach dogAsParent
				// and set the entity state of dogAsPuppy back to EntityState.Deleted.
                ObjectStateEntry stateEntry = ObjectContext.ObjectStateManager.GetObjectStateEntry(dogAsPuppy);
                EntityState state = stateEntry.State;

                if(state == EntityState.Deleted)
                {
                    stateEntry.ChangeState(EntityState.Unchanged);
                }
                dogAsPuppy.Puppies.Attach(dogAsParent);
                if(stateEntry.State != state)
                {
                    stateEntry.ChangeState(state);
                }
            }
            dogAsPuppy.Puppies.Remove(dogAsParent);
        }
        [Obsolete("This method is only intended for use by the RIA M2M solution")]
        public void InsertCatMarkedTerritories(CatMarkedTerritories catMarkedTerritories)
        {
            Cat cat = catMarkedTerritories.Cat;
            if(cat == null)
            {
			   cat = ChangeSet.ChangeSetEntries.Select(cse => cse.Entity)
			      .OfType<Cat>()
				  .SingleOrDefault(e => e.AnimalId == catMarkedTerritories.CatAnimalId );
			}
            if(cat == null)
            {
                Cat catStubEntity = new Cat { AnimalId = catMarkedTerritories.CatAnimalId };
                cat = GetEntityByKey<Cat>(ObjectContext, "Animals", catStubEntity);
            }
            MarkedTerritory markedTerritory = catMarkedTerritories.MarkedTerritory;
            if(markedTerritory == null)
            {
			   markedTerritory = ChangeSet.ChangeSetEntries.Select(cse => cse.Entity)
			      .OfType<MarkedTerritory>()
				  .SingleOrDefault(e => e.TerritoryId == catMarkedTerritories.MarkedTerritoryTerritoryId && e.CoordX == catMarkedTerritories.MarkedTerritoryCoordX && e.CoordY == catMarkedTerritories.MarkedTerritoryCoordY && e.CoordZ == catMarkedTerritories.MarkedTerritoryCoordZ );
			}
            if(markedTerritory == null)
            {
                MarkedTerritory markedTerritoryStubEntity = new MarkedTerritory { TerritoryId = catMarkedTerritories.MarkedTerritoryTerritoryId, CoordX = catMarkedTerritories.MarkedTerritoryCoordX, CoordY = catMarkedTerritories.MarkedTerritoryCoordY, CoordZ = catMarkedTerritories.MarkedTerritoryCoordZ };
                markedTerritory = GetEntityByKey<MarkedTerritory>(ObjectContext, "MarkedTerritories", markedTerritoryStubEntity);
            }
            cat.MarkedTerritories.Add(markedTerritory);
        }
        [Obsolete("This method is only intended for use by the RIA M2M solution")]
        public void DeleteCatMarkedTerritories(CatMarkedTerritories catMarkedTerritories)
        {
            Cat cat = catMarkedTerritories.Cat;
            if(cat == null)
            {
			   cat = ChangeSet.ChangeSetEntries.Select(cse => cse.Entity)
			      .OfType<Cat>()
				  .SingleOrDefault(e => e.AnimalId == catMarkedTerritories.CatAnimalId );
			}
            if(cat == null)
            {
                Cat catStubEntity = new Cat { AnimalId = catMarkedTerritories.CatAnimalId };
                cat = GetEntityByKey<Cat>(ObjectContext, "Animals", catStubEntity);
            }
            MarkedTerritory markedTerritory = catMarkedTerritories.MarkedTerritory;
            if(markedTerritory == null)
            {
			   markedTerritory = ChangeSet.ChangeSetEntries.Select(cse => cse.Entity)
			      .OfType<MarkedTerritory>()
				  .SingleOrDefault(e => e.TerritoryId == catMarkedTerritories.MarkedTerritoryTerritoryId && e.CoordX == catMarkedTerritories.MarkedTerritoryCoordX && e.CoordY == catMarkedTerritories.MarkedTerritoryCoordY && e.CoordZ == catMarkedTerritories.MarkedTerritoryCoordZ );
			}
            if(markedTerritory == null)
            {
                MarkedTerritory markedTerritoryStubEntity = new MarkedTerritory { TerritoryId = catMarkedTerritories.MarkedTerritoryTerritoryId, CoordX = catMarkedTerritories.MarkedTerritoryCoordX, CoordY = catMarkedTerritories.MarkedTerritoryCoordY, CoordZ = catMarkedTerritories.MarkedTerritoryCoordZ };
                markedTerritory = GetEntityByKey<MarkedTerritory>(ObjectContext, "MarkedTerritories", markedTerritoryStubEntity);
            }
            if(cat.MarkedTerritories.IsLoaded == false)
            {
			    // We can't attach markedTerritory if cat is deleted. In that case we
				// temporarily reset the entity state of cat, then attach markedTerritory
				// and set the entity state of cat back to EntityState.Deleted.
                ObjectStateEntry stateEntry = ObjectContext.ObjectStateManager.GetObjectStateEntry(cat);
                EntityState state = stateEntry.State;

                if(state == EntityState.Deleted)
                {
                    stateEntry.ChangeState(EntityState.Unchanged);
                }
                cat.MarkedTerritories.Attach(markedTerritory);
                if(stateEntry.State != state)
                {
                    stateEntry.ChangeState(state);
                }
            }
            cat.MarkedTerritories.Remove(markedTerritory);
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


