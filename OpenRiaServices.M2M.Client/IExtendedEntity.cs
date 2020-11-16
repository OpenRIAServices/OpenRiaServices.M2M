using OpenRiaServices.Client;

namespace OpenRiaServices.M2M
{
    /// <summary>
    /// An interface for M2M relation entities
    /// </summary>
    public interface IExtendedEntity
    {
        /// <summary>
        /// The EntitySet this entity belongs to.
        /// </summary>
        EntitySet EntitySet { get; }
    }
}