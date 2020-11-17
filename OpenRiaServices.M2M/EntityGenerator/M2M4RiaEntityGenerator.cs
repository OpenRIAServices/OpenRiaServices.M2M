using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using OpenRiaServices.Tools.TextTemplate.CSharpGenerators;

namespace OpenRiaServices.M2M.EntityGenerator
{
    public class M2M4RiaEntityGenerator : CSharpEntityGenerator
    {
        #region Methods

        protected override void GenerateClassDeclaration()
        {
            base.GenerateClassDeclaration();
            MakeLinkTableEntityAnIExtendedEntity(Type);
        }

        protected override void GenerateCustomMethods()
        {
            base.GenerateCustomMethods();
            AddRemoveEntityMethod(Type);
            AddInsertEntityMethod(Type);
            AddAttachMethodsToLinkTableEntity(Type);
        }

        protected override void GenerateProperties()
        {
            base.GenerateProperties();
            AddIExtendedEntityImplementation(Type);
            AddM2MProperties(Type);
        }

        private static string GetLinkTableOtherNavigationPropertyName(
            AssociationAttribute association, Type linkTableType)
        {
            var properties =
                from p in TypeDescriptor.GetProperties(linkTableType).OfType<PropertyDescriptor>()
                let x = p.Attributes[typeof(AssociationAttribute)] as AssociationAttribute
                where x != null
                where x.Name != association.Name
                select p;
            return properties.Single().Name;
        }

        private static IEnumerable<PropertyDescriptor> GetLinkTableViewProperties(Type type)
        {
            var props =
                TypeDescriptor.GetProperties(type).OfType<PropertyDescriptor>().Where(x => x.ComponentType == type);
            return props.Where(p => p.Attributes.OfType<LinkTableViewAttribute>().Any());
        }

        private static IEnumerable<PropertyDescriptor> GetNavigationProperties(Type type)
        {
            var props = TypeDescriptor.GetProperties(type).OfType<PropertyDescriptor>();
            return props.Where(p => p.Attributes.OfType<AssociationAttribute>().Any());
        }

        protected static bool IsLinkTableEntity(Type type)
        {
            // Find generic base type
            while(type != null && type.IsGenericType == false)
            {
                type = type.BaseType;
            }
            // Return false if no generic base type was found.
            if(type == null)
            {
                return false;
            }
            // Check if the generic type definition is assignable from LinkTable<,>
            return type.GetGenericTypeDefinition().IsAssignableFrom(typeof(LinkTable<,>));
        }

        private static string ToLowerInitial(string s)
        {
            return s[0].ToString(CultureInfo.InvariantCulture).ToLowerInvariant() + s.Substring(1);
        }

        public void AddAttachMethod(Type linkTableType, AssociationAttribute assocAttrThisEnd, AssociationAttribute assocAttrOtherEnd, PropertyDescriptor prop1, PropertyDescriptor prop2)
        {
            var linkTableFullTypeName = linkTableType.FullName;
            var navProp1TypeFullName = prop1.PropertyType.FullName;
            var navProp2TypeName = prop2.PropertyType.Name;
            var navProp2TypeFullName = prop2.PropertyType.FullName;
            var navProp1NameLower = ToLowerInitial(prop1.Name);
            var navProp2NameLower = ToLowerInitial(prop2.Name);
            var thisKeyMembers = assocAttrThisEnd.ThisKeyMembers.ToList();
            var otherKeyMembers = assocAttrThisEnd.OtherKeyMembers.ToList();

            WriteLine(@"#region Lines added by m2m4ria code generator");
            WriteLine(@"/// <summary>");
            WriteLine(
                @"/// This method attaches {0} and {1} to the current link table entity, in such a way",
                navProp1TypeFullName,
                navProp2TypeFullName);
            WriteLine(@"/// that both navigation properties are set before an INotifyCollectionChanged event is fired.");
            WriteLine(@"/// </summary>");
            WriteLine(@"/// <param name=""r""/>");
            WriteLine(@"/// <param name=""{0}""/>", navProp1NameLower);
            WriteLine(@"/// <param name=""{0}""/>", navProp2NameLower);
            WriteLine(
                @"[System.ObsoleteAttribute(""This property is only intended for use by the M2M4Ria solution."")]");
            WriteLine(
                @"public static void Attach{0}To{1}({2} r, {3} {4}, {5} {6})",
                navProp2TypeName,
                assocAttrOtherEnd.Name,
                linkTableFullTypeName,
                navProp1TypeFullName,
                navProp1NameLower,
                navProp2TypeFullName,
                navProp2NameLower);
            WriteLine(@"{");
            WriteLine(@"    var dummy = r.{0}; // this is to instantiate the EntityRef<{0}>", prop2.Name);
            WriteLine(@"    r._{0}.Entity = {0};", navProp2NameLower);

            for(var i = 0; i < thisKeyMembers.Count(); i++)
            {
                var thisKeyLower = ToLowerInitial(thisKeyMembers[i]);
                WriteLine(@"    r._{0} = {1}.{2};", thisKeyLower, navProp2NameLower, otherKeyMembers[i]);
            }
            WriteLine(@"    r.{0} = {1};", prop1.Name, navProp1NameLower);
            WriteLine(@"    r._{0}.Entity = null;", navProp2NameLower);

            for(var i = 0; i < thisKeyMembers.Count(); i++)
            {
                var thisKeyLower = ToLowerInitial(thisKeyMembers[i]);
                var keyType = linkTableType.GetProperty(thisKeyMembers[i]).PropertyType;
                WriteLine(@"    r._{0} = default({1});", thisKeyLower, keyType.FullName);
            }

            WriteLine(@"    r.{0} = {1};", prop2.Name, navProp2NameLower);
            WriteLine(@"}");
            WriteLine(@"#endregion");
        }

