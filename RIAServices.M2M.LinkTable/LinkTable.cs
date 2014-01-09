using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace RIAServices.M2M
{
    /// <summary>
    ///   Generic link table class that is used by M2M4RIA.
    /// </summary>
    /// <typeparam name="TObject1"> </typeparam>
    /// <typeparam name="TObject2"> </typeparam>
    public abstract class LinkTable<TObject1, TObject2>
        where TObject1 : class where TObject2 : class
    {
        #region Constants and Fields

        private readonly Dictionary<string, object> _keys = new Dictionary<string, object>();

        private TObject1 _object1;

        private TObject2 _object2;

        #endregion

        #region Public Properties

        public TObject1 Object1
        {
            get { return _object1; }
            set
            {
                if(_object1 != value)
                {
                    _object1 = value;
                    SetKeyValuesForEntity(x => x.Object1);
                }
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public Guid Object1_Guid_Id0
        {
            get { return GetKey(x => x.Object1_Guid_Id0); }
            set { SetKey(x => x.Object1_Guid_Id0, value); }
        }
        [EditorBrowsable(EditorBrowsableState.Never)]
        public Guid Object1_Guid_Id1
        {
            get { return GetKey(x => x.Object1_Guid_Id1); }
            set { SetKey(x => x.Object1_Guid_Id1, value); }
        }
        [EditorBrowsable(EditorBrowsableState.Never)]
        public Guid Object1_Guid_Id2
        {
            get { return GetKey(x => x.Object1_Guid_Id2); }
            set { SetKey(x => x.Object1_Guid_Id2, value); }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public short Object1_Int16_Id0
        {
            get { return GetKey(x => x.Object1_Int16_Id0); }
            set { SetKey(x => x.Object1_Int16_Id0, value); }
        }
        [EditorBrowsable(EditorBrowsableState.Never)]
        public short Object1_Int16_Id1
        {
            get { return GetKey(x => x.Object1_Int16_Id1); }
            set { SetKey(x => x.Object1_Int16_Id1, value); }
        }
        [EditorBrowsable(EditorBrowsableState.Never)]
        public short Object1_Int16_Id2
        {
            get { return GetKey(x => x.Object1_Int16_Id2); }
            set { SetKey(x => x.Object1_Int16_Id2, value); }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public int Object1_Int32_Id0
        {
            get { return GetKey(x => x.Object1_Int32_Id0); }
            set { SetKey(x => x.Object1_Int32_Id0, value); }
        }
        [EditorBrowsable(EditorBrowsableState.Never)]
        public int Object1_Int32_Id1
        {
            get { return GetKey(x => x.Object1_Int32_Id1); }
            set { SetKey(x => x.Object1_Int32_Id1, value); }
        }
        [EditorBrowsable(EditorBrowsableState.Never)]
        public int Object1_Int32_Id2
        {
            get { return GetKey(x => x.Object1_Int32_Id2); }
            set { SetKey(x => x.Object1_Int32_Id2, value); }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public long Object1_Int64_Id0
        {
            get { return GetKey(x => x.Object1_Int64_Id0); }
            set { SetKey(x => x.Object1_Int64_Id0, value); }
        }
        [EditorBrowsable(EditorBrowsableState.Never)]
        public long Object1_Int64_Id1
        {
            get { return GetKey(x => x.Object1_Int64_Id1); }
            set { SetKey(x => x.Object1_Int64_Id1, value); }
        }
        [EditorBrowsable(EditorBrowsableState.Never)]
        public long Object1_Int64_Id2
        {
            get { return GetKey(x => x.Object1_Int64_Id2); }
            set { SetKey(x => x.Object1_Int64_Id2, value); }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Object1_String_Id0
        {
            get { return GetKey(x => x.Object1_String_Id0); }
            set { SetKey(x => x.Object1_String_Id0, value); }
        }
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Object1_String_Id1
        {
            get { return GetKey(x => x.Object1_String_Id1); }
            set { SetKey(x => x.Object1_String_Id1, value); }
        }
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Object1_String_Id2
        {
            get { return GetKey(x => x.Object1_String_Id2); }
            set { SetKey(x => x.Object1_String_Id2, value); }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public DateTime Object1_DateTime_Id0
        {
            get { return GetKey(x => x.Object1_DateTime_Id0); }
            set { SetKey(x => x.Object1_DateTime_Id0, value); }
        }
        [EditorBrowsable(EditorBrowsableState.Never)]
        public DateTime Object1_DateTime_Id1
        {
            get { return GetKey(x => x.Object1_DateTime_Id1); }
            set { SetKey(x => x.Object1_DateTime_Id1, value); }
        }
        [EditorBrowsable(EditorBrowsableState.Never)]
        public DateTime Object1_DateTime_Id2
        {
            get { return GetKey(x => x.Object1_DateTime_Id2); }
            set { SetKey(x => x.Object1_DateTime_Id2, value); }
        }


        public TObject2 Object2
        {
            get { return _object2; }
            set
            {
                if(_object2 != value)
                {
                    _object2 = value;
                    SetKeyValuesForEntity(x => x.Object2);
                }
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public Guid Object2_Guid_Id0
        {
            get { return GetKey(x => x.Object2_Guid_Id0); }
            set { SetKey(x => x.Object2_Guid_Id0, value); }
        }
        [EditorBrowsable(EditorBrowsableState.Never)]
        public Guid Object2_Guid_Id1
        {
            get { return GetKey(x => x.Object2_Guid_Id1); }
            set { SetKey(x => x.Object2_Guid_Id1, value); }
        }
        [EditorBrowsable(EditorBrowsableState.Never)]
        public Guid Object2_Guid_Id2
        {
            get { return GetKey(x => x.Object2_Guid_Id2); }
            set { SetKey(x => x.Object2_Guid_Id2, value); }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public short Object2_Int16_Id0
        {
            get { return GetKey(x => x.Object2_Int16_Id0); }
            set { SetKey(x => x.Object2_Int16_Id0, value); }
        }
        [EditorBrowsable(EditorBrowsableState.Never)]
        public short Object2_Int16_Id1
        {
            get { return GetKey(x => x.Object2_Int16_Id1); }
            set { SetKey(x => x.Object2_Int16_Id1, value); }
        }
        [EditorBrowsable(EditorBrowsableState.Never)]
        public short Object2_Int16_Id2
        {
            get { return GetKey(x => x.Object2_Int16_Id2); }
            set { SetKey(x => x.Object2_Int16_Id2, value); }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public int Object2_Int32_Id0
        {
            get { return GetKey(x => x.Object2_Int32_Id0); }
            set { SetKey(x => x.Object2_Int32_Id0, value); }
        }
        [EditorBrowsable(EditorBrowsableState.Never)]
        public int Object2_Int32_Id1
        {
            get { return GetKey(x => x.Object2_Int32_Id1); }
            set { SetKey(x => x.Object2_Int32_Id1, value); }
        }
        [EditorBrowsable(EditorBrowsableState.Never)]
        public int Object2_Int32_Id2
        {
            get { return GetKey(x => x.Object2_Int32_Id2); }
            set { SetKey(x => x.Object2_Int32_Id2, value); }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public long Object2_Int64_Id0
        {
            get { return GetKey(x => x.Object2_Int64_Id0); }
            set { SetKey(x => x.Object2_Int64_Id0, value); }
        }
        [EditorBrowsable(EditorBrowsableState.Never)]
        public long Object2_Int64_Id1
        {
            get { return GetKey(x => x.Object2_Int64_Id1); }
            set { SetKey(x => x.Object2_Int64_Id1, value); }
        }
        [EditorBrowsable(EditorBrowsableState.Never)]
        public long Object2_Int64_Id2
        {
            get { return GetKey(x => x.Object2_Int64_Id2); }
            set { SetKey(x => x.Object2_Int64_Id2, value); }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Object2_String_Id0
        {
            get { return GetKey(x => x.Object2_String_Id0); }
            set { SetKey(x => x.Object2_String_Id0, value); }
        }
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Object2_String_Id1
        {
            get { return GetKey(x => x.Object2_String_Id1); }
            set { SetKey(x => x.Object2_String_Id1, value); }
        }
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Object2_String_Id2
        {
            get { return GetKey(x => x.Object2_String_Id2); }
            set { SetKey(x => x.Object2_String_Id2, value); }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public DateTime Object2_DateTime_Id0
        {
            get { return GetKey(x => x.Object2_DateTime_Id0); }
            set { SetKey(x => x.Object2_DateTime_Id0, value); }
        }
        [EditorBrowsable(EditorBrowsableState.Never)]
        public DateTime Object2_DateTime_Id1
        {
            get { return GetKey(x => x.Object2_DateTime_Id1); }
            set { SetKey(x => x.Object2_DateTime_Id1, value); }
        }
        [EditorBrowsable(EditorBrowsableState.Never)]
        public DateTime Object2_DateTime_Id2
        {
            get { return GetKey(x => x.Object2_DateTime_Id2); }
            set { SetKey(x => x.Object2_DateTime_Id2, value); }
        }

        #endregion

        #region Methods

        private static PropertyInfo GetMember<T>(Expression<Func<LinkTable<TObject1, TObject2>, T>> propertySelector)
            where T : class
        {
            var expression = propertySelector.Body as MemberExpression
                             ?? ((UnaryExpression)propertySelector.Body).Operand as MemberExpression;
            if(expression == null)
            {
                throw new ArgumentNullException("propertySelector");
            }
            return (PropertyInfo)expression.Member;
        }

        private static AssociationAttribute GetAssociationAttribute(PropertyDescriptor propDescriptor)
        {
            return (AssociationAttribute)propDescriptor.Attributes[typeof(AssociationAttribute)];
        }

        /// <summary>
        ///   The foreign key values of a link table entity are not automatically set/updated when the corresponding entities 
        ///   are new and have server-generated keys. Once these entities have been saved to the database they get id's generated 
        ///   by the database, which should be populated to the link table entities as well, such that the correct values are sent
        ///   to the RIA services client. The trick is that when a foreign key property of a link table entity is accessed, we try
        ///   to obtain its value from the associated entity. Amongst others, the foreign key properties are accessed when serializing
        ///   a link table entity. Therefore, with this trick, we're sure that always the correct values are transmitted to the
        ///   RiA services client.
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="propertySelector"> </param>
        /// <returns> </returns>
        private T GetKey<T>(Expression<Func<LinkTable<TObject1, TObject2>, T>> propertySelector)
        {
            var foreignKeyName = ((MemberExpression)propertySelector.Body).Member.Name;
            var foreignKeyValue = default(T);
            if(_keys.ContainsKey(foreignKeyName))
            {
                foreignKeyValue = (T)_keys[foreignKeyName];
            }
            var primaryKeyValue = GetPrimaryKeyValue(propertySelector);

            if(IsDefault(primaryKeyValue) == false && primaryKeyValue.Equals(foreignKeyValue) == false &&
               foreignKeyValue.Equals(default(T)))
            {
                foreignKeyValue = primaryKeyValue;
                _keys[foreignKeyName] = primaryKeyValue;
            }
            return foreignKeyValue;
        }
        /// <summary>
        /// This method checks if a given value is null or equals default(T). We have to distinguish whether T is a value type or a reference type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        private static bool IsDefault<T>(T value)
        {
            var valueTypeValue = value as ValueType;
            if(valueTypeValue != null)
            {
                return valueTypeValue.Equals(default(T));
            }
            return (object)value == null;
        }

        private PropertyDescriptor GetNavigationProperty(string foreignKeyName)
        {
            var thisType = GetType();
            var propertyDescriptor =
                from property in TypeDescriptor.GetProperties(thisType).OfType<PropertyDescriptor>()
                let association = (AssociationAttribute) property.Attributes[typeof(AssociationAttribute)]
                where association != null
                where association.ThisKeyMembers.Contains(foreignKeyName)
                select property;
            return propertyDescriptor.SingleOrDefault();
        }

        /// <summary>
        ///   Returns the primary key primaryKeyValue that corresponds to the given foreignKeySelector
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="foreignKeySelector"> </param>
        /// <returns> </returns>
        private T GetPrimaryKeyValue<T>(Expression<Func<LinkTable<TObject1, TObject2>, T>> foreignKeySelector)
        {
            var foreignKeyName = ((MemberExpression) foreignKeySelector.Body).Member.Name;
            var primaryKeyValue = default(T);
            var thisType = GetType();

            var navigationPropDescr = GetNavigationProperty(foreignKeyName);
            var navigationPropInfo = thisType.GetProperty(navigationPropDescr.Name);

            var associatedEntity = navigationPropInfo.GetValue(this, null);
            var association = GetAssociationAttribute(navigationPropDescr);
            var foreignKeyIndex = association.ThisKeyMembers.ToList().IndexOf(foreignKeyName);

            var primaryKeyName = association.OtherKeyMembers.ElementAt(foreignKeyIndex);
            if(associatedEntity != null)
            {
                var primaryKeyPropertyInfo = navigationPropInfo.PropertyType.GetProperty(primaryKeyName);
                primaryKeyValue = (T) primaryKeyPropertyInfo.GetValue(associatedEntity, null);
            }
            return primaryKeyValue;
        }

        private void SetKey<T>(Expression<Func<LinkTable<TObject1, TObject2>, T>> property, T value)
        {
            var keyName = ((MemberExpression) property.Body).Member.Name;
            _keys[keyName] = value;
        }

        private void SetKeyValuesForEntity<T>(Expression<Func<LinkTable<TObject1, TObject2>, T>> propertySelector)
            where T : class
        {
            var thisType = GetType();
            var otherType = typeof(T);
            var property = GetMember(propertySelector);
            var propertyName = property.Name;

            var entity = property.GetValue(this, null);

            var propertyDescriptor = TypeDescriptor.GetProperties(thisType)[propertyName];
            var association = (AssociationAttribute) propertyDescriptor.Attributes[typeof(AssociationAttribute)];
            var thisKeys = association.ThisKeyMembers.ToArray();
            var otherKeys = association.OtherKeyMembers.ToArray();

            for(var i = 0; i < thisKeys.Count(); i++)
            {
                var thisProperty = thisType.GetProperty(thisKeys[i]);
                var otherProperty = otherType.GetProperty(otherKeys[i]);
                var otherValue = otherProperty.GetValue(entity, null);
                thisProperty.SetValue(this, otherValue, null);
            }
        }

        #endregion
    }
}