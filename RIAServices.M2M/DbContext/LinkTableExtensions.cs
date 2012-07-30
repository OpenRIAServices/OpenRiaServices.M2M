using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.ServiceModel.DomainServices.Server;

namespace RIAServices.M2M.DbContext
{
    public static class LinkTableExtensions
    {
        #region Public Methods and Operators       

        /// <summary>
        ///   Loads the m2m association between object1 and object2, by first detaching object1 from context then adding object2 to the TObject2 collection of object1 and finally attaching object1 to context again.
        /// </summary>
        /// <remarks>
        ///   The association between object1 and object2 is not automatically fetched from the database when the RIA Services delete method for TLinkTable is called. As a consequence,
        ///   the TObject2 collection of object1 is then empty, so removing object2 will have no effect. This method is to add the association to the DbContext without a roundtrip to the database.
        /// </remarks>
        /// <typeparam name="TObject1"> </typeparam>
        /// <typeparam name="TObject2"> </typeparam>
        /// <typeparam name="TLinkTable"> </typeparam>
        /// <param name="context"> </param>
        /// <param name="object1"> </param>
        /// <param name="object2"> </param>
        public static void LoadM2M<TObject1, TObject2, TLinkTable>(this System.Data.Entity.DbContext context,
                                                                   TObject1 object1, TObject2 object2)
            where TObject1 : class where TObject2 : class where TLinkTable : LinkTable<TObject1, TObject2>
        {
            context.Entry(object1).State = EntityState.Detached;

            var linkTableViewAttribute = GetLinkTableViewAttributeForObject1<TObject1, TObject2, TLinkTable>();
            var m2mPropInfo = object1.GetType().GetProperty(linkTableViewAttribute.M2MPropertyName);
            var m2mCollection = (ICollection<TObject2>) m2mPropInfo.GetValue(object1, null);
            m2mCollection.Add(object2);

            context.Set(typeof(TObject1)).Attach(object1);
        }

        private static LinkTableViewAttribute GetLinkTableViewAttributeForObject1<TObject1, TObject2, TLinkTable>()
            where TObject1 : class
            where TObject2 : class
            where TLinkTable : LinkTable<TObject1, TObject2>
        {
            var linkTablePropDescr = TypeDescriptor.GetProperties(typeof(TLinkTable))["Object1"];
            var linkTableAssoc = (AssociationAttribute) linkTablePropDescr.Attributes[typeof(AssociationAttribute)];
            var object1PropDescr =
                (from propDescr in TypeDescriptor.GetProperties(typeof(TObject1)).OfType<PropertyDescriptor>()
                 let object1Assoc = (AssociationAttribute) propDescr.Attributes[typeof(AssociationAttribute)]
                 where object1Assoc != null
                 where object1Assoc.Name == linkTableAssoc.Name
                 select propDescr).Single();
            var linkTableViewAttribute =
                (LinkTableViewAttribute) object1PropDescr.Attributes[typeof(LinkTableViewAttribute)];
            return linkTableViewAttribute;
        }

        public static TObject1 FetchObject1<TObject1, TObject2>(
            this LinkTable<TObject1, TObject2> linkTableEntity, ChangeSet changeSet,
            System.Data.Entity.DbContext dbContext)
            where TObject1 : class where TObject2 : class
        {
            var properties = TypeDescriptor.GetProperties(linkTableEntity.GetType());
            var navProp = properties["Object1"];
            var associationAttribute =
                navProp.Attributes.OfType<AssociationAttribute>().SingleOrDefault();

            return Find(
                linkTableEntity, linkTableEntity.Object1, changeSet, dbContext.Set<TObject1>(), associationAttribute);
        }

        public static TObject2 FetchObject2<TObject1, TObject2>(
            this LinkTable<TObject1, TObject2> linkTableEntity, ChangeSet changeSet,
            System.Data.Entity.DbContext dbContext)
            where TObject1 : class where TObject2 : class
        {
            var properties = TypeDescriptor.GetProperties(linkTableEntity.GetType());
            var navProp = properties["Object2"];
            var associationAttribute =
                navProp.Attributes.OfType<AssociationAttribute>().SingleOrDefault();

            return Find(
                linkTableEntity, linkTableEntity.Object2, changeSet, dbContext.Set<TObject2>(), associationAttribute);
        }

        #endregion

        #region Methods

        private static TType Find<TType>(
            object linkTableEntity,
            TType entityToFind,
            ChangeSet changeSet,
            IDbSet<TType> set,
            AssociationAttribute association) where TType : class
        {
            if(entityToFind != null)
            {
                return entityToFind;
            }
            var entity =
                changeSet.ChangeSetEntries.Select(cse => cse.Entity).OfType<TType>().SingleOrDefault(
                    e => Match(e, linkTableEntity, association));
            if(entity == null)
            {
                entity = set.Find(MakeKeyValues(linkTableEntity, association.ThisKeyMembers));
            }
            return entity;
        }

        private static object[] MakeKeyValues(object entity, IEnumerable<string> keys)
        {
            var type = entity.GetType();
            var keyValues = new List<object>();
            foreach(var key in keys)
            {
                var propInfo = type.GetProperty(key);
                keyValues.Add(propInfo.GetValue(entity, null));
            }
            return keyValues.ToArray();
        }

        private static bool Match<TType>(TType entity, object linkTableEntity, AssociationAttribute association)
        {
            var thisKeyValues = MakeKeyValues(linkTableEntity, association.ThisKeyMembers);
            var otherKeyValues = MakeKeyValues(entity, association.OtherKeyMembers);
            for(var i = 0; i < thisKeyValues.Count(); i++)
            {
                if(thisKeyValues[i] != otherKeyValues[i])
                {
                    return false;
                }
            }
            return true;
        }

        #endregion
    }
}