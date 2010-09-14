

 

// M2M4RiaShared.ttinclude has been located and loaded.

 
// Instruct compiler not to warn about usage of obsolete members, because using them is intended.
#pragma warning disable 618


namespace M2M4RiaDemo.Web.Model  
{
	#region Entities

	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.Runtime.Serialization;
	using System.ServiceModel.DomainServices.Server;
	using M2M4Ria;

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
		[Association("DogTrainerToDogSet", "DogId", "DogId", IsForeignKey = true)]
		[DataMember]
		public Dog Dog { get; set; }

	}
		
	//
	// Regular Entity Types
	//
		
	public partial class Trainer
	{
		// 'DogTrainerToDogSet' associationSet from 'Dog.DogId' to 'DogTrainer.DogId'
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
		
	public partial class Dog
	{
		// 'DogTrainerToTrainerSet' associationSet from 'Trainer.TrainerId' to 'DogTrainer.TrainerId'
		private IEnumerable<DogTrainer> _DogTrainerToTrainerSet;
		
		[Obsolete("This property is only intended for use by the RIA M2M solution")]
		[DataMember]
		[Include]
		[Association("DogTrainerToDogSet", "DogId", "DogId", IsForeignKey = false)]
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
		using System.Data.Objects.DataClasses;
		using System.Linq;

		public class EntityCollection<JoinType, TEntity> : IEnumerable<JoinType>
			where JoinType : new()
			where TEntity : class
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
}

// Restore compiler warnings when using obsolete methods
#pragma warning restore 618
		


