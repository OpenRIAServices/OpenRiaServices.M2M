using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using OpenRiaServices.DomainServices.Client;

namespace RIAServices.M2M
{
    /// <summary>
    ///   Defines methods for manipulation a generic EntityCollection
    /// </summary>
    /// <typeparam name="TEntity"> The type of the elements in the collection </typeparam>
    public interface IEntityCollection<TEntity> : IEnumerable<TEntity>, INotifyCollectionChanged, INotifyPropertyChanged
    {
        #region Public Events

        /// <summary>
        ///   Event raised whenever an System.ServiceModel.DomainServices.Client.Entity
        ///   is added to this collection
        /// </summary>
        event EventHandler<EntityCollectionChangedEventArgs<TEntity>> EntityAdded;

        /// <summary>
        ///   Event raised whenever an System.ServiceModel.DomainServices.Client.Entity
        ///   is removed from this collection
        /// </summary>
        event EventHandler<EntityCollectionChangedEventArgs<TEntity>> EntityRemoved;

        #endregion

        #region Public Properties

        /// <summary>
        ///   Gets the current count of entities in this collection
        /// </summary>
        int Count { get; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///   Add the specified entity to this collection. If the entity is unattached,
        ///   it will be added to its System.ServiceModel.DomainServices.Client.EntitySet
        ///   automatically.
        /// </summary>
        /// <param name="entity"> The entity to add </param>
        void Add(TEntity entity);

        /// <summary>
        ///   Remove the specified entity from this collection.
        /// </summary>
        /// <param name="entity"> The entity to remove </param>
        void Remove(TEntity entity);

        #endregion
    }
}