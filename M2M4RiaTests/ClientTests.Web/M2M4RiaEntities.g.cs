


// M2M4RiaShared.ttinclude has been located and loaded.

// Instruct compiler not to warn about usage of obsolete members, because using them is intended.
#pragma warning disable 618

namespace ClientTests.Web
{
    #region Entities

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.ServiceModel.DomainServices.Server;
    using System.Xml.Serialization;

    //
    // Association Entity Types
    //
    [Obsolete("This class is only intended for use by the RIA M2M solution")]
    public partial class AnimalVet
    {
        // 'AnimalVetToVetSet' associationSet from 'Vet.VetId' to 'AnimalVet.VetVetId'
        private int _VetVetId;

        [DataMember]
        [Key]
        public int VetVetId
        {
            get
            {
                if(Vet != null)
                {
		            if(_VetVetId != Vet.VetId && _VetVetId == default(int))
					{
                        _VetVetId = Vet.VetId;
					}
                }
                return _VetVetId;
            }
            set
            {
                _VetVetId = value;
            }
        }

        // 'AnimalVetToAnimalSet' associationSet from 'Animal.AnimalId' to 'AnimalVet.AnimalAnimalId'
        private int _AnimalAnimalId;

        [DataMember]
        [Key]
        public int AnimalAnimalId
        {
            get
            {
                if(Animal != null)
                {
		            if(_AnimalAnimalId != Animal.AnimalId && _AnimalAnimalId == default(int))
					{
                        _AnimalAnimalId = Animal.AnimalId;
					}
                }
                return _AnimalAnimalId;
            }
            set
            {
                _AnimalAnimalId = value;
            }
        }

        [Include]
        [XmlIgnore]
        [Association("AnimalVetToVetSet", "VetVetId", "VetId", IsForeignKey = true)]
        [DataMember]
        public Vet Vet { get; set; }

        [Include]
        [XmlIgnore]
        [Association("AnimalVetToAnimalSet", "AnimalAnimalId", "AnimalId", IsForeignKey = true)]
        [DataMember]
        public Animal Animal { get; set; }
    }
    [Obsolete("This class is only intended for use by the RIA M2M solution")]
    public partial class DogFireHydrant
    {
        // 'DogFireHydrantToFireHydrantSet' associationSet from 'FireHydrant.FireHydrantId' to 'DogFireHydrant.FireHydrantFireHydrantId'
        private int _FireHydrantFireHydrantId;

        [DataMember]
        [Key]
        public int FireHydrantFireHydrantId
        {
            get
            {
                if(FireHydrant != null)
                {
		            if(_FireHydrantFireHydrantId != FireHydrant.FireHydrantId && _FireHydrantFireHydrantId == default(int))
					{
                        _FireHydrantFireHydrantId = FireHydrant.FireHydrantId;
					}
                }
                return _FireHydrantFireHydrantId;
            }
            set
            {
                _FireHydrantFireHydrantId = value;
            }
        }

        // 'DogFireHydrantToDogSet' associationSet from 'Dog.AnimalId' to 'DogFireHydrant.DogAnimalId'
        private int _DogAnimalId;

        [DataMember]
        [Key]
        public int DogAnimalId
        {
            get
            {
                if(Dog != null)
                {
		            if(_DogAnimalId != Dog.AnimalId && _DogAnimalId == default(int))
					{
                        _DogAnimalId = Dog.AnimalId;
					}
                }
                return _DogAnimalId;
            }
            set
            {
                _DogAnimalId = value;
            }
        }

        [Include]
        [XmlIgnore]
        [Association("DogFireHydrantToFireHydrantSet", "FireHydrantFireHydrantId", "FireHydrantId", IsForeignKey = true)]
        [DataMember]
        public FireHydrant FireHydrant { get; set; }

        [Include]
        [XmlIgnore]
        [Association("DogFireHydrantToDogSet", "DogAnimalId", "AnimalId", IsForeignKey = true)]
        [DataMember]
        public Dog Dog { get; set; }
    }
    [Obsolete("This class is only intended for use by the RIA M2M solution")]
    public partial class DogTrainer
    {
        // 'DogTrainerToTrainerSet' associationSet from 'Trainer.TrainerId' to 'DogTrainer.TrainerTrainerId'
        private int _TrainerTrainerId;

        [DataMember]
        [Key]
        public int TrainerTrainerId
        {
            get
            {
                if(Trainer != null)
                {
		            if(_TrainerTrainerId != Trainer.TrainerId && _TrainerTrainerId == default(int))
					{
                        _TrainerTrainerId = Trainer.TrainerId;
					}
                }
                return _TrainerTrainerId;
            }
            set
            {
                _TrainerTrainerId = value;
            }
        }

