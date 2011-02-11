using System.Linq;
using System.ServiceModel.DomainServices.Client;
using ClientTests.Web;
using Microsoft.Silverlight.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace M2M4Ria.Client.Tests
{
    [TestClass]
    public class DomainContextTest : SilverlightTest
    {
        public M2M4RiaTestContext Context { get; set; }


        [TestInitialize]
        public void TestInitialize()
        {
            Context = new M2M4RiaTestContext();
            Context.CreateDataBase();
        }

        /// <summary>
        /// This test creates a many to many relationship between two entities, and submits it to the domain context.  
        /// This test is successful when there are no errors during the domain context submit.
        /// </summary>
        /// <desription>
        /// Hi there
        /// </desription>  
        /// 
        [TestMethod]
        [Asynchronous]
        [Description(
            "This test creates a many to many relationship between two entities, and submits it to the domain context.\n" +
            "This test is successful when there are no errors during the domain context submit.")]
        public void M2MCreate()
        {
            Vet vet = new Vet() { Name = "Bob" };
            Dog dog = new Dog() { Name = "Poochy" };

            dog.Vets.Add(vet);
            Context.Vets.Add(vet);

            SubmitOperation submitOperation = Context.SubmitChanges();

            EnqueueConditional(() => submitOperation.IsComplete);
            EnqueueCallback
            (
                () =>
                {
                    if(submitOperation.HasError)
                        throw new AssertFailedException("Unable to create many to many relationship between entities", submitOperation.Error);
                }
            );
            EnqueueTestComplete();
        }

        /// <summary>
        /// This test creates a many to many relationship between two entites, submits it to the domain context, and then attempts
        /// to navigate from one side of the many to many relationship to the other via navigation properties.  This test is successful
        /// when a entity is retrieved upon navigating the many to many relationship.
        /// </summary>
        [TestMethod]
        [Asynchronous]
        [Description("This test creates a many to many relationship between two entites, submits it to the domain context, and then attempts\n" +
        "to navigate from one side of the many to many relationship to the other via navigation properties.  This test is successful\n" +
        "when a entity is retrieved upon navigating the many to many relationship.")]
        public void RetrieveEntityAndNavigate()
        {
            Vet vet = new Vet() { Name = "Bob" };
            Dog dog = new Dog() { Name = "Poochy" };
            Animal animal = null;

            dog.Vets.Add(vet);
            Context.Vets.Add(vet);

            SubmitOperation submitOperation = Context.SubmitChanges();

            EnqueueConditional(() => submitOperation.IsComplete);
            EnqueueCallback
            (
                () =>
                {
                    vet = (from v in Context.Vets
                           where v.VetId == vet.VetId
                           select v).First();

                    Assert.IsNotNull(vet, "Unable to retrieve the first entity from the many to many relationship");

                    animal = vet.Animals.First();

                    Assert.IsNotNull(animal, "Unable to retrieve the second entity in the many to many relationship after navigating from the first entity");
                }
            );
            EnqueueTestComplete();
        }

        /// <summary>
        /// This test creates a many to many relationship between two entities, submits it to the domain context, removes the relationship, and then submits
        /// to the domain context again.  This test is successful when there are no errors during the domain context submit.
        /// </summary>
        [TestMethod]
        [Asynchronous]
        [Description("This test creates a many to many relationship between two entities, submits it to the domain context, removes the relationship,\n" +
                     "and then submits to the domain context again.  This test is successful when there are no errors during the domain context submit.")]
        public void RetrieveEntityAndRemove()
        {
            Vet vet = new Vet() { Name = "Bob" };
            Dog dog = new Dog() { Name = "Poochy" };
            Animal animal = null;

            dog.Vets.Add(vet);
            Context.Vets.Add(vet);

            SubmitOperation submitOperation = Context.SubmitChanges();

            EnqueueConditional(() => submitOperation.IsComplete);
            EnqueueCallback
            (
                () =>
                {
                    vet = (from v in Context.Vets
                           where v.VetId == vet.VetId
                           select v).First();

                    animal = vet.Animals.First();

                    vet.Animals.Remove(animal);

                    submitOperation = Context.SubmitChanges();
                }
            );
            EnqueueConditional(() => submitOperation.IsComplete);
            EnqueueCallback
            (
                () =>
                {
                    if(submitOperation.HasError)
                        throw new AssertFailedException("Unable to remove many to many relationship between entities", submitOperation.Error);
                }
            );
            EnqueueTestComplete();
        }
        /// <summary>
        /// This test creates two many to many relations between two entities and submits it to the domain context.
        /// This test is successful when there are no errors during the domain context submit.
        /// </summary>
        [TestMethod]
        [Asynchronous]
        [Description("This test creates two many to many relations between two entities and submits it to the domain context.\n" +
                     "This test is successful when there are no errors during the domain context submit.")]
        public void CreateTwoM2MRelationsAtOnce()
        {
            Vet vet1 = new Vet() { Name = "Bob" };
            Vet vet2 = new Vet() { Name = "John" };
            Dog dog = new Dog() { Name = "Poochy" };

            dog.Vets.Add(vet1);
            dog.Vets.Add(vet2);
            Context.Animals.Add(dog);

            SubmitOperation submitOperation = Context.SubmitChanges();

            EnqueueConditional(() => submitOperation.IsComplete);
            EnqueueCallback
            (
                () =>
                {
                    if(submitOperation.HasError)
                        throw new AssertFailedException("Unable to create two m2m relations at once", submitOperation.Error);
                }
            );
            EnqueueTestComplete();
        }
#pragma warning disable 618

        /// <summary>
        /// Checks to see if the entityset property is set when adding an entity to a context.
        /// </summary>
        [TestMethod]
        [Description("Checks to see if the entityset property is set when adding an entity to a context\n")]
        public void CheckLinkEntitySetIsSet()
        {
            AnimalVet animalVet = new AnimalVet
            {
                Animal = new Dog(),
                Vet = new Vet()
            };

            var entityset = Context.EntityContainer.GetEntitySet<AnimalVet>();
            entityset.Add(animalVet);

            Assert.IsNotNull(animalVet.EntitySet, "EntitySet property for link entity is not set when adding the owning entity to a context");
        }
        /// <summary>
        /// Checks to see if the entityset property is set to null when removing an entity from a context.
        /// </summary>
        [TestMethod]
        [Description("Checks to see if the entityset property is set to null when removing an entity from a context.\n")]
        public void CheckLinkEntitySetIsNull()
        {
            AnimalVet animalVet = new AnimalVet
            {
                Animal = new Dog(),
                Vet = new Vet()
            };

            var entityset = Context.EntityContainer.GetEntitySet<AnimalVet>();
            entityset.Add(animalVet);
            entityset.Remove(animalVet);

            Assert.IsNull(animalVet.EntitySet, "EntitySet property for link entity is not set to null when removing the owning entity to a context");
        }

        [TestMethod]
        [Asynchronous]
        [Description("Test that checks if entitycollection is valid after updating the keys of two entities of an m2m relation")]
        public void EntityKeyUpdateTest()
        {
            // Create one entity
            Dog dog = new Dog { Name = "dog" };
            Context.Animals.Add(dog);

            // Create another
            Trainer trainer = new Trainer { Name = "trainer" };
            Context.Trainers.Add(trainer);

            // Join them using a join table entity
            DogTrainer dogTrainer = new DogTrainer
            {
                Dog = dog,
                Trainer = trainer
            };
            dog.DogTrainerToTrainerSet.Add(dogTrainer);

            dog.PropertyChanged += entity_PropertyChanged;
            trainer.PropertyChanged += entity_PropertyChanged;

            // Verify that both entity collections now contain a single element
            Assert.AreEqual(1, trainer.DogTrainerToDogSet.Count());
            Assert.AreEqual(1, dog.DogTrainerToTrainerSet.Count());

            // Use submitchanges to send new eneities to server and to retrieve real key values back
            SubmitOperation submitOperation = Context.SubmitChanges();
            EnqueueConditional(() => submitOperation.IsComplete);

            EnqueueCallback(
                () =>
                {
                    // Elements in entity collections should be the same as before.
                    Assert.AreEqual(1, trainer.DogTrainerToDogSet.Count());
                    Assert.AreEqual(1, dog.DogTrainerToTrainerSet.Count());
                });
            EnqueueTestComplete();
        }

        void entity_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(sender is Dog && e.PropertyName == "DogId")
            {
                ((Dog)sender).DogTrainerToTrainerSet.ToList();
            }
            else
                if(sender is Trainer && e.PropertyName == "TrainerId")
                {
                    ((Trainer)sender).DogTrainerToDogSet.ToList();
                }
        }

        [TestMethod]
        [Asynchronous]
        [Description("Test adding an m2m relation to an entity with changes")]
        public void AddM2MtoChangedEntityTest()
        {
            // Create one entity
            Dog dog = new Dog { Name = "dog" };
            Context.Animals.Add(dog);

            // Create another
            Trainer trainer = new Trainer { Name = "trainer" };
            Context.Trainers.Add(trainer);

            SubmitOperation submitOperation = Context.SubmitChanges();
            EnqueueConditional(() => submitOperation.IsComplete);

            EnqueueCallback(
                () =>
                {
                    dog.Name = "Dog2";
                    dog.Trainers.Add(trainer);
                    var submitOperation2 = Context.SubmitChanges();
                    EnqueueConditional(() => submitOperation2.IsComplete);
                    EnqueueCallback(
                        () => { 
                            Assert.IsFalse(submitOperation2.HasError); 
                        });
                });
            EnqueueTestComplete();
        }
        [Asynchronous]
        [TestMethod]
        [Description("Create an entity, then an m2m relation, then delete both")]
        public void CreateThenDeleteTest()
        {
            // Create one entity
            Dog dog = new Dog { Name = "dog" };
            Context.Animals.Add(dog);

            // Create another
            Trainer trainer = new Trainer { Name = "trainer" };
            Context.Trainers.Add(trainer);

            SubmitOperation submitOperation = Context.SubmitChanges();
            EnqueueConditional(() => submitOperation.IsComplete);

            EnqueueCallback(
                () =>
                {
                    dog.Trainers.Add(trainer);
                    var submitOperation2 = Context.SubmitChanges();
                    EnqueueConditional(() => submitOperation2.IsComplete);
                    EnqueueCallback(
                        () =>
                        {
                            dog.Trainers.Remove(trainer);
                            Context.Animals.Remove(dog);
                            var submitOperation3 = Context.SubmitChanges();
                            EnqueueConditional(() => submitOperation3.IsComplete);
                            EnqueueCallback(
                                () =>
                                {
                                    Assert.IsFalse(submitOperation3.HasError);
                                });
                        });
                });
            EnqueueTestComplete();
        }
    }
}