        public void AddAttachMethodsToLinkTableEntity(Type type)
        {
            if(!IsLinkTableEntity(type)) return;
            var propertyDescriptors = GetNavigationProperties(type).ToList();
            if(propertyDescriptors.Count() != 2)
            {
                throw new Exception(
                    String.Format(
                        "Currently m2m4ria requires link table entities with exactly two navigation properties. (found: {0} for {1})",
                        propertyDescriptors.Count(), type.FullName));
            }
            var assocAttr0 = propertyDescriptors[0].Attributes.OfType<AssociationAttribute>().Single();
            var assocAttr1 = propertyDescriptors[1].Attributes.OfType<AssociationAttribute>().Single();
            AddAttachMethod(type, assocAttr1, assocAttr0, propertyDescriptors[0], propertyDescriptors[1]);
            AddAttachMethod(type, assocAttr0, assocAttr1, propertyDescriptors[1], propertyDescriptors[0]);
        }

        public void AddIExtendedEntityImplementation(Type type)
        {
            if(!IsLinkTableEntity(type)) return;
            WriteLine(@"#region Lines added by m2m4ria code generator");
            WriteLine(@"/// <summary>");
            WriteLine(@"/// Gets the EntitySet the link table entity is contained in.");
            WriteLine(@"/// </summary>");
            WriteLine(
                @"OpenRiaServices.Client.EntitySet OpenRiaServices.M2M.IExtendedEntity.EntitySet");
            WriteLine(@"{");
            WriteLine(@"    get");
            WriteLine(@"    {");
            WriteLine(@"        return EntitySet;");
            WriteLine(@"    }");
            WriteLine(@"}");
            WriteLine(@"#endregion");
        }

        public void AddInsertEntityMethod(Type type)
        {
            foreach(var propertyDescriptor in GetLinkTableViewProperties(type))
            {
                var linkTableViewAttribute =
                    propertyDescriptor.Attributes.OfType<LinkTableViewAttribute>().Single();
                var linkTableTypeFullName = linkTableViewAttribute.LinkTableType.FullName;
                var elementTypeName = linkTableViewAttribute.ElementType.Name;
                var elementTypeFullName = linkTableViewAttribute.ElementType.FullName;
                var attachMethodName = "Attach" + elementTypeName + "To" + linkTableViewAttribute.OtherEndAssociationName;

                WriteLine(@"#region Lines added by m2m4ria code generator");
                WriteLine(
                    @"// Instruct compiler not to warn about usage of obsolete members, because using them is intended.");
                WriteLine(@"#pragma warning disable 618");
                WriteLine(
                    @"private void Add{0}({1} {2})", propertyDescriptor.Name, elementTypeFullName, ToLowerInitial(elementTypeName));
                WriteLine(@"{");
                WriteLine(@"    var newLinkTableEntity = new {0}();", linkTableTypeFullName);
                WriteLine(
                    @"    {0}.{1}(newLinkTableEntity, this, {2});",
                    linkTableTypeFullName,
                    attachMethodName,
                    ToLowerInitial(elementTypeName));
                WriteLine(@"}");
                WriteLine(@"#pragma warning restore 618");
                WriteLine(@"#endregion");
            }
        }

