

 

// M2M4RiaShared.ttinclude has been located and loaded.

 
// Instruct compiler not to warn about usage of obsolete members, because using them is intended.
#pragma warning disable 618


namespace ClientTests.Web  
{
	#region Entities

	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.Runtime.Serialization;
	using System.ServiceModel.DomainServices.Server;
    using System.Xml.Serialization;
	using M2M4Ria;

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
		[XmlIgnore]
		[Association("DogTrainerToDogSet", "DogId", "AnimalId", IsForeignKey = true)]
		[DataMember]
		public Dog Dog { get; set; }
	}
	//
	// Regular Entity Types
	//
	public partial class Animal
	{
		// 'AnimalVetToVetSet' associationSet from 'Vet.VetId' to 'AnimalVet.VetId'
		private IEnumerable<AnimalVet> _AnimalVetToVetSet;
		
		[Obsolete("This property is only intended for use by the RIA M2M solution")]
		[DataMember]
		[Include]
		[Association("AnimalVetToAnimalSet", "AnimalId", "AnimalId", IsForeignKey = false)]
		public IEnumerable<AnimalVet> AnimalVetToVetSet
		{
			get
			{
				if(_AnimalVetToVetSet == null)
				{
					_AnimalVetToVetSet = new EntityCollection<AnimalVet, Vet>
					(
						Vets,
						(r) => new AnimalVet { Animal = this, Vet = r }
					);
				}
				return _AnimalVetToVetSet;
			}
		}
	}
	public partial class Vet
	{
		// 'AnimalVetToAnimalSet' associationSet from 'Animal.AnimalId' to 'AnimalVet.AnimalId'
		private IEnumerable<AnimalVet> _AnimalVetToAnimalSet;
		
		[Obsolete("This property is only intended for use by the RIA M2M solution")]
		[DataMember]
		[Include]
		[Association("AnimalVetToVetSet", "VetId", "VetId", IsForeignKey = false)]
		public IEnumerable<AnimalVet> AnimalVetToAnimalSet
		{
			get
			{
				if(_AnimalVetToAnimalSet == null)
				{
					_AnimalVetToAnimalSet = new EntityCollection<AnimalVet, Animal>
					(
						Animals,
						(r) => new AnimalVet { Vet = this, Animal = r }
					);
				}
				return _AnimalVetToAnimalSet;
			}
		}
	}
	public partial class FireHydrant
	{
	}
	public partial class Trainer
	{
		// 'DogTrainerToDogSet' associationSet from 'Dog.AnimalId' to 'DogTrainer.DogId'
		private IEnumerable<DogTrainer> _DogTrainerToDogSet;
		
		[Obsolete("This property is only intended for use by the RIA M2M solution")]
		[DataMember]
		[Include]
		[Association("DogTrainerToTrainerSet", "TrainerId", "TrainerId", IsForeignKey = false)]
		public IEnumerable<DogTrainer> DogTrainerToDogSet
		{
			get
			{
				if(_DogTrainerToDogSet == null)
				{
					_DogTrainerToDogSet = new EntityCollection<DogTrainer, Dog>
					(
						Dogs,
						(r) => new DogTrainer { Trainer = this, Dog = r }
					);
				}
				return _DogTrainerToDogSet;
			}
		}
	}
	public partial class Food
	{
	}
	public partial class Dog
	{
		// 'DogFireHydrantToFireHydrantSet' associationSet from 'FireHydrant.FireHydrantId' to 'DogFireHydrant.FireHydrantId'
		private IEnumerable<DogFireHydrant> _DogFireHydrantToFireHydrantSet;
		
		[Obsolete("This property is only intended for use by the RIA M2M solution")]
		[DataMember]
		[Include]
		[Association("DogFireHydrantToDogSet", "AnimalId", "DogId", IsForeignKey = false)]
		public IEnumerable<DogFireHydrant> DogFireHydrantToFireHydrantSet
		{
			get
			{
				if(_DogFireHydrantToFireHydrantSet == null)
				{
					_DogFireHydrantToFireHydrantSet = new EntityCollection<DogFireHydrant, FireHydrant>
					(
						FireHydrants,
						(r) => new DogFireHydrant { Dog = this, FireHydrant = r }
					);
				}
				return _DogFireHydrantToFireHydrantSet;
			}
		}
		// 'DogTrainerToTrainerSet' associationSet from 'Trainer.TrainerId' to 'DogTrainer.TrainerId'
		private IEnumerable<DogTrainer> _DogTrainerToTrainerSet;
		
		[Obsolete("This property is only intended for use by the RIA M2M solution")]
		[DataMember]
		[Include]
		[Association("DogTrainerToDogSet", "AnimalId", "DogId", IsForeignKey = false)]
		public IEnumerable<DogTrainer> DogTrainerToTrainerSet
		{
			get
			{
				if(_DogTrainerToTrainerSet == null)
				{
					_DogTrainerToTrainerSet = new EntityCollection<DogTrainer, Trainer>
					(
						Trainers,
						(r) => new DogTrainer { Dog = this, Trainer = r }
					);
				}
				return _DogTrainerToTrainerSet;
			}
		}
	}
	#endregion

	#region EntityCollection
	namespace M2M4Ria
	{
		using System;
		using System.Collections.Generic;
		using System.Linq;

		public class EntityCollection<JoinType, TEntity> : IEnumerable<JoinType> where JoinType : new()	where TEntity : class
		{
			private ICollection<TEntity> collection;
			private Func<TEntity, JoinType> newJoinType;
			/// <summary>
			/// Constructor
			/// </summary>
			/// <param name="collection">Entity collection that represents a m2m relation</param>
			/// <param name="newJoinType">The function used to create a new joint type entity and set both elements</param>
			public EntityCollection(ICollection<TEntity> collection,Func<TEntity, JoinType> newJoinType)
			{
				this.collection = collection;
				this.newJoinType = newJoinType;
			}
            /// <summary>
            ///     Constructs an enumerator of JoinType objects which forms a jointype view on the underlying many-2-many collection 
            /// </summary>
            /// <returns>
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
			/// However, the corresponding domainservice operation will already take the appropriate action to add a new association object.
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
}

// Restore compiler warnings when using obsolete methods
#pragma warning restore 618
		


