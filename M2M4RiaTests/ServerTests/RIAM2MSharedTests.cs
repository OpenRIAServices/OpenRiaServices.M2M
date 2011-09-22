using System;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServerTests;
using System.Data.Metadata.Edm;

namespace RIAM2M.Web.Tests
{
    [TestClass]
    public class M2M4RiaSharedTests
    {
        // The unit test settings for this solution have been configured to copy the DemoMode.edmx file to the root directory
        // of the unit test's workspace.  See the "Deployment" menu under the Local.testsettings file in the "Solution Items" solution folder.
        private const string EdmxFilePath = "M2M4RiaTestModel.edmx";
        private readonly Type[] ExpectedAssocationTypes = new Type[] { };

        private M2M4RiaShared.M2MData _M2MDataObject;

        private M2M4RiaShared.M2MData M2MDataObject
        {
            get
            {
                if (_M2MDataObject == null)
                    _M2MDataObject = GenerateM2MData();
                
                return _M2MDataObject;
            }
        }

        /// <summary>
        /// Check to see if the GenerateM2MData method returns an instance of a M2MData object
        /// </summary>
        [TestMethod]
        public void GenerateM2MData_ExecuteMethod_ShouldReturnNotNullM2MDataObject()
        {
            Assert.IsNotNull(GenerateM2MData());
        }
        
        /// <summary>
        /// Check to see if the M2MData object contains all of the expected AssociationSets
        /// </summary>
        [TestMethod]
        public void GenerateM2MData_InspectReturnType_ShouldContainRequiredAssociationSets()
        {
            string[] requiredAssociationSets = { "AnimalVet", "DogFireHydrant", "DogTrainer", "AnimalFood", "CatMarkedTerritories", "DogDog" };

            Assert.IsTrue(M2MDataObject.Associations.All(e => requiredAssociationSets.Contains(e.Name)), "M2MData.Associations does not match the expected AsssociationSet list");
        }

        /// <summary>
        /// Check to see if the M2MData object contains all of the expected Entities
        /// </summary>
        [TestMethod]
        public void GenerateM2MData_InspectReturnType_ShouldContainRequiredEntities()
        {
            string[] requiredEntities = { "Animal", "Vet", "FireHydrant", "Trainer", "Food", "Dog", "Cat", "MarkedTerritory" };

            Assert.IsTrue(M2MDataObject.Entities.All(e => requiredEntities.Contains(e.Name)), "M2MData.Entities does not match the expected Entities list");
        }

        /// <summary>
        /// Check to see if the AnimalVet AssociationSet contains valid data
        /// </summary>
        [TestMethod]
        public void GenerateM2MData_InspectReturnType_CheckAssociationSetIsValidForAnimalVet()
        {
            M2M4RiaShared.M2MAssociationSet associationSet = GetAssociationSetByName("AnimalVet");

            CheckUnaryAssociationIsValid(associationSet.Entity1ToLink, "Entity1ToLink", "int", "Animal", "AnimalAnimalId", true, "AnimalVetToAnimalSet", "Vets", "AnimalId");
            CheckUnaryAssociationIsValid( associationSet.Entity2ToLink, "Entity2ToLink", "int", "Vet", "VetVetId", true, "AnimalVetToVetSet", "Animals", "VetId" );
        }
        [TestMethod]
        public void GenerateM2MData_InspectReturnType_CheckAssociationSetIsValidForCatMarkedTerritory()
        {
            M2M4RiaShared.M2MAssociationSet associationSet = GetAssociationSetByName( "CatMarkedTerritories" );
            CheckMultiaryAssociationIsValid( associationSet.Entity2ToLink, 4, "Entity2ToLink", new[] { "System.Guid", "int", "int", "int" }, "MarkedTerritory", new[] { "MarkedTerritoryTerritoryId", "MarkedTerritoryCoordX", "MarkedTerritoryCoordY", "MarkedTerritoryCoordZ" }, true, "CatMarkedTerritoriesToMarkedTerritorySet", "Cats", new[] { "TerritoryId", "CoordX", "CoordY", "CoordZ" } );
            CheckMultiaryAssociationIsValid( associationSet.Entity1ToLink, 1, "Entity1ToLink", new[] { "int" }, "Cat", new[] { "CatAnimalId" }, true, "CatMarkedTerritoriesToCatSet", "MarkedTerritories", new[] { "AnimalId" } );
        }

        /// <summary>
        /// Check to see if the DogFireHydrant AssociationSet contains valid data
        /// </summary>
        [TestMethod]
        public void GenerateM2MData_InspectReturnType_CheckAssociationSetIsValidForDogFireHydrant()
        {
            M2M4RiaShared.M2MAssociationSet associationSet = GetAssociationSetByName("DogFireHydrant");

            CheckUnaryAssociationIsValid(associationSet.Entity1ToLink, "Entity1ToLink", "int", "Dog", "DogAnimalId", true, "DogFireHydrantToDogSet", "FireHydrants", "AnimalId");
            CheckUnaryAssociationIsValid( associationSet.Entity2ToLink, "Entity2ToLink", "int", "FireHydrant", "FireHydrantFireHydrantId", false, "DogFireHydrantToFireHydrantSet", null, "FireHydrantId" );
        }

