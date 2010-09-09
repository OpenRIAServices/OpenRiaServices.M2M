using System;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServerTests;

namespace RIAM2M.Web.Tests
{
    [TestClass]
    public class RIAM2MSharedTests
    {
        // The unit test settings for this solution have been configured to copy the DemoMode.edmx file to the root directory
        // of the unit test's workspace.  See the "Deployment" menu under the Local.testsettings file in the "Solution Items" solution folder.
        private const string EdmxFilePath = "DemoModel.edmx";
        private readonly Type[] ExpectedAssocationTypes = new Type[] { };

        private RIAM2MShared.M2MData _M2MDataObject;

        private RIAM2MShared.M2MData M2MDataObject
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
            string[] requiredAssociationSets = { "AnimalVet", "DogFireHydrant", "DogTrainer", "AnimalFood" };

            Assert.IsTrue(M2MDataObject.Associations.All(e => requiredAssociationSets.Contains(e.Name)), "M2MData.Associations does not match the expected AsssociationSet list");
        }

        /// <summary>
        /// Check to see if the M2MData object contains all of the expected Entities
        /// </summary>
        [TestMethod]
        public void GenerateM2MData_InspectReturnType_ShouldContainRequiredEntities()
        {
            string[] requiredEntities = { "Animal", "Vet", "FireHydrant", "Trainer", "Food", "Dog" };

            Assert.IsTrue(M2MDataObject.Entities.All(e => requiredEntities.Contains(e.Name)), "M2MData.Entities does not match the expected Entities list");
        }

        /// <summary>
        /// Check to see if the AnimalVet AssociationSet contains valid data
        /// </summary>
        [TestMethod]
        public void GenerateM2MData_InspectReturnType_CheckAssociationSetIsValidForAnimalVet()
        {
            RIAM2MShared.M2MAssociationSet associationSet = GetAssociationSetByName("AnimalVet");

            CheckAssociationIsValid(associationSet.Entity1ToLink, "Entity1ToLink", "int", "Animal", "AnimalId", true, "AnimalVetToAnimal", "Vets", "AnimalId");
            CheckAssociationIsValid(associationSet.Entity2ToLink, "Entity2ToLink", "int", "Vet", "VetId", true, "AnimalVetToVet", "Animals", "VetId");
        }

        /// <summary>
        /// Check to see if the DogFireHydrant AssociationSet contains valid data
        /// </summary>
        [TestMethod]
        public void GenerateM2MData_InspectReturnType_CheckAssociationSetIsValidForDogFireHydrant()
        {
            RIAM2MShared.M2MAssociationSet associationSet = GetAssociationSetByName("DogFireHydrant");

            CheckAssociationIsValid(associationSet.Entity1ToLink, "Entity1ToLink", "int", "Dog", "DogId", true, "DogFireHydrantToDog", "FireHydrants", "AnimalId");
            CheckAssociationIsValid(associationSet.Entity2ToLink, "Entity2ToLink", "int", "FireHydrant", "FireHydrantId", false, "DogFireHydrantToFireHydrant", null, "FireHydrantId");
        }

        /// <summary>
        /// Check to see if the DogTrainer AssociationSet contains valid data
        /// </summary>
        [TestMethod]
        public void GenerateM2MData_InspectReturnType_CheckAssociationSetIsValidForDogTrainer()
        {
            RIAM2MShared.M2MAssociationSet associationSet = GetAssociationSetByName("DogTrainer");

            CheckAssociationIsValid(associationSet.Entity1ToLink, "Entity1ToLink", "int", "Dog", "DogId", true, "DogTrainerToDog", "Trainers", "AnimalId");
            CheckAssociationIsValid(associationSet.Entity2ToLink, "Entity2ToLink", "int", "Trainer", "TrainerId", true, "DogTrainerToTrainer", "Dogs", "TrainerId");
        }
         

        /// <summary>
        /// Check to see if the AnimalFood AssociationSet contains valid data
        /// </summary>
        [TestMethod]
        public void GenerateM2MData_InspectReturnType_CheckAssociationSetIsValidForAnimalFood()
        {
            RIAM2MShared.M2MAssociationSet associationSet = GetAssociationSetByName("AnimalFood");

            CheckAssociationIsValid(associationSet.Entity1ToLink, "Entity1ToLink", "int", "Animal", "AnimalId", false, "AnimalFoodToAnimal", null, "AnimalId");
            CheckAssociationIsValid(associationSet.Entity2ToLink, "Entity2ToLink", "int", "Food", "FoodId", false, "AnimalFoodToFood", null, "FoodId");
        }

