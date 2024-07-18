using Hunts.Api.DTOs.Hunt;

namespace Hunts.Api.Services
{
    public interface IMessageBusClient
    {
        void PublishNewHunt(HuntPublishDto hunt);
        void PublishUpdatedHunt(HuntPublishDto hunt);
        void PublishDeletedHunt(HuntPublishDto hunt);
    }
}
