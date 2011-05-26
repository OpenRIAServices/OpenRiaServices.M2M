


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
        // 'AnimalVetToVetSet' associationSet from 'Vet.VetId' to 'AnimalVet.VetId'
        private int _VetId;

        [DataMember]
        [Key]
        public int VetId
        {
            get
            {
                if(Vet != null)
                {
		            if(_VetId != Vet.VetId && _VetId == 0 )
                        _VetId = Vet.VetId;
                }
                return _VetId;
            }
            set
            {
                _VetId = value;
            }
        }

        [Include]
        [XmlIgnore]
        [Association("AnimalVetToVetSet", "VetId", "VetId", IsForeignKey = true)]
        [DataMember]
        public Vet Vet { get; set; }

        // 'AnimalVetToAnimalSet' associationSet from 'Animal.AnimalId' to 'AnimalVet.AnimalId'
        private int _AnimalId;

        [DataMember]
        [Key]
        public int AnimalId
        {
            get
            {
                if(Animal != null)
                {
		            if(_AnimalId != Animal.AnimalId && _AnimalId == 0 )
                        _AnimalId = Animal.AnimalId;
                }
                return _AnimalId;
            }
            set
            {
                _AnimalId = value;
            }
        }

        [Include]
        [XmlIgnore]
        [Association("AnimalVetToAnimalSet", "AnimalId", "AnimalId", IsForeignKey = true)]
        [DataMember]
        public Animal Animal { get; set; }
    }
    [Obsolete("This class is only intended for use by the RIA M2M solution")]
    public partial class DogFireHydrant
    {
        // 'DogFireHydrantToFireHydrantSet' associationSet from 'FireHydrant.FireHydrantId' to 'DogFireHydrant.FireHydrantId'
        private int _FireHydrantId;

        [DataMember]
        [Key]
        public int FireHydrantId
        {
            get
            {
                if(FireHydrant != null)
                {
		            if(_FireHydrantId != FireHydrant.FireHydrantId && _FireHydrantId == 0 )
                        _FireHydrantId = FireHydrant.FireHydrantId;
                }
                return _FireHydrantId;
            }
            set
            {
                _FireHydrantId = value;
            }
        }

        [Include]
        [XmlIgnore]
        [Association("DogFireHydrantToFireHydrantSet", "FireHydrantId", "FireHydrantId", IsForeignKey = true)]
        [DataMember]
        public FireHydrant FireHydrant { get; set; }

        // 'DogFireHydrantToDogSet' associationSet from 'Dog.AnimalId' to 'DogFireHydrant.DogId'
        private int _DogId;

        [DataMember]
        [Key]
        public int DogId
        {
            get
            {
                if(Dog != null)
                {
		            if(_DogId != Dog.AnimalId && _DogId == 0 )
                        _DogId = Dog.AnimalId;
                }
                return _DogId;
            }
            set
            {
                _DogId = value;
            }
        }

        [Include]
        [XmlIgnore]
        [Association("DogFireHydrantToDogSet", "DogId", "AnimalId", IsForeignKey = true)]
        [DataMember]
        public Dog Dog { get; set; }
    }
    [Obsolete("This class is only intended for use by the RIA M2M solution")]
    public partial class DogTrainer
    {
        // 'DogTrainerToTrainerSet' associationSet from 'Trainer.TrainerId' to 'DogTrainer.TrainerId'
        private int _TrainerId;

        [DataMember]
        [Key]
        public int TrainerId
        {
            get
            {
                if(Trainer != null)
                {
		            if(_TrainerId != Trainer.TrainerId && _TrainerId == 0 )
                        _TrainerId = Trainer.TrainerId;
                }
                return _TrainerId;
            }
            set
            {
                _TrainerId = value;
            }
        }

        [Include]
        [XmlIgnore]
        [Association("DogTrainerToTrainerSet", "TrainerId", "TrainerId", IsForeignKey = true)]
        [DataMember]
        public Trainer Trainer { get; set; }

        // 'DogTrainerToDogSet' associationSet from 'Dog.AnimalId' to 'DogTrainer.DogId'
        private int _DogId;

        [DataMember]
        [Key]
        public int DogId
        {
            get
            {
                if(Dog != null)
                {
		            if(_DogId != Dog.AnimalId && _DogId == 0 )
                        _DogId = Dog.AnimalId;
                }
                return _DogId;
            }
            set
            {
                _DogId = value;
            }
        }

        [Include]
        [XmlIgnore]
        [Association("DogTrainerToDogSet", "DogId", "AnimalId", IsForeignKey = true)]
        [DataMember]
        public Dog Dog { get; set; }
    }
    [Obsolete("This class is only intended for use by the RIA M2M solution")]
    public partial class CatAnimal
    {
        // 'CatAnimalToAnimalSet' associationSet from 'Animal.AnimalId' to 'CatAnimal.AnimalId'
        private int _AnimalId;

        [DataMember]
        [Key]
        public int AnimalId
        {
            get
            {
                if(Animal != null)
                {
		            if(_AnimalId != Animal.AnimalId && _AnimalId == 0 )
                        _AnimalId = Animal.AnimalId;
                }
                return _AnimalId;
            }
            set
            {
                _AnimalId = value;
            }
        }

        [Include]
        [XmlIgnore]
        [Association("CatAnimalToAnimalSet", "AnimalId", "AnimalId", IsForeignKey = true)]
        [DataMember]
        public Animal Animal { get; set; }

        // 'CatAnimalToCatSet' associationSet from 'Cat.AnimalId' to 'CatAnimal.CatId'
        private int _CatId;

        [DataMember]
        [Key]
        public int CatId
        {
            get
            {
                if(Cat != null)
                {
		            if(_CatId != Cat.AnimalId && _CatId == 0 )
                        _CatId = Cat.AnimalId;
                }
                return _CatId;
            }
            set
            {
                _CatId = value;
            }
        }

        [Include]
        [XmlIgnore]
        [Association("CatAnimalToCatSet", "CatId", "AnimalId", IsForeignKey = true)]
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
        [Association("AnimalVetToAnimalSet", "AnimalId", "AnimalId", IsForeignKey = false)]
        public IList<AnimalVet> AnimalVetToVetSet
        {
            get
            {
                Func<Vet, AnimalVet> makeJoinType = 
                    e => new AnimalVet { Animal = this, Vet = e };
                return Vets.Select(makeJoinType).ToList();
            }
        }
        [Obsolete("This property is only intended for use by the RIA M2M solution")]
        [DataMember]
        [Include]
        [Association("CatAnimalToAnimalSet", "AnimalId", "AnimalId", IsForeignKey = false)]
        public IList<CatAnimal> CatAnimalToCatSet
        {
            get
            {
                Func<Cat, CatAnimal> makeJoinType = 
                    e => new CatAnimal { Animal = this, Cat = e };
                return Cats.Select(makeJoinType).ToList();
            }
        }
    }
    public partial class Vet
    {
        [Obsolete("This property is only intended for use by the RIA M2M solution")]
        [DataMember]
        [Include]
        [Association("AnimalVetToVetSet", "VetId", "VetId", IsForeignKey = false)]
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
        [Association("DogTrainerToTrainerSet", "TrainerId", "TrainerId", IsForeignKey = false)]
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
    public partial class Dog
    {
        [Obsolete("This property is only intended for use by the RIA M2M solution")]
        [DataMember]
        [Include]
        [Association("DogFireHydrantToDogSet", "AnimalId", "DogId", IsForeignKey = false)]
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
        [Association("DogTrainerToDogSet", "AnimalId", "DogId", IsForeignKey = false)]
        public IList<DogTrainer> DogTrainerToTrainerSet
        {
            get
            {
                Func<Trainer, DogTrainer> makeJoinType = 
                    e => new DogTrainer { Dog = this, Trainer = e };
                return Trainers.Select(makeJoinType).ToList();
            }
        }
    }
    public partial class Cat
    {
        [Obsolete("This property is only intended for use by the RIA M2M solution")]
        [DataMember]
        [Include]
        [Association("CatAnimalToCatSet", "AnimalId", "CatId", IsForeignKey = false)]
        public IList<CatAnimal> CatAnimalToAnimalSet
        {
            get
            {
                Func<Animal, CatAnimal> makeJoinType = 
                    e => new CatAnimal { Cat = this, Animal = e };
                return Animals.Select(makeJoinType).ToList();
            }
        }
    }
    #endregion
}

// Restore compiler warnings when using obsolete methods
#pragma warning restore 618



