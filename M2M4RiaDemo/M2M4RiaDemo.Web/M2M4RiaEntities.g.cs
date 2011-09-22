


// M2M4RiaShared.ttinclude has been located and loaded.

// Instruct compiler not to warn about usage of obsolete members, because using them is intended.
#pragma warning disable 618

namespace M2M4RiaDemo.Web.Model
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

        // 'DogTrainerToDogSet' associationSet from 'Dog.DogId' to 'DogTrainer.DogDogId'
        private int _DogDogId;

        [DataMember]
        [Key]
        public int DogDogId
        {
            get
            {
                if(Dog != null)
                {
		            if(_DogDogId != Dog.DogId && _DogDogId == default(int))
					{
                        _DogDogId = Dog.DogId;
					}
                }
                return _DogDogId;
            }
            set
            {
                _DogDogId = value;
            }
        }

        [Include]
        [XmlIgnore]
        [Association("DogTrainerToTrainerSet", "TrainerTrainerId", "TrainerId", IsForeignKey = true)]
        [DataMember]
        public Trainer Trainer { get; set; }

        [Include]
        [XmlIgnore]
        [Association("DogTrainerToDogSet", "DogDogId", "DogId", IsForeignKey = true)]
        [DataMember]
        public Dog Dog { get; set; }
    }
    //
    // Regular Entity Types
    //
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
    public partial class Dog
    {
        [Obsolete("This property is only intended for use by the RIA M2M solution")]
        [DataMember]
        [Include]
        [Association("DogTrainerToDogSet", "DogId", "DogDogId", IsForeignKey = false)]
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
    #endregion
}

// Restore compiler warnings when using obsolete methods
#pragma warning restore 618



