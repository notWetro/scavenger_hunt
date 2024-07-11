using Participants.Api.DTOs;
using System.Text.Json;

namespace Participants.Api.Services
{
    public class EventProcessor(IServiceScopeFactory scopeFactory) : IEventProcessor
    {
        private readonly IServiceScopeFactory scopeFactory = scopeFactory;

        public string ProcessEvent(string message)
        {
            Console.WriteLine(message);

            // TODO: Do stuff with the message.
            // Created => Add new KVP to Redis store or sth like that
            // Updated => Replace current KVP with new one + check for conflicts
            // Deleted => Remove current KVP + check for conflicts

            // TODO: THINGS TO CONSIDER for Updated/Deleted
            // 1) Participant has done assignment that now doesn't exist anymore => Set flag of participation to invalid, Controllers should return that too
            // 2) When a Hunt was deleted all Participations should be deleted aswell

            var evenType = DetermineEvent(message);
            switch (evenType)
            {
                case EventType.Undefined:
                    break;
                case EventType.HuntUpdated:
                    break;
                case EventType.HuntCreated:
                    break;
                case EventType.HuntDeleted:
                    break;
            }

            throw new NotImplementedException();
        }

        private static EventType DetermineEvent(string notificationMessage)
        {
            var evenType = JsonSerializer.Deserialize<GenericEventDto>(notificationMessage);

            return evenType?.Event switch
            {
                "Hunt_Updated" => EventType.HuntUpdated,
                "Hunt_Created" => EventType.HuntCreated,
                "Hunt_Deleted" => EventType.HuntDeleted,
                _ => EventType.Undefined
            };
        }
    }

    enum EventType
    {
        Undefined,
        HuntUpdated,
        HuntCreated,
        HuntDeleted,

    }
}
