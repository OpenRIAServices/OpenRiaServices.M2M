
namespace M2M4RiaDemo.Web.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Data.Objects.DataClasses;
    using System.Linq;
    using System.ServiceModel.DomainServices.Hosting;
    using System.ServiceModel.DomainServices.Server;


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
}
