using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.ServiceModel.DomainServices.Client;

namespace RIAServices.M2M
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

        private readonly Action<TEntity> addAction;

        private readonly EntityCollection<TLinkTable> collection;

        private readonly Func<TLinkTable, TEntity> getEntity;

        private readonly Action<TLinkTable> removeAction;

        // Indicates the index where a change of the collection occurred.
        private int indexOfChange;

        #endregion

        #region Constructors and Destructors

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
            this.collection = collection;
            this.getEntity = getEntity;
            this.removeAction = removeAction;
            this.addAction = addAction;

            collection.EntityAdded += (a, b) =>
                                          {
                                              var jt = b.Entity;
                                              if(EntityAdded != null)
                                              {
                                                  EntityAdded(this,
                                                              new EntityCollectionChangedEventArgs<TEntity>(getEntity(jt)));
                                              }
                                          };
            collection.EntityRemoved += (a, b) =>
                                            {
                                                var jt = b.Entity;
                                                if(EntityRemoved != null)
                                                {
                                                    EntityRemoved(this,
                                                                  new EntityCollectionChangedEventArgs<TEntity>(
                                                                      getEntity(jt)));
                                                }
                                            };
            ((INotifyCollectionChanged) collection).CollectionChanged += (sender, e) =>
                                                                             {
                                                                                 if(CollectionChanged != null)
                                                                                 {
                                                                                     // Checkout http://m2m4ria.codeplex.com/wikipage?title=CreatingAnM2MAssociationBetweenTwoNewEntities
                                                                                     if(
                                                                                         !(e.Action ==
                                                                                           NotifyCollectionChangedAction
                                                                                               .Reset &&
                                                                                           collection.Select(getEntity).
                                                                                               Any(x => x == null)))
                                                                                     {
                                                                                         CollectionChanged(this,
                                                                                                           MakeNotifyCollectionChangedEventArgs
                                                                                                               (e));
                                                                                     }
                                                                                 }
                                                                             };
            ((INotifyPropertyChanged) collection).PropertyChanged += (sender, e) =>
                                                                         {
                                                                             if(PropertyChanged != null)
                                                                             {
                                                                                 PropertyChanged(this, e);
                                                                             }
                                                                         };
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
            get { return collection.Count; }
        }

        #endregion

        #region Public Methods and Operators

        public void Add(TEntity entity)
        {
            addAction(entity);
        }

        public IEnumerator<TEntity> GetEnumerator()
        {
            return collection.Select(getEntity).GetEnumerator();
        }

        /// <summary>
        ///   Removes an m2m relation with the given entity.
        /// </summary>
        /// <param name="entity"> </param>
        public void Remove(TEntity entity)
        {
            indexOfChange = IndexOf(entity);
            var linkTableEntityToRemove = collection.SingleOrDefault(jt => getEntity(jt) == entity);
            if(linkTableEntityToRemove != null)
            {
                removeAction(linkTableEntityToRemove);
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

        private int IndexOf(TEntity entity)
        {
            var index = 0;
            foreach(var e in this)
            {
                if(e == entity)
                {
                    return index;
                }
                index++;
            }
            return -1;
        }

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
                        var entity = getEntity((TLinkTable) e.NewItems[0]);
                        return new NotifyCollectionChangedEventArgs(e.Action, entity, indexOfChange);
                    }
                case NotifyCollectionChangedAction.Remove:
                    {
                        var entity = getEntity((TLinkTable) e.OldItems[0]);
                        return new NotifyCollectionChangedEventArgs(e.Action, entity, indexOfChange);
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