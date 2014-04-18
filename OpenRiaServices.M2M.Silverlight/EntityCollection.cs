using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using OpenRiaServices.DomainServices.Client;

namespace OpenRiaServices.M2M
{
    /// <summary>
    ///   M2M-specific entity collection class. It forms a view on the underlying link table collection.
    /// </summary>
    /// <typeparam name="TLinkTable"> </typeparam>
    /// <typeparam name="TEntity"> </typeparam>
    public class EntityCollection<TLinkTable, TEntity> : IEntityCollection<TEntity>
        where TLinkTable : Entity, new() where TEntity : Entity
    {
        #region Constants and Fields

        private readonly Action<TEntity> _addAction;

        private readonly EntityCollection<TLinkTable> _collection;

        private readonly Func<TLinkTable, TEntity> _getEntity;

        private readonly Action<TLinkTable> _removeAction;

        #endregion

        #region Constructors and Destructor

        ///<summary>
        ///</summary>
        ///<param name="collection"> The collection of associations to which this collection is connected </param>
        ///<param name="getEntity"> The function used to get the entity object out of a link table entity </param>
        ///<param name="removeAction"> </param>
        ///<param name="addAction"> </param>
        public EntityCollection(
            EntityCollection<TLinkTable> collection,
            Func<TLinkTable, TEntity> getEntity,
            Action<TLinkTable> removeAction,
            Action<TEntity> addAction)
        {
            _collection = collection;
            _getEntity = getEntity;
            _removeAction = removeAction;
            _addAction = addAction;

            collection.EntityAdded += OnEntityAdded;
            collection.EntityRemoved += OnEntityRemoved;
            ((INotifyCollectionChanged)collection).CollectionChanged += OnCollectionChanged;
            ((INotifyPropertyChanged)collection).PropertyChanged += OnPropertyChanged;
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, e);
            }
        }

        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if(CollectionChanged == null) return;
            // Checkout http://m2m4ria.codeplex.com/wikipage?title=CreatingAnM2MAssociationBetweenTwoNewEntities
            if(!(e.Action == NotifyCollectionChangedAction.Reset && _collection.Select(_getEntity).Any(x => x == null)))
            {
                CollectionChanged(this, MakeNotifyCollectionChangedEventArgs(e));
            }
        }

        private void OnEntityRemoved(object sender, EntityCollectionChangedEventArgs<TLinkTable> e)
        {
            if(EntityRemoved != null)
            {
                EntityRemoved(this, new EntityCollectionChangedEventArgs<TEntity>(_getEntity(e.Entity)));
            }
        }

        private void OnEntityAdded(object sender, EntityCollectionChangedEventArgs<TLinkTable> e)
        {
            if(EntityAdded != null)
            {
                EntityAdded(this, new EntityCollectionChangedEventArgs<TEntity>(_getEntity(e.Entity)));
            }
        }

        #endregion

        #region Public Events

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public event EventHandler<EntityCollectionChangedEventArgs<TEntity>> EntityAdded;

        public event EventHandler<EntityCollectionChangedEventArgs<TEntity>> EntityRemoved;

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Public Properties

        public int Count
        {
            get { return _collection.Count; }
        }

        #endregion

        #region Public Methods and Operators

        public void Add(TEntity entity)
        {
            _addAction(entity);
        }

        public IEnumerator<TEntity> GetEnumerator()
        {
            return _collection.Select(_getEntity).GetEnumerator();
        }

        /// <summary>
        ///   Removes an m2m relation with the given entity.
        /// </summary>
        /// <param name="entity"> </param>
        public void Remove(TEntity entity)
        {
            var linkTableEntityToRemove = _collection.SingleOrDefault(jt => _getEntity(jt) == entity);
            if(linkTableEntityToRemove != null)
            {
                _removeAction(linkTableEntityToRemove);
            }
        }

        #endregion

        #region Explicit Interface Methods

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region Methods

        /// <summary>
        ///   Replaces Link Table entities in NotifyCollectionChangedEventArgs by elements of type TEntity
        /// </summary>
        /// <param name="e"> </param>
        /// <returns> </returns>
        private NotifyCollectionChangedEventArgs MakeNotifyCollectionChangedEventArgs(
            NotifyCollectionChangedEventArgs e)
        {
            switch(e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    {
                        var entity = _getEntity((TLinkTable)e.NewItems[0]);
                        return new NotifyCollectionChangedEventArgs(e.Action, entity, e.NewStartingIndex);
                    }
                case NotifyCollectionChangedAction.Remove:
                    {
                        var entity = _getEntity((TLinkTable)e.OldItems[0]);
                        return new NotifyCollectionChangedEventArgs(e.Action, entity, e.OldStartingIndex);
                    }
                case NotifyCollectionChangedAction.Reset:
                    return new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset);
            }
            throw new Exception(
                String.Format(
                    "NotifyCollectionChangedAction.{0} action not supported by M2M4Ria.EntityCollection",
                    e.Action));
        }

        #endregion
    }
}