        /// <summary>
        /// Check to see if the Animal entity contains valid data
        /// </summary>
        [TestMethod]
        public void GenerateM2MData_InspectReturnType_CheckEntityIsValidForAnimal()
        {
            CheckEntityIsValid("Animal", new string[] { "AnimalFood", "AnimalVet" }, new string[] { "Cat", "Dog" }, "Animals", false, true, null); 
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
            CheckEntityIsValid("Dog", new string[] { "DogFireHydrant", "DogTrainer" }, new string[] { }, "Animals", false, false, "Animal"); 
        }

        private void CheckEntityIsValid(string actualEntityName, string[] expectedAssociationSetNames, string[] expectedDerivedEntityNames, string expectedEntitySet, bool expectedIsAbstract, bool expectedIsBaseEntity, string expectedBaseEntityName)
        {
            RIAM2MShared.M2MEntity entity = GetEntityByName(actualEntityName);

            if (!(expectedAssociationSetNames.Length == 0 && entity.Associations.Count == 0))
                Assert.IsTrue(entity.Associations.All(e => expectedAssociationSetNames.Contains(e.Name)), "Entity.Associations does not match the expected AssociationSet list");

            if (!(expectedDerivedEntityNames.Length == 0 && entity.DerivedEntityNames.Count == 0))
                Assert.IsTrue(entity.DerivedEntityNames.All(e => expectedDerivedEntityNames.Contains(e)), "Entity.DerivedEntityNames does not match the expected derived entity name list");

            Assert.AreEqual(expectedEntitySet, entity.EntitySet, "EntitySet is not equal");
            Assert.AreEqual(expectedIsAbstract, entity.IsAbstract, "IsAbstract is not equal");
            Assert.AreEqual(expectedIsBaseEntity, entity.IsBaseEntity, "IsBaseEntity is not equal");
            Assert.AreEqual(expectedBaseEntityName, entity.BaseEntityName, "BaseEntityName is not equal");
        }

        private void CheckAssociationIsValid(RIAM2MShared.M2MAssociation actualAssociation, string associationEndName, string expectedDataType, string expectedEntityName, string expectedFK, bool expectedHasM2MNavProp, string expectedLinkTableNavProp, string expectedM2MNavProp, string expectedPK)
        {
            Assert.AreEqual(expectedDataType, actualAssociation.DataType, "DataType is not equal for association end '" + associationEndName + "'");
            Assert.IsNotNull(actualAssociation.Entity, "Entity is not set to an instance of an object for association end '" + associationEndName + "'. Entity '" + expectedEntityName + "' expected.");
            Assert.AreEqual(expectedEntityName, actualAssociation.Entity.Name, "Unexpected entity attached to association for association end '" + associationEndName + "'");
            Assert.AreEqual(expectedFK, actualAssociation.FK, "FK is not equal for association end '" + associationEndName + "'");
            Assert.AreEqual(expectedHasM2MNavProp, actualAssociation.HasM2MNavProp, "HasM2MNavProp is not equal for association end '" + associationEndName + "'");
            Assert.AreEqual(expectedLinkTableNavProp, actualAssociation.LinkTableNavProp, "LinkTableNavProp is not equal for association end '" + associationEndName + "'");
            Assert.AreEqual(expectedM2MNavProp, actualAssociation.M2MNavProp, "M2MNavProp is not equal for association end '" + associationEndName + "'");
            Assert.AreEqual(expectedPK, actualAssociation.PK, "PK is not equal for association end '" + associationEndName + "'");
        }

        private RIAM2MShared.M2MEntity GetEntityByName(string name)
        {
            return M2MDataObject.Entities.Where<RIAM2MShared.M2MEntity>(e => e.Name == name).FirstOrDefault();
        }

        private RIAM2MShared.M2MAssociationSet GetAssociationSetByName(string name)
        {
            return M2MDataObject.Associations.Where<RIAM2MShared.M2MAssociationSet>(e => e.Name == name).FirstOrDefault();
        }
         
        private RIAM2MShared.M2MData GenerateM2MData()
        {
            try
            {
                RIAM2MShared template = new RIAM2MShared();

                template.EdmxFilePath = EdmxFilePath;

                RIAM2MShared.M2MData m2mData = template.GenerateM2MData();

                return m2mData;
            }
            catch (FileNotFoundException ex)
            {
                throw new FileNotFoundException("Unable to located DemoModel.edmx file.  Make sure the 'Deployment' menu under the Local.testsettings file is configured to copy the DemoModel.edmx during test execution.  Check inner exception for extra details", ex);
            }
        }
    }
}