        /// <summary>
        /// Check to see if the DogTrainer AssociationSet contains valid data
        /// </summary>
        [TestMethod]
        public void GenerateM2MData_InspectReturnType_CheckAssociationSetIsValidForDogTrainer()
        {
            M2M4RiaShared.M2MAssociationSet associationSet = GetAssociationSetByName("DogTrainer");

            CheckUnaryAssociationIsValid(associationSet.Entity1ToLink, "Entity1ToLink", "int", "Dog", "DogAnimalId", true, "DogTrainerToDogSet", "Trainers", "AnimalId");
            CheckUnaryAssociationIsValid( associationSet.Entity2ToLink, "Entity2ToLink", "int", "Trainer", "TrainerTrainerId", true, "DogTrainerToTrainerSet", "Dogs", "TrainerId" );
        }
         

        /// <summary>
        /// Check to see if the AnimalFood AssociationSet contains valid data
        /// </summary>
        [TestMethod]
        public void GenerateM2MData_InspectReturnType_CheckAssociationSetIsValidForAnimalFood()
        {
            M2M4RiaShared.M2MAssociationSet associationSet = GetAssociationSetByName("AnimalFood");

            CheckUnaryAssociationIsValid(associationSet.Entity1ToLink, "Entity1ToLink", "int", "Animal", "AnimalAnimalId", false, "AnimalFoodToAnimalSet", null, "AnimalId");
            CheckUnaryAssociationIsValid(associationSet.Entity2ToLink, "Entity2ToLink", "int", "Food", "FoodFoodId", false, "AnimalFoodToFoodSet", null, "FoodId");
        }

        /// <summary>
        /// Check to see if the Animal entity contains valid data
        /// </summary>
        [TestMethod]
        public void GenerateM2MData_InspectReturnType_CheckEntityIsValidForAnimal()
        {
            CheckEntityIsValid("Animal", new string[] { "AnimalFood", "AnimalVet", "CatAnimal" }, new string[] { "Cat", "Dog" }, "Animals", false, true, null); 
        }

        /// <summary>
        /// Check to see if the Vet entity contains valid data
        /// </summary>
        [TestMethod]
        public void GenerateM2MData_InspectReturnType_CheckEntityIsValidForVet()
        {
            CheckEntityIsValid("Vet", new string[] { "AnimalVet" }, new string[] { }, "Vets", false, true, null); 
        }

        /// <summary>
        /// Check to see if the FireHydrant entity contains valid data
        /// </summary>
        [TestMethod]
        public void GenerateM2MData_InspectReturnType_CheckEntityIsValidForFireHydrant()
        {
            CheckEntityIsValid("FireHydrant", new string[] { "DogFireHydrant" }, new string[] { }, "FireHydrants", false, true, null); 
        }

        /// <summary>
        /// Check to see if the Trainer entity contains valid data
        /// </summary>
        [TestMethod]
        public void GenerateM2MData_InspectReturnType_CheckEntityIsValidForTrainer()
        {
            CheckEntityIsValid("Trainer", new string[] { "DogTrainer" }, new string[] { }, "Trainers", false, true, null); 
        }

        /// <summary>
        /// Check to see if the Food entity contains valid data
        /// </summary>
        [TestMethod]
        public void GenerateM2MData_InspectReturnType_CheckEntityIsValidForFood()
        {
            CheckEntityIsValid("Food", new string[] { "AnimalFood" }, new string[] { }, "Foods", false, true, null); 
        }

        /// <summary>
        /// Check to see if the Dog entity contains valid data
        /// </summary>
        [TestMethod]
        public void GenerateM2MData_InspectReturnType_CheckEntityIsValidForDog()
        {
            CheckEntityIsValid("Dog", new string[] { "DogFireHydrant", "DogTrainer", "DogDog" }, new string[] { }, "Animals", false, false, "Animal"); 
        }