        public void AddM2MProperties(Type type)
        {
            foreach(var propertyDescriptor in GetLinkTableViewProperties(type))
            {
                var linkTableViewAttribute =
                    propertyDescriptor.Attributes.OfType<LinkTableViewAttribute>().Single();
                var linkTableAssociationAttribute =
                    propertyDescriptor.Attributes.OfType<AssociationAttribute>().Single();
                var m2mPropertyName = linkTableViewAttribute.M2MPropertyName;
                var m2mPropertyNameLower = ToLowerInitial(m2mPropertyName);
                var elementTypeFullName = linkTableViewAttribute.ElementType.FullName;
                var linkTableTypeFullName = linkTableViewAttribute.LinkTableType.FullName;
                var linkTablePropertyName = propertyDescriptor.Name;
                var addMethodName = "Add" + propertyDescriptor.Name;
                var removeMethodName = "Remove" + propertyDescriptor.Name;
                var linkTableOtherNavigationPropertyName =
                    GetLinkTableOtherNavigationPropertyName(
                        linkTableAssociationAttribute, linkTableViewAttribute.LinkTableType);

                WriteLine(@"#region Lines added by m2m4ria code generator");
                WriteLine(@"//");
                WriteLine(
                    @"// Code relating to the managing of the '{0}' association from '{1}' to '{2}'",
                    linkTableTypeFullName,
                    type.FullName,
                    elementTypeFullName);
                
                WriteLine(@"//");
                WriteLine(
                    @"private OpenRiaServices.DomainServices.Client.IEntityCollection<{0}> _{1};", elementTypeFullName, m2mPropertyNameLower);
                WriteLine(@"/// <summary>");
                WriteLine(@"/// Gets the collection of associated <see cref=""{0}]""/> entities.", elementTypeFullName);
                WriteLine(@"/// </summary>");
                WriteLine(@"public OpenRiaServices.DomainServices.Client.IEntityCollection<{0}> {1}", elementTypeFullName, m2mPropertyName);
                WriteLine(@"{");
                WriteLine(@"    get");
                WriteLine(@"    {");
                WriteLine(@"        if(_{0} == null)", m2mPropertyNameLower);
                WriteLine(@"        {");
                WriteLine(
                    @"            _{0} = new OpenRiaServices.M2M.EntityCollection<{1}, {2}>(",
                    m2mPropertyNameLower,
                    linkTableTypeFullName,
                    elementTypeFullName);
                WriteLine(@"                this.{0},", linkTablePropertyName);
                WriteLine(@"                r => r.{0},", linkTableOtherNavigationPropertyName);
                WriteLine(@"                {0},", removeMethodName);
                WriteLine(@"                {0}", addMethodName);
                WriteLine(@"                );");
                WriteLine(@"        }");
                WriteLine(@"        return _{0};", m2mPropertyNameLower);
                WriteLine(@"    }");
                WriteLine(@"}");
                WriteLine(@"#endregion");
            }
        }

        public void AddRemoveEntityMethod(Type type)
        {
            foreach (var propertyDescriptor in GetLinkTableViewProperties(type))
            {
                var linkTableViewAttribute =
                    propertyDescriptor.Attributes.OfType<LinkTableViewAttribute>().Single();
                var linkTableTypeFullName = linkTableViewAttribute.LinkTableType.FullName;

                WriteLine(@"#region Lines added by m2m4ria code generator");
                WriteLine(@"//" + propertyDescriptor.Name);
                WriteLine(@"private void Remove{0}({1} r)", propertyDescriptor.Name, linkTableTypeFullName);
                WriteLine(@"{");
                WriteLine(@"    if(((OpenRiaServices.M2M.IExtendedEntity)r).EntitySet == null)");
                WriteLine(@"    {");
                WriteLine(@"        this.{0}.Remove(r);", propertyDescriptor.Name);
                WriteLine(@"    }");
                WriteLine(@"    else");
                WriteLine(@"    {");
                WriteLine(@"        ((OpenRiaServices.M2M.IExtendedEntity)r).EntitySet.Remove(r);");
                WriteLine(@"    }");
                WriteLine(@"}");
                WriteLine(@"#endregion");
            }
        }

        public void MakeLinkTableEntityAnIExtendedEntity(Type type)
        {
            if(!IsLinkTableEntity(type)) return;
            WriteLine(@"#region Lines added by m2m4ria code generator");
            WriteLine(@", OpenRiaServices.M2M.IExtendedEntity");
            WriteLine(@"#endregion");
        }

        #endregion
    }
}