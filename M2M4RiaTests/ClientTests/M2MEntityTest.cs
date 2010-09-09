using System.Linq;
using ClientTests.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace M2M4Ria.Client.Tests
{
    [TestClass]
    public class M2MEntityTest
    {
        /// <summary>
        /// This method creates a many to many relationship between two related entities.  
        /// It then checks to see if each of the entites automatically appear in each others many to many collection.
        /// </summary>
        [TestMethod]
        [Description("This method creates a many to many relationship between two related entities.\n" +
                     "It then checks to see if each of the entites automatically appear in each others many to many collection")]
        public void AddEntityToAnotherRelatedEntitysM2MEntityCollection1()
        {
            Vet vet = new Vet();
            Dog dog = new Dog();

            vet.Animals.Add(dog);

            Assert.IsTrue(vet.Animals.Contains(dog), "Could not find dog in vet collection");
            Assert.IsTrue(dog.Vets.Contains(vet), "Could not find vet in dog collection");
        }

        /// <summary>
        /// This method creates a many to many relationship between two related entities and then removes it.  
        /// It then checks to see if each of the entities are automatically removed from each others many to many collection.
        /// </summary>
        [TestMethod]
        [Description("This method creates a many to many relationship between two related entities and then removes it.\n" +
                     "It then checks to see if each of the entities are automatically removed from each others many to many collection")]
        public void RemoveEntityFromAnotherRelatedEntitysM2MEntityCollection1()
        {
            Vet vet = new Vet();
            Dog dog = new Dog();

            dog.Vets.Add(vet);
            vet.Animals.Remove(dog);

            Assert.IsFalse(vet.Animals.Contains(dog), "Found dog in vet collection when it shouldn't have been there.");
            Assert.IsFalse(dog.Vets.Contains(vet), "Found vet in dog collection when it shouldn't have been there.");
        }

        /// <summary>
        /// This method creates a many to many relationship between two related entities.  
        /// It then checks to see if an accompanying "Link Entity" has been created and referenced 
        /// in each of the entities Link Entity collections.
        /// </summary>
        [TestMethod]
        [Description("This method creates a many to many relationship between two related entities.\n"+
                     "It then checks to see if an accompanying \"Link Entity\" has been created and referenced \n" +
                     "in each of the entities Link Entity collections.")]
        public void AddEntityToAnotherRelatedEntitysM2MEntityCollection2()
        {
            Vet vet = new Vet();
            Dog dog = new Dog();

            vet.Animals.Add(dog);

            AnimalVet linkEntityFromVet = (from v in vet.AnimalVetToAnimal
                                           where v.Animal == dog && v.Vet == vet
                                           select v).FirstOrDefault();

            AnimalVet linkEntityFromDog = (from d in dog.AnimalVetToVet
                                           where d.Animal == dog && d.Vet == vet
                                           select d).FirstOrDefault();

            Assert.IsNotNull(linkEntityFromVet, "Unable to retrieve many to many link entity in relationship from vet to dog");
            Assert.IsNotNull(linkEntityFromDog, "Unable to retrieve many to many link entity in relationship from dog to vet");
        }

        /// <summary>
        /// This method creates a many to many relationship between two related entities and then removes it.  
        /// It then checks to see if the accompanying "Link Entity", that was created when the relationship was 
        /// created, is removed from each entities Link Entity collection.
        /// </summary>
        [TestMethod]
        [Description("This method creates a many to many relationship between two related entities and then removes it.\n"+
                     "It then checks to see if the accompanying \"Link Entity\", that was created when the relationship was" +
                     "created, is removed from each entities Link Entity collection.")]
        public void RemoveEntityFromAnotherRelatedEntitysM2MEntityCollection2()
        {
            Vet vet = new Vet();
            Dog dog = new Dog();

            vet.Animals.Add(dog);

            dog.Vets.Remove(vet);

            AnimalVet linkEntityFromVet = (from v in vet.AnimalVetToAnimal
                                           where v.Animal == dog && v.Vet == vet
                                           select v).FirstOrDefault();

            AnimalVet linkEntityFromDog = (from d in dog.AnimalVetToVet
                                           where d.Animal == dog && d.Vet == vet
                                           select d).FirstOrDefault();

            Assert.IsNull(linkEntityFromVet, "Link entity found from vet to dog when no link entity was expected");
            Assert.IsNull(linkEntityFromDog, "Link entity found from dog to vet when no link entity was expected");
        }
    }
}