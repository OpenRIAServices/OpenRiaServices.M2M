using OpenRiaServices.FluentMetadata;
using OpenRiaServices.M2M.Configuration;
using OpenRiaServices.M2M.Demo.Web.Model;

namespace OpenRiaServices.M2M.Demo.Web.Service
{
    public class MetadataConfiguration : IFluentMetadataConfiguration
    {
        #region Public Methods and Operators

        public void OnTypeCreation(MetadataContainer metadataContainer)
        {
            metadataContainer.Entity<Dog>().Projection(x => x.Trainers).M2M(
                x => x.DogTrainers, x => x.DogTrainers, x => x.Dogs);
        }

        #endregion
    }
}