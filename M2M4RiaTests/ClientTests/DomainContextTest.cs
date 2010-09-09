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
        public DemoContext Context { get; set; }


        [TestInitialize]
        public void TestInitialize()
        {
            Context = new DemoContext();
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
            "This test creates a many to many relationship between two entities, and submits it to the domain context.\n"+
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
                    if (submitOperation.HasError)
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
        [Description("This test creates a many to many relationship between two entites, submits it to the domain context, and then attempts\n"+
        "to navigate from one side of the many to many relationship to the other via navigation properties.  This test is successful\n"+
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
        [Description("This test creates a many to many relationship between two entities, submits it to the domain context, removes the relationship,\n"+
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
                    if (submitOperation.HasError)
                        throw new AssertFailedException("Unable to remove many to many relationship between entities", submitOperation.Error);
                }
            );
            EnqueueTestComplete();
        }
    }
}