        // 'DogTrainerToDogSet' associationSet from 'Dog.AnimalId' to 'DogTrainer.DogAnimalId'
        private int _DogAnimalId;

        [DataMember]
        [Key]
        public int DogAnimalId
        {
            get
            {
                if(Dog != null)
                {
		            if(_DogAnimalId != Dog.AnimalId && _DogAnimalId == default(int))
					{
                        _DogAnimalId = Dog.AnimalId;
					}
                }
                return _DogAnimalId;
            }
            set
            {
                _DogAnimalId = value;
            }
        }

        [Include]
        [XmlIgnore]
        [Association("DogTrainerToTrainerSet", "TrainerTrainerId", "TrainerId", IsForeignKey = true)]
        [DataMember]
        public Trainer Trainer { get; set; }

        [Include]
        [XmlIgnore]
        [Association("DogTrainerToDogSet", "DogAnimalId", "AnimalId", IsForeignKey = true)]
        [DataMember]
        public Dog Dog { get; set; }
    }
    [Obsolete("This class is only intended for use by the RIA M2M solution")]
    public partial class AnimalFood
    {

        [Include]
        [XmlIgnore]
        [Association("AnimalFoodToFoodSet", "FoodFoodId", "FoodId", IsForeignKey = true)]
        [DataMember]
        public Food Food { get; set; }

        [Include]
        [XmlIgnore]
        [Association("AnimalFoodToAnimalSet", "AnimalAnimalId", "AnimalId", IsForeignKey = true)]
        [DataMember]
        public Animal Animal { get; set; }
    }
    [Obsolete("This class is only intended for use by the RIA M2M solution")]
    public partial class DogDog
    {
        // 'DogDogToDogAsParentSet' associationSet from 'DogAsParent.AnimalId' to 'DogDog.DogAsParentAnimalId'
        private int _DogAsParentAnimalId;

        [DataMember]
        [Key]
        public int DogAsParentAnimalId
        {
            get
            {
                if(DogAsParent != null)
                {
		            if(_DogAsParentAnimalId != DogAsParent.AnimalId && _DogAsParentAnimalId == default(int))
					{
                        _DogAsParentAnimalId = DogAsParent.AnimalId;
					}
                }
                return _DogAsParentAnimalId;
            }
            set
            {
                _DogAsParentAnimalId = value;
            }
        }

        // 'DogDogToDogAsPuppySet' associationSet from 'DogAsPuppy.AnimalId' to 'DogDog.DogAsPuppyAnimalId'
        private int _DogAsPuppyAnimalId;

        [DataMember]
        [Key]
        public int DogAsPuppyAnimalId
        {
            get
            {
                if(DogAsPuppy != null)
                {
		            if(_DogAsPuppyAnimalId != DogAsPuppy.AnimalId && _DogAsPuppyAnimalId == default(int))
					{
                        _DogAsPuppyAnimalId = DogAsPuppy.AnimalId;
					}
                }
                return _DogAsPuppyAnimalId;
            }
            set
            {
                _DogAsPuppyAnimalId = value;
            }
        }

        [Include]
        [XmlIgnore]
        [Association("DogDogToDogAsParentSet", "DogAsParentAnimalId", "AnimalId", IsForeignKey = true)]
        [DataMember]
        public Dog DogAsParent { get; set; }

        [Include]
        [XmlIgnore]
        [Association("DogDogToDogAsPuppySet", "DogAsPuppyAnimalId", "AnimalId", IsForeignKey = true)]
        [DataMember]
        public Dog DogAsPuppy { get; set; }
    }
    [Obsolete("This class is only intended for use by the RIA M2M solution")]
    public partial class CatMarkedTerritories
    {
        // 'CatMarkedTerritoriesToMarkedTerritorySet' associationSet from 'MarkedTerritory.TerritoryId' to 'CatMarkedTerritories.MarkedTerritoryTerritoryId'
        private System.Guid _MarkedTerritoryTerritoryId;

        [DataMember]
        [Key]
        public System.Guid MarkedTerritoryTerritoryId
        {
            get
            {
                if(MarkedTerritory != null)
                {
		            if(_MarkedTerritoryTerritoryId != MarkedTerritory.TerritoryId && _MarkedTerritoryTerritoryId == default(System.Guid))
					{
                        _MarkedTerritoryTerritoryId = MarkedTerritory.TerritoryId;
					}
                }
                return _MarkedTerritoryTerritoryId;
            }
            set
            {
                _MarkedTerritoryTerritoryId = value;
            }
        }
        // 'CatMarkedTerritoriesToMarkedTerritorySet' associationSet from 'MarkedTerritory.CoordX' to 'CatMarkedTerritories.MarkedTerritoryCoordX'
        private int _MarkedTerritoryCoordX;

