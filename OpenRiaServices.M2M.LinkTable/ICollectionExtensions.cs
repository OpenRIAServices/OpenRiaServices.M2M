using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace OpenRiaServices.M2M
{
    /// <summary>
    ///   Extension methods for ICollection
    /// </summary>
    public static class ICollectionExtensions
    {
        #region Public Methods and Operators
        /// <summary>
        /// Converts a collection of TObject2 owned by TObject1 to a collection of link table objects of type TLinktable
        /// </summary>
        /// <typeparam name="TObject1"></typeparam>
        /// <typeparam name="TObject2"></typeparam>
        /// <typeparam name="TLinkTable"></typeparam>
        /// <param name="collection"></param>
        /// <param name="owner"></param>
        /// <param name="view"></param>
        /// <returns></returns>
        public static ICollection<TLinkTable> ProjectObject1<TObject1, TObject2, TLinkTable>(
            this ICollection<TObject2> collection, TObject1 owner,
            Expression<Func<TObject1, IEnumerable<TLinkTable>>> view)
            where TObject1 : class
            where TObject2 : class
            where TLinkTable : LinkTable<TObject1, TObject2>, new()
        {
            if(collection == null)
            {
                return null;
            }
            Func<TObject2, TLinkTable> makeLinkTableEntity = x => new TLinkTable {Object1 = owner, Object2 = x};
            return collection.Select(makeLinkTableEntity).ToList();
        }

        /// <summary>
        /// Converts a collection of TObject1 owned by TObject2 to a collection of link table objects of type TLinktable
        /// </summary>
        /// <typeparam name="TObject1"></typeparam>
        /// <typeparam name="TObject2"></typeparam>
        /// <typeparam name="TLinkTable"></typeparam>
        /// <param name="collection"></param>
        /// <param name="owner"></param>
        /// <param name="view"></param>
        /// <returns></returns>
        public static ICollection<TLinkTable> ProjectObject2<TObject1, TObject2, TLinkTable>(
            this ICollection<TObject1> collection, TObject2 owner,
            Expression<Func<TObject2, IEnumerable<TLinkTable>>> view)
            where TObject1 : class
            where TObject2 : class
            where TLinkTable : LinkTable<TObject1, TObject2>, new()
        {
            if(collection == null)
            {
                return null;
            }
            Func<TObject1, TLinkTable> makeLinkTableEntity = x => new TLinkTable {Object2 = owner, Object1 = x};
            return collection.Select(makeLinkTableEntity).ToList();
        }

        #endregion
    }
}