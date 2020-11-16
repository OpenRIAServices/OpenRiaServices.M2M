using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using OpenRiaServices.FluentMetadata;
using OpenRiaServices.M2M.Utilities;

namespace OpenRiaServices.M2M.Configuration
{
    /// <summary>
    /// Static Extension class for M2M Configuration expressions.
    /// </summary>
    public static class FluentConfigurationExpressions
    {
        #region Public Methods and Operators

        /// <summary>
        ///   Configures an m2m association for Open Ria Services
        /// </summary>
        /// <typeparam name="TObject1"> Entity type A. </typeparam>
        /// <typeparam name="TObject2"> Entity type B. </typeparam>
        /// <typeparam name="TLinkTable"> Link table entity that links A and B together. </typeparam>
        /// <param name="projection"> Projection to select m2m collection on entity A. </param>
        /// <param name="m2mView1"> Selector for link table view on m2m collection of entity A. </param>
        /// <param name="m2mView2"> Selector for link table view on m2m collection of entity B. </param>
        /// <param name="m2m2"> Selector for m2m collection on entity B. </param>
        /// <returns> </returns>
        public static M2M4RiaExpression<TObject1, TObject2, TLinkTable> M2M<TObject1, TObject2, TLinkTable>(
            this MemberProjectionCollectionMetadata<TObject1, TObject2> projection,
            Expression<Func<TObject1, ICollection<TLinkTable>>> m2mView1,
            Expression<Func<TObject2, ICollection<TLinkTable>>> m2mView2,
            Expression<Func<TObject2, ICollection<TObject1>>> m2m2) where TObject1 : class where TObject2 : class
            where TLinkTable : LinkTable<TObject1, TObject2>
        {
            return new M2M4RiaExpression<TObject1, TObject2, TLinkTable>(
                projection.Metadata.Container,
                projection.MemberName,
                m2mView1.GetProperty().Name,
                m2mView2.GetProperty().Name,
                m2m2.GetProperty().Name);
        }

        #endregion
    }
}