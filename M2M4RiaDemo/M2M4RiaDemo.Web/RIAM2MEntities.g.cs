

 

// RIAM2MShared.ttinclude has been located and loaded.

 

#pragma warning disable 618

#region Entities

namespace M2M4RiaDemo.Web.Model  
{
	using System;
	using System.ComponentModel.DataAnnotations;
	using System.Runtime.Serialization;
	using System.ServiceModel.DomainServices.Server;
	using RIAM2M.Web.Services.RIAM2MTools;

	//
	// Association Entity Types
	//
	[Obsolete("This class is only intended for use by the RIA M2M solution")]
	public partial class AnimalVet
	{

		// 'AnimalVetToVet' associationSet from 'Vet.VetId' to 'AnimalVet.VetId'
		private int _VetId;
		
		[DataMember]
		[Key]
		public int VetId
		{
			get
			{
				if(Vet != null)
				{
					if(_VetId != Vet.VetId && _VetId == 0)
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
		[Association("AnimalVetToVet", "VetId", "VetId", IsForeignKey = true)]
		[DataMember]
		public Vet Vet { get; set; }
		
		// 'AnimalVetToAnimal' associationSet from 'Animal.AnimalId' to 'AnimalVet.AnimalId'
		private int _AnimalId;

		[DataMember]
		[Key]
		public int AnimalId
		{
			get
			{
				if(Animal != null)
				{
					if(_AnimalId != Animal.AnimalId && _AnimalId == 0)
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
		[Association("AnimalVetToAnimal", "AnimalId", "AnimalId", IsForeignKey = true)]
		[DataMember]
		public Animal Animal { get; set; }

	}
		
	[Obsolete("This class is only intended for use by the RIA M2M solution")]
	public partial class DogFireHydrant
	{

		// 'DogFireHydrantToFireHydrant' associationSet from 'FireHydrant.FireHydrantId' to 'DogFireHydrant.FireHydrantId'
		private int _FireHydrantId;
		
		[DataMember]
		[Key]
		public int FireHydrantId
		{
			get
			{
				if(FireHydrant != null)
				{
					if(_FireHydrantId != FireHydrant.FireHydrantId && _FireHydrantId == 0)
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
		[Association("DogFireHydrantToFireHydrant", "FireHydrantId", "FireHydrantId", IsForeignKey = true)]
		[DataMember]
		public FireHydrant FireHydrant { get; set; }
		
		// 'DogFireHydrantToDog' associationSet from 'Dog.AnimalId' to 'DogFireHydrant.DogId'
		private int _DogId;

		[DataMember]
		[Key]
		public int DogId
		{
			get
			{
				if(Dog != null)
				{
					if(_DogId != Dog.AnimalId && _DogId == 0)
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
		[Association("DogFireHydrantToDog", "DogId", "AnimalId", IsForeignKey = true)]
		[DataMember]
		public Dog Dog { get; set; }

	}
		
	[Obsolete("This class is only intended for use by the RIA M2M solution")]
	public partial class DogTrainer
	{

		// 'DogTrainerToTrainer' associationSet from 'Trainer.TrainerId' to 'DogTrainer.TrainerId'
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
		[Association("DogTrainerToTrainer", "TrainerId", "TrainerId", IsForeignKey = true)]
		[DataMember]
		public Trainer Trainer { get; set; }
		
		// 'DogTrainerToDog' associationSet from 'Dog.AnimalId' to 'DogTrainer.DogId'
		private int _DogId;

		[DataMember]
		[Key]
		public int DogId
		{
			get
			{
				if(Dog != null)
				{
					if(_DogId != Dog.AnimalId && _DogId == 0)
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
		[Association("DogTrainerToDog", "DogId", "AnimalId", IsForeignKey = true)]
		[DataMember]
		public Dog Dog { get; set; }

	}
		
	//
	// Regular Entity Types
	//
		
	public partial class Animal
	{
		// 'AnimalVetToVet' associationSet from 'Vet.VetId' to 'AnimalVet.VetId'
		private M2MEntityCollection<AnimalVet, Vet> _AnimalVetToVet;
		
		[Obsolete("This property is only intended for use by the RIA M2M solution")]
		[DataMember]
		[Include]
		[Association("AnimalVetToAnimal", "AnimalId", "AnimalId", IsForeignKey = false)]
		public M2MEntityCollection<AnimalVet, Vet> AnimalVetToVet
		{
			get
			{
				if(_AnimalVetToVet == null)
				{
					_AnimalVetToVet = new M2MEntityCollection<AnimalVet, Vet>
					(
						Vets,
						(r) => new AnimalVet { Animal = this, Vet = r },
						pd => pd.Vet
					);
				}
				
				return _AnimalVetToVet;
			}
		}
		
	}
		
	public partial class Vet
	{
		// 'AnimalVetToAnimal' associationSet from 'Animal.AnimalId' to 'AnimalVet.AnimalId'
		private M2MEntityCollection<AnimalVet, Animal> _AnimalVetToAnimal;
		
		[Obsolete("This property is only intended for use by the RIA M2M solution")]
		[DataMember]
		[Include]
		[Association("AnimalVetToVet", "VetId", "VetId", IsForeignKey = false)]
		public M2MEntityCollection<AnimalVet, Animal> AnimalVetToAnimal
		{
			get
			{
				if(_AnimalVetToAnimal == null)
				{
					_AnimalVetToAnimal = new M2MEntityCollection<AnimalVet, Animal>
					(
						Animals,
						(r) => new AnimalVet { Vet = this, Animal = r },
						pd => pd.Animal
					);
				}
				
				return _AnimalVetToAnimal;
			}
		}
		
	}
		
	public partial class FireHydrant
	{
	}
		
	public partial class Trainer
	{
		// 'DogTrainerToDog' associationSet from 'Dog.AnimalId' to 'DogTrainer.DogId'
		private M2MEntityCollection<DogTrainer, Dog> _DogTrainerToDog;
		
		[Obsolete("This property is only intended for use by the RIA M2M solution")]
		[DataMember]
		[Include]
		[Association("DogTrainerToTrainer", "TrainerId", "TrainerId", IsForeignKey = false)]
		public M2MEntityCollection<DogTrainer, Dog> DogTrainerToDog
		{
			get
			{
				if(_DogTrainerToDog == null)
				{
					_DogTrainerToDog = new M2MEntityCollection<DogTrainer, Dog>
					(
						Dogs,
						(r) => new DogTrainer { Trainer = this, Dog = r },
						pd => pd.Dog
					);
				}
				
				return _DogTrainerToDog;
			}
		}
		
	}
		
	public partial class Food
	{
	}
		
	public partial class Dog
	{
		// 'DogFireHydrantToFireHydrant' associationSet from 'FireHydrant.FireHydrantId' to 'DogFireHydrant.FireHydrantId'
		private M2MEntityCollection<DogFireHydrant, FireHydrant> _DogFireHydrantToFireHydrant;
		
		[Obsolete("This property is only intended for use by the RIA M2M solution")]
		[DataMember]
		[Include]
		[Association("DogFireHydrantToDog", "AnimalId", "DogId", IsForeignKey = false)]
		public M2MEntityCollection<DogFireHydrant, FireHydrant> DogFireHydrantToFireHydrant
		{
			get
			{
				if(_DogFireHydrantToFireHydrant == null)
				{
					_DogFireHydrantToFireHydrant = new M2MEntityCollection<DogFireHydrant, FireHydrant>
					(
						FireHydrants,
						(r) => new DogFireHydrant { Dog = this, FireHydrant = r },
						pd => pd.FireHydrant
					);
				}
				
				return _DogFireHydrantToFireHydrant;
			}
		}
		
		// 'DogTrainerToTrainer' associationSet from 'Trainer.TrainerId' to 'DogTrainer.TrainerId'
		private M2MEntityCollection<DogTrainer, Trainer> _DogTrainerToTrainer;
		
		[Obsolete("This property is only intended for use by the RIA M2M solution")]
		[DataMember]
		[Include]
		[Association("DogTrainerToDog", "AnimalId", "DogId", IsForeignKey = false)]
		public M2MEntityCollection<DogTrainer, Trainer> DogTrainerToTrainer
		{
			get
			{
				if(_DogTrainerToTrainer == null)
				{
					_DogTrainerToTrainer = new M2MEntityCollection<DogTrainer, Trainer>
					(
						Trainers,
						(r) => new DogTrainer { Dog = this, Trainer = r },
						pd => pd.Trainer
					);
				}
				
				return _DogTrainerToTrainer;
			}
		}
		
	}
}

#endregion

#region M2MEntityCollection

namespace RIAM2M.Web.Services.RIAM2MTools
{
    using System;
    using System.Collections.Generic;
    using System.Data.Objects.DataClasses;
    using System.Linq;

    public class M2MEntityCollection<JoinType, TEntity> : IEnumerable<JoinType>
        where JoinType : new()
        where TEntity : class
    {
        private ICollection<TEntity> collection;
        private Func<TEntity, JoinType> newJoinType;
        private Func<JoinType, TEntity> getEntity;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="collection">Entity collection that represents a m2m relation</param>
        /// <param name="newJoinType">The function used to create a new joint type entity and set both elements</param>
        public M2MEntityCollection(ICollection<TEntity> collection,Func<TEntity, JoinType> newJoinType, Func<JoinType, TEntity> getEntity)
        {
            this.collection = collection;
            this.newJoinType = newJoinType;
            this.getEntity = getEntity;
        }


        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
        /// <summary>
        /// Construct an enumerator by creating JoinType objects for each element in the associated m2m collection
        /// </summary>
        /// <returns></returns>
        public IEnumerator<JoinType> GetEnumerator()
        {
            return collection.Select(newJoinType).GetEnumerator();
        }

        /// <summary>
        /// Not clear if this method should have an implementation. It is only called for newly created JoinType objects.
        /// However, the corresponding domainservice operation will already take the appropriate action the add a new association obejct.
        /// Is there a need to also add similar functionality here?
        /// </summary>
        /// <param name="entity"></param>
        public void Add(JoinType entity)
        {
            // Empty
        }
    }
}

#endregion

#pragma warning restore 618
		


