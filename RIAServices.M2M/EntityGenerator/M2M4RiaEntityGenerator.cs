using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using Microsoft.ServiceModel.DomainServices.Tools.TextTemplate.CSharpGenerators;

namespace RIAServices.M2M.EntityGenerator
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
            var props = TypeDescriptor.GetProperties(type).OfType<PropertyDescriptor>().Where(x => x.ComponentType == type);
            return props.Where(p => p.Attributes.OfType<LinkTableViewAttribute>().Any());
        }

        private static IEnumerable<PropertyDescriptor> GetNavigationProperties(Type type)
        {
            var props = TypeDescriptor.GetProperties(type).OfType<PropertyDescriptor>();
            return props.Where(p => p.Attributes.OfType<AssociationAttribute>().Any());
        }

        private static bool IsLinkTableEntity(Type type)
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
            // Check if the generic type definition is asignable from LinkTable<,>
            return type.GetGenericTypeDefinition().IsAssignableFrom(typeof(LinkTable<,>));
        }

        private static string ToLowerInitial(string s)
        {
            return s[0].ToString(CultureInfo.InvariantCulture).ToLowerInvariant() + s.Substring(1);
        }

        private void AddAttachMethod(
            Type linkTableType, AssociationAttribute assocAttr, PropertyDescriptor prop1, PropertyDescriptor prop2)
        {
            var linkTableTypeName = linkTableType.Name;
            var navProp1TypeName = prop1.PropertyType.Name;
            var navProp2TypeName = prop2.PropertyType.Name;
            var navProp1NameLower = ToLowerInitial(prop1.Name);
            var navProp2NameLower = ToLowerInitial(prop2.Name);
            var thisKeyMembers = assocAttr.ThisKeyMembers.ToList();
            var otherKeyMembers = assocAttr.OtherKeyMembers.ToList();

            WriteLine(@"#region Lines added by m2m4ria code generator");
            WriteLine(@"/// <summary>");
            WriteLine(
                @"/// This method attaches {0} and {1} to the current link table entity, in such a way",
                navProp1TypeName,
                navProp2TypeName);
            WriteLine(@"/// that both navigation properties are set before an INotifyCollectionChanged event is fired.");
            WriteLine(@"/// </summary>");
            WriteLine(@"/// <param name=""r""/>");
            WriteLine(@"/// <param name=""{0}""/>", navProp1NameLower);
            WriteLine(@"/// <param name=""{0}""/>", navProp2NameLower);
            WriteLine(
                @"[System.ObsoleteAttribute(""This property is only intended for use by the M2M4Ria solution."")]");
            WriteLine(
                @"public static void Attach{3}To{1}({0} r, {1} {2}, {3} {4})",
                linkTableTypeName,
                navProp1TypeName,
                navProp1NameLower,
                navProp2TypeName,
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

        private void AddAttachMethodsToLinkTableEntity(Type type)
        {
            if(IsLinkTableEntity(type))
            {
                var propertyDescriptors = GetNavigationProperties(type).ToList();
                if(propertyDescriptors.Count() != 2)
                {
                    throw new Exception(
                        String.Format(
                            "Currently m2m4ria requires link table entities with exactly two navigation properties. (found: {0} for {1})",
                            propertyDescriptors.Count(), type.Name));
                }
                var assocAttr0 = propertyDescriptors[0].Attributes.OfType<AssociationAttribute>().Single();
                var assocAttr1 = propertyDescriptors[1].Attributes.OfType<AssociationAttribute>().Single();
                AddAttachMethod(type, assocAttr1, propertyDescriptors[0], propertyDescriptors[1]);
                AddAttachMethod(type, assocAttr0, propertyDescriptors[1], propertyDescriptors[0]);
            }
        }

        private void AddIExtendedEntityImplementation(Type type)
        {
            if(IsLinkTableEntity(type))
            {
                WriteLine(@"#region Lines added by m2m4ria code generator");
                WriteLine(@"/// <summary>");
                WriteLine(@"/// Gets the EntitySet the link table entity is contained in.");
                WriteLine(@"/// </summary>");
                WriteLine(
                    @"System.ServiceModel.DomainServices.Client.EntitySet RIAServices.M2M.IExtendedEntity.EntitySet");
                WriteLine(@"{");
                WriteLine(@"    get");
                WriteLine(@"    {");
                WriteLine(@"        return EntitySet;");
                WriteLine(@"    }");
                WriteLine(@"}");
                WriteLine(@"#endregion");
            }
        }

        private void AddInsertEntityMethod(Type type)
        {
            foreach(var propertyDescriptor in GetLinkTableViewProperties(type))
            {
                var linkTableViewAttribute =
                    propertyDescriptor.Attributes.OfType<LinkTableViewAttribute>().Single();
                var linkTableTypeName = linkTableViewAttribute.LinkTableType.Name;
                var elementTypeName = linkTableViewAttribute.ElementType.Name;
                var attachMethodName = "Attach" + elementTypeName + "To" + type.Name;

                WriteLine(@"#region Lines added by m2m4ria code generator");
                WriteLine(
                    @"// Instruct compiler not to warn about usage of obsolete members, because using them is intended.");
                WriteLine(@"#pragma warning disable 618");
                WriteLine(
                    @"private void Add{0}({1} {2})", linkTableTypeName, elementTypeName, ToLowerInitial(elementTypeName));
                WriteLine(@"{");
                WriteLine(@"    var newLinkTableEntity = new {0}();", linkTableTypeName);
                WriteLine(
                    @"    {0}.{1}(newLinkTableEntity, this, {2});",
                    linkTableTypeName,
                    attachMethodName,
                    ToLowerInitial(elementTypeName));
                WriteLine(@"}");
                WriteLine(@"#pragma warning restore 618");
                WriteLine(@"#endregion");
            }
        }

        private void AddM2MProperties(Type type)
        {
            foreach(var propertyDescriptor in GetLinkTableViewProperties(type))
            {
                var linkTableViewAttribute =
                    propertyDescriptor.Attributes.OfType<LinkTableViewAttribute>().Single();
                var linkTableAssociationAttribute =
                    propertyDescriptor.Attributes.OfType<AssociationAttribute>().Single();
                var m2mPropertyName = linkTableViewAttribute.M2MPropertyName;
                var m2mPropertyNameLower = ToLowerInitial(m2mPropertyName);
                var elementTypeName = linkTableViewAttribute.ElementType.Name;
                var linkTableTypeName = linkTableViewAttribute.LinkTableType.Name;
                var linkTablePropertyName = propertyDescriptor.Name;
                var addMethodName = "Add" + linkTableTypeName;
                var removeMethodName = "Remove" + linkTableTypeName;
                var linkTableOtherNavigationPropertyName =
                    GetLinkTableOtherNavigationPropertyName(
                        linkTableAssociationAttribute, linkTableViewAttribute.LinkTableType);

                WriteLine(@"#region Lines added by m2m4ria code generator");
                WriteLine(@"//");
                WriteLine(
                    @"// Code relating to the managing of the '{0}' association from '{1}' to '{2}'",
                    linkTableTypeName,
                    type.Name,
                    elementTypeName);
                WriteLine(@"//");
                WriteLine(
                    @"private RIAServices.M2M.IEntityCollection<{0}> _{1};", elementTypeName, m2mPropertyNameLower);
                WriteLine(@"/// <summary>");
                WriteLine(@"/// Gets the collection of associated <see cref=""{0}]""/> entities.", elementTypeName);
                WriteLine(@"/// </summary>");
                WriteLine(@"public RIAServices.M2M.IEntityCollection<{0}> {1}", elementTypeName, m2mPropertyName);
                WriteLine(@"{");
                WriteLine(@"    get");
                WriteLine(@"    {");
                WriteLine(@"        if(_{0} == null)", m2mPropertyNameLower);
                WriteLine(@"        {");
                WriteLine(
                    @"            _{0} = new RIAServices.M2M.EntityCollection<{1}, {2}>(",
                    m2mPropertyNameLower,
                    linkTableTypeName,
                    elementTypeName);
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

        private void AddRemoveEntityMethod(Type type)
        {
            foreach(var propertDescr in GetLinkTableViewProperties(type))
            {
                var linkTableViewAttribute =
                    propertDescr.Attributes.OfType<LinkTableViewAttribute>().Single();
                var linkTableTypeName = linkTableViewAttribute.LinkTableType.Name;

                WriteLine(@"#region Lines added by m2m4ria code generator");
                WriteLine(@"private void Remove{0}({0} r)", linkTableTypeName);
                WriteLine(@"{");
                WriteLine(@"    if(((RIAServices.M2M.IExtendedEntity)r).EntitySet == null)");
                WriteLine(@"    {");
                WriteLine(@"        this.{0}.Remove(r);", propertDescr.Name);
                WriteLine(@"    }");
                WriteLine(@"    else");
                WriteLine(@"    {");
                WriteLine(@"        ((RIAServices.M2M.IExtendedEntity)r).EntitySet.Remove(r);");
                WriteLine(@"    }");
                WriteLine(@"}");
                WriteLine(@"#endregion");
            }
        }

        private void MakeLinkTableEntityAnIExtendedEntity(Type type)
        {
            if(IsLinkTableEntity(type))
            {
                WriteLine(@"#region Lines added by m2m4ria code generator");
                WriteLine(@", RIAServices.M2M.IExtendedEntity");
                WriteLine(@"#endregion");
            }
        }

        #endregion
    }
}