using System;
using System.Collections.Generic;
using System.Data.Objects.DataClasses;
using System.Linq;

namespace M2M.EF
{
    public class EntityCollection<JoinType, TEntity> : IEnumerable<JoinType>
        where JoinType : new()
        where TEntity : EntityObject
    {
        private EntityCollection<TEntity> collection;
        private Func<TEntity, JoinType> newJoinType;
        private Func<JoinType, TEntity> getEntity;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="collection">Entity collection that represents a m2m relation</param>
        /// <param name="newJoinType">The function used to create a new joint type entity and set both elements</param>
        public EntityCollection(EntityCollection<TEntity> collection,Func<TEntity, JoinType> newJoinType, Func<JoinType, TEntity> getEntity)
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