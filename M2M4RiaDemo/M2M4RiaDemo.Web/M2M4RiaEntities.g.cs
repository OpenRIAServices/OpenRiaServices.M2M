


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
                    if(_TrainerId != Trainer.TrainerId && _TrainerId == 0)
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

        // 'DogTrainerToDogSet' associationSet from 'Dog.DogId' to 'DogTrainer.DogId'
        private int _DogId;

        [DataMember]
        [Key]
        public int DogId
        {
            get
            {
                if(Dog != null)
                {
                    if(_DogId != Dog.DogId && _DogId == 0)
                        _DogId = Dog.DogId;
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
        [Association("DogTrainerToDogSet", "DogId", "DogId", IsForeignKey = true)]
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
    public partial class Dog
    {
        [Obsolete("This property is only intended for use by the RIA M2M solution")]
        [DataMember]
        [Include]
        [Association("DogTrainerToDogSet", "DogId", "DogId", IsForeignKey = false)]
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



