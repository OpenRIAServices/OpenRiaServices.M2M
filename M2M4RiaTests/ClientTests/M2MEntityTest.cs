using System;
using System.Net;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Silverlight.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClientTests.Web;

namespace RIAM2M.Client.Tests
{

    /// <summary>
    /// A collection of unit tests that test the Many to Many functionality on entities that have many to many relationships.
    /// </summary>
    [TestClass]
    public class M2MEntityTest
    {
        /// <summary>
        /// This method creates a many to many relationship between two related entities.  It then checks to see if each of the entites automatically appear in 
        /// each others many to many collection.
        /// </summary>
        [TestMethod]
        public void M2MEntityCollection_AddEntityToAnotherRelatedEntitysM2MEntityCollection_EntitiesShouldAppearInEachOthersM2MEntityCollections()
        {
            Vet vet = new Vet();
            Dog dog = new Dog();

            vet.Animals.Add(dog);

            Assert.IsTrue(vet.Animals.Contains(dog), "Could not find dog in vet collection");
            Assert.IsTrue(dog.Vets.Contains(vet), "Could not find vet in dog collection");
        }

        /// <summary>
        /// This method creates a many to many relationship between two related entities and then removes it.  It then checks to see if each of the entities are automatically
        /// removed from each others many to many collection.
        /// </summary>
        [TestMethod]
        public void M2MEntityCollection_RemoveEntityFromAnotherRelatedEntitysM2MEntityCollection_EntitiesShouldBeRemovedFromEachOthersM2MEntityCollections()
        {
            Vet vet = new Vet();
            Dog dog = new Dog();

            dog.Vets.Add(vet);
            vet.Animals.Remove(dog);

            Assert.IsFalse(vet.Animals.Contains(dog), "Found dog in vet collection when it shouldn't have been there.");
            Assert.IsFalse(dog.Vets.Contains(vet), "Found vet in dog collection when it shouldn't have been there.");
        }

        /// <summary>
        /// This method creates a many to many relationship between two related entities.  It then checks to see if an accompanying "Link Entity" has been
        /// created and referenced in each of the entities Link Entity collections.
        /// </summary>
        [TestMethod] 
        public void LinkEntityCollection_AddEntityToAnotherRelatedEntitysM2MEntityCollection_LinkEntityShouldAppearInEacOthersLinkEntityCollections()
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
        /// This method creates a many to many relationship between two related entities and then removes it.  It then checks to see if the accompanying "Link Entity",
        /// that was created when the relationship was created, is removed from each entities Link Entity collection.
        /// </summary>
        [TestMethod]
        public void LinkEntityCollection_RmoveEntityFromAnotherRelatedEntitysM2MEntityCollection_LinkEntityShouldBeRemovedFromEachOthersLinkEntityCollections()
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

#pragma warning disable 618

        /// <summary>
        /// Checks to see if the Link Entity remove event is fired when calling the remove event to remove a many to many relationship on an entity.
        /// </summary>
        [TestMethod]
        public void M2MEntityCollection_CheckThatTheLinkEntityRemoveEventFiresWhenManyToManyRelationshipIsRemoved_LinkEntytRemoveEventShouldFire()
        {
            bool DogToVetLinkRemoved = false;

            Vet vet = new Vet();
            Dog dog = new Dog();

            vet.Animals.Add(dog);
            dog.AnimalVetToVetRemoved += (e) =>
                {
                    DogToVetLinkRemoved = true;
                };

            dog.Vets.Remove(vet);

            Assert.IsTrue(DogToVetLinkRemoved, "Link Entity remove event did not fire when calling the remove method on a M2MEntityCollection");
        }
    }
}

#pragma warning restore 618