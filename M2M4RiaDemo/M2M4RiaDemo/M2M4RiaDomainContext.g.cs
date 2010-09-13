

 

// M2M4RiaShared.ttinclude has been located and loaded.


// Instruct compiler not to warn about usage of obsolete members, because using them is intended.
#pragma warning disable 618

#region Domain Context

namespace M2M4RiaDemo.Web.Service
{
    using System.ServiceModel.DomainServices.Client;
	using M2M4RiaDemo.Web.Model;
	
    public partial class M2M4RiaDemoContext
    {
        /// <summary>
        /// Gets the set of <see cref="DogTrainer"/> entities that have been loaded into this <see cref="M2M4RiaDemoContext"/> instance.
        /// </summary>
        public EntitySet<DogTrainer> DogTrainers
        {
            get
            {
                return base.EntityContainer.GetEntitySet<DogTrainer>();
            }
        }
		partial void OnCreated()
        {
			// Install handlers that set/reset EntitySet properties of link table entities when they are 
			// added/removed from the domain context's entity sets. This is only needed as long as RIA
			// doesn't provide access from an Entity to its EntitySet.
			EntitySet< DogTrainer > DogTrainerEntitySet = EntityContainer.GetEntitySet< DogTrainer>();
			DogTrainerEntitySet.EntityAdded += (sender, args) => args.Entity.EntitySet = DogTrainerEntitySet;
			DogTrainerEntitySet.EntityRemoved += (sender, args) => args.Entity.EntitySet = null;
			
		}
	}
}

#endregion

namespace M2M4RiaDemo.Web.Model
{
	#region Entities
	
	using System;
	using System.ServiceModel.DomainServices.Client;

	using M2M4Ria;
	
  	/// <summary>
	/// This class provides access to the entity's entity set. This is only needed as long as RIA
	// doesn't provide this access it self.
	/// </summary>
	public partial class DogTrainer
	{
		/// <summary>
        /// Gets or sets the EntitySet the link table entity is contained in. It is set by the domain context
		/// when the link entity is added to an entity set, and reset to null when it is removed from an entity set.
        /// </summary>
		[Obsolete("This property is only intended for use by the M2M4Ria solution.")]
		public EntitySet<DogTrainer> EntitySet{ get; set; }
	}
	public partial class Trainer
	{
	
		//
		// Code relating to the managing of the 'DogTrainer' association from 'Trainer' to 'Dog'
		//
		
		private IEntityCollection<Dog> _Dogs;
		
		public IEntityCollection<Dog> Dogs
		{
			get
			{
				if(_Dogs == null)
				{
					_Dogs = new EntityCollection<DogTrainer, Dog>(this.DogTrainerToDogSet, r => r.Dog, (r, t2) => r.Dog = t2, r => r.Trainer = this, RemoveFromDogTrainerToDogSet);
				}
				
				return _Dogs;
			}
		}
		
		private void RemoveFromDogTrainerToDogSet(DogTrainer r)
		{
			if(r.EntitySet == null)
			{
				this.DogTrainerToDogSet.Remove(r);
			}
			else
			{
				r.EntitySet.Remove(r);
			}
		}
	}
	
	public partial class Dog
	{
	
		//
		// Code relating to the managing of the 'DogTrainer' association from 'Dog' to 'Trainer'
		//
		
		private IEntityCollection<Trainer> _Trainers;
		
		public IEntityCollection<Trainer> Trainers
		{
			get
			{
				if(_Trainers == null)
				{
					_Trainers = new EntityCollection<DogTrainer, Trainer>(this.DogTrainerToTrainerSet, r => r.Trainer, (r, t2) => r.Trainer = t2, r => r.Dog = this, RemoveFromDogTrainerToTrainerSet);
				}
				
				return _Trainers;
			}
		}
		
		private void RemoveFromDogTrainerToTrainerSet(DogTrainer r)
		{
			if(r.EntitySet == null)
			{
				this.DogTrainerToTrainerSet.Remove(r);
			}
			else
			{
				r.EntitySet.Remove(r);
			}
		}
	}
	
	#endregion

	#region EntityCollection

	namespace M2M4Ria
	{
		using System;
		using System.Collections;
		using System.Collections.Generic;
		using System.Collections.Specialized;
		using System.ComponentModel;
		using System.Linq;
		using System.ServiceModel.DomainServices.Client;

