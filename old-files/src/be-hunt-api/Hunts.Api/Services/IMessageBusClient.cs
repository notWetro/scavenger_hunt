using Hunts.Api.DTOs.Hunt;

namespace Hunts.Api.Services
{
    /// <summary>
    /// Interface for a message bus client.
    /// </summary>
    public interface IMessageBusClient
    {
        /// <summary>
        /// Publishes a new hunt message to the RabbitMQ exchange.
        /// </summary>
        /// <param name="hunt">The hunt data transfer object.</param>
        void PublishNewHunt(HuntPublishDto hunt);

        /// <summary>
        /// Publishes an updated hunt message to the RabbitMQ exchange.
        /// </summary>
        /// <param name="hunt">The hunt data transfer object.</param>
        void PublishUpdatedHunt(HuntPublishDto hunt);

        /// <summary>
        /// Publishes a deleted hunt message to the RabbitMQ exchange.
        /// </summary>
        /// <param name="hunt">The hunt data transfer object.</param>
        void PublishDeletedHunt(HuntPublishDto hunt);
    }
}
