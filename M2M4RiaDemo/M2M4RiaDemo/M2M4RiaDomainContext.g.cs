


// M2M4RiaShared.ttinclude has been located and loaded.

#region http://riaservicescontrib.codeplex.com/
namespace RiaServicesContrib
{
	public interface IExtendedEntity
    {
        System.ServiceModel.DomainServices.Client.EntitySet EntitySet { get; }
    }
}
#endregion

#region Entities
namespace M2M4RiaDemo.Web.Model
{
    using System;
    using System.ServiceModel.DomainServices.Client;
	using RiaServicesContrib;
    using M2M4Ria;

    /// <summary>
    /// This class provides access to the entity's entity set and contains methods for attaching
	/// to entities to the link table in a single action.
    /// </summary>
    public partial class DogTrainer : IExtendedEntity
    {
        /// <summary>
        /// This method attaches Dog and Trainer to the current join table entity, in such a way
        /// that both navigation properties are set before an INotifyCollectionChanged event is fired.
        /// </summary>
        /// <param name="r"></param>
        /// <param name="dog"></param>
        /// <param name="trainer"></param>
        [Obsolete("This property is only intended for use by the M2M4Ria solution.")]
        public static void AttachTrainerToDog(DogTrainer r, Dog dog, Trainer trainer)
        {
            var dummy = r.Trainer; // this is to instantiate the EntityRef<Trainer>
            r._trainer.Entity = trainer;
            r._trainerTrainerId = trainer.TrainerId;

            r.Dog = dog;

            r._trainer.Entity = null;
            r._trainerTrainerId = default(int);
            r.Trainer = trainer;
        }
        /// <summary>
        /// This method attaches Trainer and Dog to the current join table entity, in such a way
        /// that both navigation properties are set before an INotifyCollectionChanged event is fired.
        /// </summary>
        /// <param name="r"></param>
        /// <param name="trainer"></param>
        /// <param name="dog"></param>
        [Obsolete("This property is only intended for use by the M2M4Ria solution.")]
        public static void AttachDogToTrainer(DogTrainer r, Trainer trainer, Dog dog)
        {
            var dummy = r.Dog; // this is to instantiate the EntityRef<Dog>
            r._dog.Entity = dog;
            r._dogDogId = dog.DogId;

            r.Trainer = trainer;

            r._dog.Entity = null;
            r._dogDogId = default(int);
            r.Dog = dog;
        }
        /// <summary>
        /// Gets the EntitySet the link table entity is contained in.
        /// </summary>
        EntitySet IExtendedEntity.EntitySet
        {
            get
            {
                return EntitySet;
            }
        }
    }
    public partial class Trainer
    {
        //
        // Code relating to the managing of the 'DogTrainer' association from 'Trainer' to 'Dog'
        //
        private IEntityCollection<Dog> _Dogs;

        /// <summary>
        /// Gets the collection of associated <see cref="Dog"/> entities.
        /// </summary>
        public IEntityCollection<Dog> Dogs
        {
            get
            {
                if(_Dogs == null)
                {
                    _Dogs = new EntityCollection<DogTrainer, Dog>(
						this.DogTrainerToDogSet,
						r => r.Dog,
						RemoveTrainerDog,
						AddTrainerDog
				    );
                }
                return _Dogs;
            }
        }

        // Instruct compiler not to warn about usage of obsolete members, because using them is intended.
        #pragma warning disable 618
        private void AddTrainerDog(Dog dog)
		{
            var newJoinType = new DogTrainer();
            DogTrainer.AttachDogToTrainer(newJoinType, this, dog);
		}
		#pragma warning restore 618

        private void RemoveTrainerDog(DogTrainer r)
        {
            if(((IExtendedEntity)r).EntitySet == null)
            {
                this.DogTrainerToDogSet.Remove(r);
            }
            else
            {
                ((IExtendedEntity)r).EntitySet.Remove(r);
            }
        }
    }
    public partial class Dog
    {
        //
        // Code relating to the managing of the 'DogTrainer' association from 'Dog' to 'Trainer'
        //
        private IEntityCollection<Trainer> _Trainers;

        /// <summary>
        /// Gets the collection of associated <see cref="Trainer"/> entities.
        /// </summary>
        public IEntityCollection<Trainer> Trainers
        {
            get
            {
                if(_Trainers == null)
                {
                    _Trainers = new EntityCollection<DogTrainer, Trainer>(
						this.DogTrainerToTrainerSet,
						r => r.Trainer,
						RemoveDogTrainer,
						AddDogTrainer
				    );
                }
                return _Trainers;
            }
        }

        // Instruct compiler not to warn about usage of obsolete members, because using them is intended.
        #pragma warning disable 618
        private void AddDogTrainer(Trainer trainer)
		{
            var newJoinType = new DogTrainer();
            DogTrainer.AttachTrainerToDog(newJoinType, this, trainer);
		}
		#pragma warning restore 618

        private void RemoveDogTrainer(DogTrainer r)
        {
            if(((IExtendedEntity)r).EntitySet == null)
            {
                this.DogTrainerToTrainerSet.Remove(r);
            }
            else
            {
                ((IExtendedEntity)r).EntitySet.Remove(r);
            }
        }
    }
}
#endregion

#region EntityCollection
namespace M2M4RiaDemo.Web.Model.M2M4Ria
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
        Action<JoinType> removeAction;
		Action<TEntity> addAction;
        /// <summary>
        ///
        /// </summary>
        /// <param name="collection">The collection of associations to which this collection is connected</param>
        /// <param name="getEntity">The function used to get the entity object out of a join type entity</param>
        /// <param name="setEntity">The function used to set the entity object in a join type entity</param>
        public EntityCollection(
			EntityCollection<JoinType> collection,
			Func<JoinType, TEntity> getEntity,
            Action<JoinType> removeAction,
			Action<TEntity> addAction)
        {
            this.collection = collection;
            this.getEntity = getEntity;
            this.removeAction = removeAction;
            this.addAction = addAction;

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
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    {
                        TEntity entity = getEntity((JoinType)e.NewItems[0]);
                        return new NotifyCollectionChangedEventArgs(e.Action, entity, indexOfChange);
                    }
                case NotifyCollectionChangedAction.Remove:
                    {
                        TEntity entity = getEntity((JoinType)e.OldItems[0]);
                        return new NotifyCollectionChangedEventArgs(e.Action, entity, indexOfChange);
                    }
                case NotifyCollectionChangedAction.Reset:
                    return new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset);
            }
            throw new Exception(String.Format("NotifyCollectionChangedAction.{0} action not supported by M2M4Ria.EntityCollection", e.Action.ToString()));
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

        private int IndexOf(TEntity entity)
        {
            int index = 0;
            foreach(TEntity e in this){
                if(e == entity)
                    return index;
                index++;
            }
            return -1;
        }

        // Indicates the index where a change of the collection occurred.
        private int indexOfChange;

        public void Add(TEntity entity)
        {
			addAction(entity);
        }

        /// <summary>
        /// Removes an m2m relation with the given entity.
        /// </summary>
        /// <param name="entity"></param>
        public void Remove(TEntity entity)
        {
            indexOfChange = IndexOf(entity);
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


