using System.ServiceModel.DomainServices.Client;

namespace RIAServices.M2M
{
    public interface IExtendedEntity
    {
        EntitySet EntitySet { get; }
    }
}