using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.ServiceModel.DomainServices.Client;

namespace M2M.RIA
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="JoinType"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    public class EntityCollection<JoinType, TEntity> : IEnumerable<TEntity>, IEnumerable, INotifyCollectionChanged, INotifyPropertyChanged
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