        private void CheckEntityIsValid(string actualEntityName, string[] expectedAssociationSetNames, string[] expectedDerivedEntityNames, string expectedEntitySet, bool expectedIsAbstract, bool expectedIsBaseEntity, string expectedBaseEntityName)
        {
            M2M4RiaShared.M2MEntity entity = GetEntityByName(actualEntityName);

            if (!(expectedAssociationSetNames.Length == 0 && entity.Associations.Count == 0))
                Assert.IsTrue(entity.Associations.All(e => expectedAssociationSetNames.Contains(e.Name)), "Entity.Associations does not match the expected AssociationSet list");

            if (!(expectedDerivedEntityNames.Length == 0 && entity.DerivedEntityNames.Count == 0))
                Assert.IsTrue(entity.DerivedEntityNames.All(e => expectedDerivedEntityNames.Contains(e)), "Entity.DerivedEntityNames does not match the expected derived entity name list");

            Assert.AreEqual(expectedEntitySet, entity.EntitySet, "EntitySet is not equal");
            Assert.AreEqual(expectedIsAbstract, entity.IsAbstract, "IsAbstract is not equal");
            Assert.AreEqual(expectedIsBaseEntity, entity.IsBaseEntity, "IsBaseEntity is not equal");
            Assert.AreEqual(expectedBaseEntityName, entity.BaseEntityName, "BaseEntityName is not equal");
        }

        private void CheckUnaryAssociationIsValid(M2M4RiaShared.M2MAssociation actualAssociation, string associationEndName, string expectedDataType, string expectedEntityName, string expectedFK, bool expectedHasM2MNavProp, string expectedLinkTableNavProp, string expectedM2MNavProp, string expectedPK)
        {
            CheckMultiaryAssociationIsValid( actualAssociation, 1, associationEndName, new[] { expectedDataType }, expectedEntityName, new[] { expectedFK }, expectedHasM2MNavProp, expectedLinkTableNavProp, expectedM2MNavProp, new[] { expectedPK } );
        }

        private void CheckMultiaryAssociationIsValid( M2M4RiaShared.M2MAssociation actualAssociation, int arity, string associationEndName, string[] expectedDataTypes, string expectedEntityName, string[] expectedFKs, bool expectedHasM2MNavProp, string expectedLinkTableNavProp, string expectedM2MNavProp, string[] expectedPKs )
        {
            Assert.AreEqual( actualAssociation.PKDataType.Count, arity, "The arity of PKDataType of the association end '" + associationEndName + "' doesn't match" );
            for( int i = 0; i < arity; i++ )
            {
                Assert.AreEqual( expectedDataTypes[i], actualAssociation.PKDataType[i], string.Format( "PKDataType[{0}] is not equal for association end '" + associationEndName + "'", i ) );
            }
            Assert.IsNotNull( actualAssociation.Entity, "Entity is not set to an instance of an object for association end '" + associationEndName + "'. Entity '" + expectedEntityName + "' expected." );
            Assert.AreEqual( expectedEntityName, actualAssociation.Entity.Name, "Unexpected entity attached to association for association end '" + associationEndName + "'" );
            Assert.AreEqual( actualAssociation.FK.Count, arity, "The arity of FK of the association end '" + associationEndName + "' doesn't match" );
            for( int i = 0; i < arity; i++ )
            {
                Assert.AreEqual( expectedFKs[i], actualAssociation.FK[i], string.Format( "FK[{0}] is not equal for association end '" + associationEndName + "'", i ) );
            }
            Assert.AreEqual( expectedHasM2MNavProp, actualAssociation.HasM2MNavProp, "HasM2MNavProp is not equal for association end '" + associationEndName + "'" );
            Assert.AreEqual( expectedLinkTableNavProp, actualAssociation.LinkTableNavProp, "LinkTableNavProp is not equal for association end '" + associationEndName + "'" );
            Assert.AreEqual( expectedM2MNavProp, actualAssociation.M2MNavProp, "M2MNavProp is not equal for association end '" + associationEndName + "'" );
            Assert.AreEqual( actualAssociation.PK.Count, arity, "The arity of PK of the association end '" + associationEndName + "' doesn't match" );
            for( int i = 0; i < arity; i++ )
            {
                Assert.AreEqual( expectedPKs[i], actualAssociation.PK[i], string.Format( "PK[{0}] is not equal for association end '" + associationEndName + "'", i ) );
            }
        }

        private M2M4RiaShared.M2MEntity GetEntityByName(string name)
        {
            return M2MDataObject.Entities.Where<M2M4RiaShared.M2MEntity>(e => e.Name == name).FirstOrDefault();
        }

        private M2M4RiaShared.M2MAssociationSet GetAssociationSetByName(string name)
        {
            return M2MDataObject.Associations.Where<M2M4RiaShared.M2MAssociationSet>(e => e.Name == name).FirstOrDefault();
        }
         
        private M2M4RiaShared.M2MData GenerateM2MData()
        {
            try
            {
                M2M4RiaShared template = new M2M4RiaShared();
                template.Host = new Host();

                template.EdmxFilePath = EdmxFilePath;

                M2M4RiaShared.M2MData m2mData = template.GenerateM2MData();

                return m2mData;
            }
            catch (FileNotFoundException ex)
            {
                throw new FileNotFoundException("Unable to locate M2M4RiaTestModel.edmx file.  Make sure the 'Deployment' menu under the Local.testsettings file is configured to copy the DemoModel.edmx during test execution.  Check inner exception for extra details", ex);
            }
        }
    }
}
