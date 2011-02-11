


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
	
    /// <summary>
    /// This class defines Create and Delete operations for the following many-2-many relation(s):
    ///
    ///   Animal <--> Vet
    ///   Dog <--> FireHydrant
    ///   Dog <--> Trainer
    ///   Cat <--> Animal
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
    public partial class M2M4RiaTestService
    {
        [Obsolete("This method is only intended for use by the RIA M2M solution")]
        public void UpdateAnimalVet(AnimalVet animalVet)
        {
            throw new NotSupportedException("Update operation on AnimalVet is not supported.");
        }
        [Obsolete("This method is only intended for use by the RIA M2M solution")]
        public void InsertAnimalVet(AnimalVet animalVet)
        {
            Animal animal = animalVet.Animal;
            if(animal == null)
            {
                Animal animalStubEntity = new Animal() { AnimalId = animalVet.AnimalId };
                animal = GetEntityByKey<Animal>(ObjectContext, "Animals", animalStubEntity);
            }
            Vet vet = animalVet.Vet;
            if(vet == null)
            {
                Vet vetStubEntity = new Vet() { VetId = animalVet.VetId };
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
                Animal animalStubEntity = new Animal() { AnimalId = animalVet.AnimalId };
                animal = GetEntityByKey<Animal>(ObjectContext, "Animals", animalStubEntity);
            }
            Vet vet = animalVet.Vet;
            if(vet == null)
            {
                Vet vetStubEntity = new Vet() { VetId = animalVet.VetId };
                vet = GetEntityByKey<Vet>(ObjectContext, "Vets", vetStubEntity);
            }
            animal.Vets.Remove(vet);
        }
        [Obsolete("This method is only intended for use by the RIA M2M solution")]
        public void UpdateDogFireHydrant(DogFireHydrant dogFireHydrant)
        {
            throw new NotSupportedException("Update operation on DogFireHydrant is not supported.");
        }
        [Obsolete("This method is only intended for use by the RIA M2M solution")]
        public void InsertDogFireHydrant(DogFireHydrant dogFireHydrant)
        {
            Dog dog = dogFireHydrant.Dog;
            if(dog == null)
            {
                Dog dogStubEntity = new Dog() { AnimalId = dogFireHydrant.DogId };
                dog = GetEntityByKey<Dog>(ObjectContext, "Animals", dogStubEntity);
            }
            FireHydrant fireHydrant = dogFireHydrant.FireHydrant;
            if(fireHydrant == null)
            {
                FireHydrant fireHydrantStubEntity = new FireHydrant() { FireHydrantId = dogFireHydrant.FireHydrantId };
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
                Dog dogStubEntity = new Dog() { AnimalId = dogFireHydrant.DogId };
                dog = GetEntityByKey<Dog>(ObjectContext, "Animals", dogStubEntity);
            }
            FireHydrant fireHydrant = dogFireHydrant.FireHydrant;
            if(fireHydrant == null)
            {
                FireHydrant fireHydrantStubEntity = new FireHydrant() { FireHydrantId = dogFireHydrant.FireHydrantId };
                fireHydrant = GetEntityByKey<FireHydrant>(ObjectContext, "FireHydrants", fireHydrantStubEntity);
            }
            dog.FireHydrants.Remove(fireHydrant);
        }
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
                Dog dogStubEntity = new Dog() { AnimalId = dogTrainer.DogId };
                dog = GetEntityByKey<Dog>(ObjectContext, "Animals", dogStubEntity);
            }
            Trainer trainer = dogTrainer.Trainer;
            if(trainer == null)
            {
                Trainer trainerStubEntity = new Trainer() { TrainerId = dogTrainer.TrainerId };
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
                Dog dogStubEntity = new Dog() { AnimalId = dogTrainer.DogId };
                dog = GetEntityByKey<Dog>(ObjectContext, "Animals", dogStubEntity);
            }
            Trainer trainer = dogTrainer.Trainer;
            if(trainer == null)
            {
                Trainer trainerStubEntity = new Trainer() { TrainerId = dogTrainer.TrainerId };
                trainer = GetEntityByKey<Trainer>(ObjectContext, "Trainers", trainerStubEntity);
            }
            dog.Trainers.Remove(trainer);
        }
        [Obsolete("This method is only intended for use by the RIA M2M solution")]
        public void UpdateCatAnimal(CatAnimal catAnimal)
        {
            throw new NotSupportedException("Update operation on CatAnimal is not supported.");
        }
        [Obsolete("This method is only intended for use by the RIA M2M solution")]
        public void InsertCatAnimal(CatAnimal catAnimal)
        {
            Cat cat = catAnimal.Cat;
            if(cat == null)
            {
                Cat catStubEntity = new Cat() { AnimalId = catAnimal.CatId };
                cat = GetEntityByKey<Cat>(ObjectContext, "Animals", catStubEntity);
            }
            Animal animal = catAnimal.Animal;
            if(animal == null)
            {
                Animal animalStubEntity = new Animal() { AnimalId = catAnimal.AnimalId };
                animal = GetEntityByKey<Animal>(ObjectContext, "Animals", animalStubEntity);
            }
            cat.Animals.Add(animal);
        }
        [Obsolete("This method is only intended for use by the RIA M2M solution")]
        public void DeleteCatAnimal(CatAnimal catAnimal)
        {
            Cat cat = catAnimal.Cat;
            if(cat == null)
            {
                Cat catStubEntity = new Cat() { AnimalId = catAnimal.CatId };
                cat = GetEntityByKey<Cat>(ObjectContext, "Animals", catStubEntity);
            }
            Animal animal = catAnimal.Animal;
            if(animal == null)
            {
                Animal animalStubEntity = new Animal() { AnimalId = catAnimal.AnimalId };
                animal = GetEntityByKey<Animal>(ObjectContext, "Animals", animalStubEntity);
            }
            cat.Animals.Remove(animal);
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


