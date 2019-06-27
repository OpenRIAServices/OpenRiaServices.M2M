using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using OpenRiaServices.DomainServices.Client;

namespace OpenRiaServices.M2M
{
    /// <summary>
    ///   Defines methods for manipulation a generic EntityCollection
    /// </summary>
    /// <typeparam name="TEntity"> The type of the elements in the collection </typeparam>
    public interface IM2MEntityCollection<TEntity> : ICollection<TEntity>, INotifyCollectionChanged, INotifyPropertyChanged
    {
        /// <summary>
        ///   Event raised whenever an <see cref="Entity"/> is added to this collection
        /// </summary>
        event EventHandler<EntityCollectionChangedEventArgs<TEntity>> EntityAdded;

        /// <summary>
        ///   Event raised whenever an  <see cref="Entity"/> is removed from this collection
        /// </summary>
        event EventHandler<EntityCollectionChangedEventArgs<TEntity>> EntityRemoved;
    }
}