        [DataMember]
        [Key]
        public int MarkedTerritoryCoordX
        {
            get
            {
                if(MarkedTerritory != null)
                {
		            if(_MarkedTerritoryCoordX != MarkedTerritory.CoordX && _MarkedTerritoryCoordX == default(int))
					{
                        _MarkedTerritoryCoordX = MarkedTerritory.CoordX;
					}
                }
                return _MarkedTerritoryCoordX;
            }
            set
            {
                _MarkedTerritoryCoordX = value;
            }
        }
        // 'CatMarkedTerritoriesToMarkedTerritorySet' associationSet from 'MarkedTerritory.CoordY' to 'CatMarkedTerritories.MarkedTerritoryCoordY'
        private int _MarkedTerritoryCoordY;

        [DataMember]
        [Key]
        public int MarkedTerritoryCoordY
        {
            get
            {
                if(MarkedTerritory != null)
                {
		            if(_MarkedTerritoryCoordY != MarkedTerritory.CoordY && _MarkedTerritoryCoordY == default(int))
					{
                        _MarkedTerritoryCoordY = MarkedTerritory.CoordY;
					}
                }
                return _MarkedTerritoryCoordY;
            }
            set
            {
                _MarkedTerritoryCoordY = value;
            }
        }
        // 'CatMarkedTerritoriesToMarkedTerritorySet' associationSet from 'MarkedTerritory.CoordZ' to 'CatMarkedTerritories.MarkedTerritoryCoordZ'
        private int _MarkedTerritoryCoordZ;

        [DataMember]
        [Key]
        public int MarkedTerritoryCoordZ
        {
            get
            {
                if(MarkedTerritory != null)
                {
		            if(_MarkedTerritoryCoordZ != MarkedTerritory.CoordZ && _MarkedTerritoryCoordZ == default(int))
					{
                        _MarkedTerritoryCoordZ = MarkedTerritory.CoordZ;
					}
                }
                return _MarkedTerritoryCoordZ;
            }
            set
            {
                _MarkedTerritoryCoordZ = value;
            }
        }

        // 'CatMarkedTerritoriesToCatSet' associationSet from 'Cat.AnimalId' to 'CatMarkedTerritories.CatAnimalId'
        private int _CatAnimalId;

        [DataMember]
        [Key]
        public int CatAnimalId
        {
            get
            {
                if(Cat != null)
                {
		            if(_CatAnimalId != Cat.AnimalId && _CatAnimalId == default(int))
					{
                        _CatAnimalId = Cat.AnimalId;
					}
                }
                return _CatAnimalId;
            }
            set
            {
                _CatAnimalId = value;
            }
        }

        [Include]
        [XmlIgnore]
        [Association("CatMarkedTerritoriesToMarkedTerritorySet", "MarkedTerritoryTerritoryId,MarkedTerritoryCoordX,MarkedTerritoryCoordY,MarkedTerritoryCoordZ", "TerritoryId,CoordX,CoordY,CoordZ", IsForeignKey = true)]
        [DataMember]
        public MarkedTerritory MarkedTerritory { get; set; }

