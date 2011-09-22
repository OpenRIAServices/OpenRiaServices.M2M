using System.Linq;
using System.ServiceModel.DomainServices.Client;
using ClientTests.Web;
using Microsoft.Silverlight.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using RiaServicesContrib;

namespace M2M4Ria.Client.Tests
{
    [TestClass]
    public class MultiaryDomainContextTest : SilverlightTest
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
            "This test is successful when there are no errors during the domain context submit." )]
        public void M2MCreate()
        {
            Cat cat = new Cat { Name = "Garfield" };
            MarkedTerritory territory = new MarkedTerritory
            {
                TerritoryId = Guid.NewGuid(),
                CoordX = 25,
                CoordY = 37,
                CoordZ = 2
            };

            cat.MarkedTerritories.Add( territory );
            Context.MarkedTerritories.Add( territory );

            SubmitOperation submitOperation = Context.SubmitChanges();

            EnqueueConditional( () => submitOperation.IsComplete );
            EnqueueCallback
            (
                () =>
                {
                    if( submitOperation.HasError )
                        throw new AssertFailedException( "Unable to create many to many relationship between entities", submitOperation.Error );
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
        [Description( "This test creates a many to many relationship between two entites, submits it to the domain context, and then attempts\n" +
        "to navigate from one side of the many to many relationship to the other via navigation properties.  This test is successful\n" +
        "when a entity is retrieved upon navigating the many to many relationship." )]
        public void RetrieveEntityAndNavigate()
        {
            Cat cat = new Cat { Name = "Garfield" };
            MarkedTerritory territory = new MarkedTerritory
            {
                TerritoryId = Guid.NewGuid(),
                CoordX = 25,
                CoordY = 37,
                CoordZ = 2
            };
            Animal animal = null;

            cat.MarkedTerritories.Add( territory );
            Context.MarkedTerritories.Add( territory );

            SubmitOperation submitOperation = Context.SubmitChanges();

            EnqueueConditional( () => submitOperation.IsComplete );
            EnqueueCallback
            (
                () =>
                {
                    territory = (from mt in Context.MarkedTerritories
                                 where mt.TerritoryId == territory.TerritoryId
                                      && mt.CoordX == territory.CoordX
                                      && mt.CoordY == territory.CoordY
                                      && mt.CoordZ == territory.CoordZ
                                 select mt).First();

                    Assert.IsNotNull( territory, "Unable to retrieve the first entity from the many to many relationship" );

                    animal = territory.Cats.First();

                    Assert.IsNotNull( animal, "Unable to retrieve the second entity in the many to many relationship after navigating from the first entity" );
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
        [Description( "This test creates a many to many relationship between two entities, submits it to the domain context, removes the relationship,\n" +
                     "and then submits to the domain context again.  This test is successful when there are no errors during the domain context submit." )]
        public void RetrieveEntityAndRemove()
        {
            Cat cat = new Cat { Name = "Garfield" };
            MarkedTerritory territory = new MarkedTerritory
            {
                TerritoryId = Guid.NewGuid(),
                CoordX = 25,
                CoordY = 37,
                CoordZ = 2
            };

            cat.MarkedTerritories.Add( territory );
            Context.MarkedTerritories.Add( territory );

            SubmitOperation submitOperation = Context.SubmitChanges();

            EnqueueConditional( () => submitOperation.IsComplete );
            EnqueueCallback
            (
                () =>
                {
                    territory = (from mt in Context.MarkedTerritories
                                 where mt.TerritoryId == territory.TerritoryId
                                      && mt.CoordX == territory.CoordX
                                      && mt.CoordY == territory.CoordY
                                      && mt.CoordZ == territory.CoordZ
                                 select mt).First();

                    cat = territory.Cats.First();

                    territory.Cats.Remove( cat );

                    submitOperation = Context.SubmitChanges();

                    EnqueueConditional( () => submitOperation.IsComplete );
                    EnqueueCallback
                    (
                        () =>
                        {
                            if( submitOperation.HasError )
                                throw new AssertFailedException( "Unable to remove many to many relationship between entities", submitOperation.Error );
                        }
                    );
                    EnqueueTestComplete();
                }
            );
        }

        /// <summary>
        /// This test creates two many to many relations between two entities and submits it to the domain context.
        /// This test is successful when there are no errors during the domain context submit.
        /// </summary>
        [TestMethod]
        [Asynchronous]
        [Description( "This test creates two many to many relations between two entities and submits it to the domain context.\n" +
                     "This test is successful when there are no errors during the domain context submit." )]
        public void CreateTwoM2MRelationsAtOnce()
        {
            Cat cat = new Cat { Name = "Garfield" };
            MarkedTerritory territory1 = new MarkedTerritory
            {
                TerritoryId = Guid.NewGuid(),
                CoordX = 25,
                CoordY = 37,
                CoordZ = 2
            };
            MarkedTerritory territory2 = new MarkedTerritory
            {
                TerritoryId = Guid.NewGuid(),
                CoordX = 42,
                CoordY = 73,
                CoordZ = 1
            };

            cat.MarkedTerritories.Add( territory1 );
            cat.MarkedTerritories.Add( territory2 );
            Context.Animals.Add( cat );

            SubmitOperation submitOperation = Context.SubmitChanges();

            EnqueueConditional( () => submitOperation.IsComplete );
            EnqueueCallback
            (
                () =>
                {
                    if( submitOperation.HasError )
                        throw new AssertFailedException( "Unable to create two m2m relations at once", submitOperation.Error );
                }
            );
            EnqueueTestComplete();
        }

#pragma warning disable 618

        /// <summary>
        /// Checks to see if the entityset property is set when adding an entity to a context.
        /// </summary>
        [TestMethod]
        [Description( "Checks to see if the entityset property is set when adding an entity to a context\n" )]
        public void CheckLinkEntitySetIsSet()
        {
            CatMarkedTerritories catTerritories = new CatMarkedTerritories
            {
                Cat = new Cat(),
                MarkedTerritory = new MarkedTerritory
                {
                    TerritoryId = Guid.NewGuid(),
                    CoordX = 42,
                    CoordY = 73,
                    CoordZ = 1
                }
            };

            var entityset = Context.EntityContainer.GetEntitySet<CatMarkedTerritories>();
            entityset.Add( catTerritories );

            Assert.IsNotNull( ((IExtendedEntity)catTerritories).EntitySet, "EntitySet property for link entity is not set when adding the owning entity to a context" );
        }

        /// <summary>
        /// Checks to see if the entityset property is set to null when removing an entity from a context.
        /// </summary>
        [TestMethod]
        [Description( "Checks to see if the entityset property is set to null when removing an entity from a context.\n" )]
        public void CheckLinkEntitySetIsNull()
        {
            CatMarkedTerritories catTerritories = new CatMarkedTerritories
            {
                Cat = new Cat(),
                MarkedTerritory = new MarkedTerritory
                {
                    TerritoryId = Guid.NewGuid(),
                    CoordX = 42,
                    CoordY = 73,
                    CoordZ = 1
                }
            };

            var entityset = Context.EntityContainer.GetEntitySet<CatMarkedTerritories>();
            entityset.Add( catTerritories );
            entityset.Remove( catTerritories );

            Assert.IsNull( ((IExtendedEntity)catTerritories).EntitySet, "EntitySet property for link entity is not set to null when removing the owning entity to a context" );
        }

        [TestMethod]
        [Asynchronous]
        [Description( "Test that checks if entitycollection is valid after updating the keys of two entities of an m2m relation" )]
        public void EntityKeyUpdateTest()
        {
            // Create one entity
            Cat cat = new Cat { Name = "cat" };
            Context.Animals.Add( cat );

            // Create another
            MarkedTerritory territory = new MarkedTerritory
            {
                TerritoryId = Guid.NewGuid(),
                CoordX = 42,
                CoordY = 73,
                CoordZ = 1
            };
            Context.MarkedTerritories.Add( territory );

            // Join them using a join table entity
            CatMarkedTerritories catTerritories = new CatMarkedTerritories
            {
                Cat = cat,
                MarkedTerritory = territory
            };
            cat.CatMarkedTerritoriesToMarkedTerritorySet.Add( catTerritories );

            cat.PropertyChanged += entity_PropertyChanged;
            territory.PropertyChanged += entity_PropertyChanged;

            // Verify that both entity collections now contain a single element
            Assert.AreEqual( 1, territory.CatMarkedTerritoriesToCatSet.Count() );
            Assert.AreEqual( 1, cat.CatMarkedTerritoriesToMarkedTerritorySet.Count() );

            // Use submitchanges to send new eneities to server and to retrieve real key values back
            SubmitOperation submitOperation = Context.SubmitChanges();
            EnqueueConditional( () => submitOperation.IsComplete );

            EnqueueCallback
            (
                () =>
                {
                    // Elements in entity collections should be the same as before.
                    Assert.AreEqual( 1, territory.CatMarkedTerritoriesToCatSet.Count() );
                    Assert.AreEqual( 1, cat.CatMarkedTerritoriesToMarkedTerritorySet.Count() );
                }
            );
            EnqueueTestComplete();
        }

        void entity_PropertyChanged( object sender, System.ComponentModel.PropertyChangedEventArgs e )
        {
            if( sender is Cat && e.PropertyName == "AnimalId" )
            {
                ((Cat)sender).CatMarkedTerritoriesToMarkedTerritorySet.ToList();
            }
            else if( sender is MarkedTerritory && e.PropertyName == "TerritoryId" )
            {
                ((MarkedTerritory)sender).CatMarkedTerritoriesToCatSet.ToList();
            }
        }

        [TestMethod]
        [Asynchronous]
        [Description( "Test adding an m2m relation to an entity with changes" )]
        public void AddM2MtoChangedEntityTest()
        {
            // Create one entity
            Cat cat = new Cat { Name = "cat" };
            Context.Animals.Add( cat );

            // Create another
            MarkedTerritory territory = new MarkedTerritory
            {
                TerritoryId = Guid.NewGuid(),
                CoordX = 42,
                CoordY = 73,
                CoordZ = 1
            };
            Context.MarkedTerritories.Add( territory );

            SubmitOperation submitOperation = Context.SubmitChanges();
            EnqueueConditional( () => submitOperation.IsComplete );
            EnqueueCallback
            (
                () =>
                {
                    cat.Name = "Cat2";
                    cat.MarkedTerritories.Add( territory );
                    var submitOperation2 = Context.SubmitChanges();
                    EnqueueConditional( () => submitOperation2.IsComplete );
                    EnqueueCallback
                    (
                        () =>
                        {
                            Assert.IsFalse( submitOperation2.HasError );
                        }
                    );
                    EnqueueTestComplete();
                }
            );
        }

        [Asynchronous]
        [TestMethod]
        [Description( "Create an entity, then an m2m relation, then delete both" )]
        public void CreateThenDeleteTest()
        {
            // Create one entity
            Cat cat = new Cat { Name = "cat" };
            Context.Animals.Add( cat );

            // Create another
            MarkedTerritory territory = new MarkedTerritory
            {
                TerritoryId = Guid.NewGuid(),
                CoordX = 42,
                CoordY = 73,
                CoordZ = 1
            };
            Context.MarkedTerritories.Add( territory );

            SubmitOperation submitOperation = Context.SubmitChanges();
            EnqueueConditional( () => submitOperation.IsComplete );

            EnqueueCallback
            (
                () =>
                {
                    cat.MarkedTerritories.Add( territory );
                    var submitOperation2 = Context.SubmitChanges();
                    EnqueueConditional( () => submitOperation2.IsComplete );
                    EnqueueCallback
                    (
                        () =>
                        {
                            cat.MarkedTerritories.Remove( territory );
                            Context.Animals.Remove( cat );
                            var submitOperation3 = Context.SubmitChanges();
                            EnqueueConditional( () => submitOperation3.IsComplete );
                            EnqueueCallback
                            (
                                () =>
                                {
                                    Assert.IsFalse( submitOperation3.HasError );
                                    EnqueueTestComplete();
                                }
                            );
                        }
                    );
                }
            );
        }

        [Asynchronous]
        [TestMethod]
        public void DeleteParentTest()
        {
            // step1: create cat, marked territory and catmarkterr objects
            var cat = new Cat { Name = "myCat" + DateTime.Now };
            var territory = new MarkedTerritory
            {
                TerritoryId = Guid.NewGuid(),
                CoordX = 42,
                CoordY = 73,
                CoordZ = 1
            };
            cat.MarkedTerritories.Add( territory );
            Context.Animals.Add( cat );

            SubmitOperation submitOperation1 = Context.SubmitChanges();

            EnqueueConditional( () => submitOperation1.IsComplete );
            EnqueueCallback
            (
                () =>
                {
                    Assert.IsFalse( submitOperation1.HasError );
                    // step2: remove catmarkterr and cat objects
                    cat.MarkedTerritories.Remove( territory );
                    Context.Animals.Remove( cat );
                    SubmitOperation submitOperation2 = Context.SubmitChanges();

                    EnqueueConditional( () => submitOperation2.IsComplete );
                    EnqueueCallback
                    (
                        () =>
                        {
                            Assert.IsFalse( submitOperation2.HasError );
                            // step3 load all animals to verify that cat no longer ecists in the database
                            LoadOperation loadOperation1 = Context.Load( Context.GetAnimalsQuery() );

                            EnqueueConditional( () => loadOperation1.IsComplete );
                            EnqueueCallback
                            (
                                () =>
                                {
                                    Assert.IsFalse( loadOperation1.HasError );
                                    Assert.IsFalse( Context.Animals.Contains( cat ) );

                                    EnqueueTestComplete();
                                }
                            );

                        }
                    );
                }
            );
        }
    }
}
