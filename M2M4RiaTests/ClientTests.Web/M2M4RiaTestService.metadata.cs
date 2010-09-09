
namespace ClientTests.Web
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Data.Objects.DataClasses;
    using System.Linq;
    using System.ServiceModel.DomainServices.Hosting;
    using System.ServiceModel.DomainServices.Server;


    // The MetadataTypeAttribute identifies AnimalMetadata as the class
    // that carries additional metadata for the Animal class.
    [MetadataTypeAttribute(typeof(Animal.AnimalMetadata))]
    public partial class Animal
    {

        // This class allows you to attach custom attributes to properties
        // of the Animal class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class AnimalMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private AnimalMetadata()
            {
            }

            public int AnimalId { get; set; }

            public string Name { get; set; }

            public Owner Owner { get; set; }

            public Nullable<int> OwnerOwnerId { get; set; }

            public EntityCollection<Vet> Vets { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies ChewedShoeMetadata as the class
    // that carries additional metadata for the ChewedShoe class.
    [MetadataTypeAttribute(typeof(ChewedShoe.ChewedShoeMetadata))]
    public partial class ChewedShoe
    {

        // This class allows you to attach custom attributes to properties
        // of the ChewedShoe class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class ChewedShoeMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private ChewedShoeMetadata()
            {
            }

            public int ChewedShoeId { get; set; }

            public Dog Dog { get; set; }

            public int DogAnimalId { get; set; }

            public string Type { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies FireHydrantMetadata as the class
    // that carries additional metadata for the FireHydrant class.
    [MetadataTypeAttribute(typeof(FireHydrant.FireHydrantMetadata))]
    public partial class FireHydrant
    {

        // This class allows you to attach custom attributes to properties
        // of the FireHydrant class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class FireHydrantMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private FireHydrantMetadata()
            {
            }

            public int FireHydrantId { get; set; }

            public string Location { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies FoodMetadata as the class
    // that carries additional metadata for the Food class.
    [MetadataTypeAttribute(typeof(Food.FoodMetadata))]
    public partial class Food
    {

        // This class allows you to attach custom attributes to properties
        // of the Food class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class FoodMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private FoodMetadata()
            {
            }

            public int FoodId { get; set; }

            public string Name { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies OwnerMetadata as the class
    // that carries additional metadata for the Owner class.
    [MetadataTypeAttribute(typeof(Owner.OwnerMetadata))]
    public partial class Owner
    {

        // This class allows you to attach custom attributes to properties
        // of the Owner class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class OwnerMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private OwnerMetadata()
            {
            }

            public EntityCollection<Animal> Animals { get; set; }

            public string Name { get; set; }

            public int OwnerId { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies TrainerMetadata as the class
    // that carries additional metadata for the Trainer class.
    [MetadataTypeAttribute(typeof(Trainer.TrainerMetadata))]
    public partial class Trainer
    {

        // This class allows you to attach custom attributes to properties
        // of the Trainer class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class TrainerMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private TrainerMetadata()
            {
            }

            public EntityCollection<Dog> Dogs { get; set; }

            public string Name { get; set; }

            public int TrainerId { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies VetMetadata as the class
    // that carries additional metadata for the Vet class.
    [MetadataTypeAttribute(typeof(Vet.VetMetadata))]
    public partial class Vet
    {

        // This class allows you to attach custom attributes to properties
        // of the Vet class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class VetMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private VetMetadata()
            {
            }

            public EntityCollection<Animal> Animals { get; set; }

            public string Name { get; set; }

            public int VetId { get; set; }
        }
    }
}