        [Include]
        [XmlIgnore]
        [Association("CatMarkedTerritoriesToCatSet", "CatAnimalId", "AnimalId", IsForeignKey = true)]
        [DataMember]
        public Cat Cat { get; set; }
    }
    //
    // Regular Entity Types
    //
    public partial class Animal
    {
        [Obsolete("This property is only intended for use by the RIA M2M solution")]
        [DataMember]
        [Include]
        [Association("AnimalVetToAnimalSet", "AnimalId", "AnimalAnimalId", IsForeignKey = false)]
        public IList<AnimalVet> AnimalVetToVetSet
        {
            get
            {
                Func<Vet, AnimalVet> makeJoinType = 
                    e => new AnimalVet { Animal = this, Vet = e };
                return Vets.Select(makeJoinType).ToList();
            }
        }
    }
    public partial class Vet
    {
        [Obsolete("This property is only intended for use by the RIA M2M solution")]
        [DataMember]
        [Include]
        [Association("AnimalVetToVetSet", "VetId", "VetVetId", IsForeignKey = false)]
        public IList<AnimalVet> AnimalVetToAnimalSet
        {
            get
            {
                Func<Animal, AnimalVet> makeJoinType = 
                    e => new AnimalVet { Vet = this, Animal = e };
                return Animals.Select(makeJoinType).ToList();
            }
        }
    }
    public partial class FireHydrant
    {
    }
    public partial class Trainer
    {
        [Obsolete("This property is only intended for use by the RIA M2M solution")]
        [DataMember]
        [Include]
        [Association("DogTrainerToTrainerSet", "TrainerId", "TrainerTrainerId", IsForeignKey = false)]
        public IList<DogTrainer> DogTrainerToDogSet
        {
            get
            {
                Func<Dog, DogTrainer> makeJoinType = 
                    e => new DogTrainer { Trainer = this, Dog = e };
                return Dogs.Select(makeJoinType).ToList();
            }
        }
    }
    public partial class Food
    {
    }
    public partial class MarkedTerritory
    {
        [Obsolete("This property is only intended for use by the RIA M2M solution")]
        [DataMember]
        [Include]
        [Association("CatMarkedTerritoriesToMarkedTerritorySet", "TerritoryId,CoordX,CoordY,CoordZ", "MarkedTerritoryTerritoryId,MarkedTerritoryCoordX,MarkedTerritoryCoordY,MarkedTerritoryCoordZ", IsForeignKey = false)]
        public IList<CatMarkedTerritories> CatMarkedTerritoriesToCatSet
        {
            get
            {
                Func<Cat, CatMarkedTerritories> makeJoinType = 
                    e => new CatMarkedTerritories { MarkedTerritory = this, Cat = e };
                return Cats.Select(makeJoinType).ToList();
            }
        }
    }
    public partial class Dog
    {
        [Obsolete("This property is only intended for use by the RIA M2M solution")]
        [DataMember]
        [Include]
        [Association("DogFireHydrantToDogSet", "AnimalId", "DogAnimalId", IsForeignKey = false)]
        public IList<DogFireHydrant> DogFireHydrantToFireHydrantSet
        {
            get
            {
                Func<FireHydrant, DogFireHydrant> makeJoinType = 
                    e => new DogFireHydrant { Dog = this, FireHydrant = e };
                return FireHydrants.Select(makeJoinType).ToList();
            }
        }
        [Obsolete("This property is only intended for use by the RIA M2M solution")]
        [DataMember]
        [Include]
        [Association("DogTrainerToDogSet", "AnimalId", "DogAnimalId", IsForeignKey = false)]
        public IList<DogTrainer> DogTrainerToTrainerSet
        {
            get
            {
                Func<Trainer, DogTrainer> makeJoinType = 
                    e => new DogTrainer { Dog = this, Trainer = e };
                return Trainers.Select(makeJoinType).ToList();
            }
        }
        [Obsolete("This property is only intended for use by the RIA M2M solution")]
        [DataMember]
        [Include]
        [Association("DogDogToDogAsPuppySet", "AnimalId", "DogAsPuppyAnimalId", IsForeignKey = false)]
        public IList<DogDog> DogDogToDogAsParentSet
        {
            get
            {
                Func<Dog, DogDog> makeJoinType = 
                    e => new DogDog { DogAsPuppy = this, DogAsParent = e };
                return Puppies.Select(makeJoinType).ToList();
            }
        }
        [Obsolete("This property is only intended for use by the RIA M2M solution")]
        [DataMember]
        [Include]
        [Association("DogDogToDogAsParentSet", "AnimalId", "DogAsParentAnimalId", IsForeignKey = false)]
        public IList<DogDog> DogDogToDogAsPuppySet
        {
            get
            {
                Func<Dog, DogDog> makeJoinType = 
                    e => new DogDog { DogAsParent = this, DogAsPuppy = e };
                return Parents.Select(makeJoinType).ToList();
            }
        }
    }
    public partial class Cat
    {
        [Obsolete("This property is only intended for use by the RIA M2M solution")]
        [DataMember]
        [Include]
        [Association("CatMarkedTerritoriesToCatSet", "AnimalId", "CatAnimalId", IsForeignKey = false)]
        public IList<CatMarkedTerritories> CatMarkedTerritoriesToMarkedTerritorySet
        {
            get
            {
                Func<MarkedTerritory, CatMarkedTerritories> makeJoinType = 
                    e => new CatMarkedTerritories { Cat = this, MarkedTerritory = e };
                return MarkedTerritories.Select(makeJoinType).ToList();
            }
        }
    }
    #endregion
}

// Restore compiler warnings when using obsolete methods
#pragma warning restore 618