        /// <summary>
        /// Defines methods for manipulation a generic EntityCollection 
        /// </summary>
        /// <typeparam name="TEntity">The type of the elements in the collection</typeparam>
        public interface IEntityCollection<TEntity> : IEnumerable<TEntity>, IEnumerable, INotifyCollectionChanged, INotifyPropertyChanged
        {
            /// <summary>
            /// Gets the current count of entities in this collection
            /// </summary>
            int Count { get; }
            /// <summary>
            /// Event raised whenever an System.ServiceModel.DomainServices.Client.Entity
            /// is added to this collection
            /// </summary>
            event EventHandler<EntityCollectionChangedEventArgs<TEntity>> EntityAdded;
            /// <summary>
            /// Event raised whenever an System.ServiceModel.DomainServices.Client.Entity
            /// is removed from this collection
            /// </summary>
            event EventHandler<EntityCollectionChangedEventArgs<TEntity>> EntityRemoved;
            /// <summary>
            /// Add the specified entity to this collection. If the entity is unattached,
            /// it will be added to its System.ServiceModel.DomainServices.Client.EntitySet
            /// automatically.
            /// </summary>
            /// <param name="entity"> The entity to add</param>
            void Add(TEntity entity);
            /// <summary>
            /// Remove the specified entity from this collection.
            /// </summary>
            /// <param name="entity">The entity to remove</param>
            void Remove(TEntity entity);
        }

		/// <summary>
		/// M2M-specific entity collection class. It vorms a view on the underlying jointable collection.
		/// </summary>
		/// <typeparam name="JoinType"></typeparam>
		/// <typeparam name="TEntity"></typeparam>
		public class EntityCollection<JoinType, TEntity> : IEntityCollection<TEntity>
			where JoinType : Entity, new()
			where TEntity : Entity
		{
			EntityCollection<JoinType> collection;
			Func<JoinType, TEntity> getEntity;
			Action<JoinType, TEntity> setEntity;
			Action<JoinType> setParent;
			Action<JoinType> removeAction;
			/// <summary>
			/// 
			/// </summary>
			/// <param name="collection">The collection of associations to which this collection is connected</param>
			/// <param name="getEntity">The function used to get the entity object out of a join type entity</param>
			/// <param name="setEntity">The function used to set the entity object in a join type entity</param>
			public EntityCollection(EntityCollection<JoinType> collection, Func<JoinType, TEntity> getEntity,
				Action<JoinType, TEntity> setEntity, Action<JoinType> setParent, Action<JoinType> removeAction)
			{
				this.collection = collection;
				this.getEntity = getEntity;
				this.setEntity = setEntity;
				this.setParent = setParent;
				this.removeAction = removeAction;

				collection.EntityAdded += (a, b) =>
				{
					JoinType jt = b.Entity as JoinType;
					if (EntityAdded != null)
						EntityAdded(this, new EntityCollectionChangedEventArgs<TEntity>(getEntity(jt)));
				};
				collection.EntityRemoved += (a, b) =>
				{
					JoinType jt = b.Entity as JoinType;
					if (EntityRemoved != null)
						EntityRemoved(this, new EntityCollectionChangedEventArgs<TEntity>(getEntity(jt)));
				};
				((INotifyCollectionChanged)collection).CollectionChanged += (sender, e) =>
				{
					if (CollectionChanged != null)
						CollectionChanged(this, MakeNotifyCollectionChangedEventArgs(e));
				};
				((INotifyPropertyChanged)collection).PropertyChanged += (sender, e) =>
				{
					if (PropertyChanged != null)
						PropertyChanged(this, e);
				};
			}

			/// <summary>
			/// Replaces JoinType elements in NotifyCollectionChangedEventArgs by elements of type TEntity
			/// </summary>
			/// <param name="e"></param>
			/// <returns></returns>
			private NotifyCollectionChangedEventArgs MakeNotifyCollectionChangedEventArgs(NotifyCollectionChangedEventArgs e)
			{
				if (e.NewItems != null)
				{
					TEntity entity = getEntity((JoinType)e.NewItems[0]);
					e.NewItems[0] = entity == null ? entityToAdd : entity;
				}
				if (e.OldItems != null)
				{
					TEntity entity = getEntity((JoinType)e.OldItems[0]);
					e.OldItems[0] = entity;
				}
				return e;
			}

			public IEnumerator<TEntity> GetEnumerator()
			{
				return collection.Select(getEntity).GetEnumerator();
			}

			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			public int Count
			{
				get
				{
					return collection.Count;
				}
			}

			TEntity entityToAdd = null;
			public void Add(TEntity entity)
			{
				entityToAdd = entity;
				JoinType joinTypeToAdd = new JoinType();
				setParent(joinTypeToAdd);
				setEntity(joinTypeToAdd, entity);
				entityToAdd = null;
			}
			/// <summary>
			/// Use remove on the entityset on the domain context, rather than this functioln
			/// There seems to be a limitation of RIA which requires that associations should be deleted on the domain context
			/// </summary>
			/// <param name="entity"></param>
			public void Remove(TEntity entity)
			{
				JoinType joinTypeToRemove = collection.SingleOrDefault(jt => getEntity(jt) == entity);
				if (joinTypeToRemove != null)
					removeAction(joinTypeToRemove);
			}

			public event EventHandler<EntityCollectionChangedEventArgs<TEntity>> EntityAdded;
			public event EventHandler<EntityCollectionChangedEventArgs<TEntity>> EntityRemoved;
			public event NotifyCollectionChangedEventHandler CollectionChanged;
			public event PropertyChangedEventHandler PropertyChanged;
		}
	}

	#endregion

}

// Restore compiler warnings when using obsolete methods
#pragma warning restore 618

