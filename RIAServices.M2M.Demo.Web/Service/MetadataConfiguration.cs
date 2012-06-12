namespace RIAServices.M2M.Demo.Web.Service
{
    using System.Web.DomainServices.FluentMetadata;

    using RIAServices.M2M.Configuration;
    using RIAServices.M2M.Demo.Web.Model;

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