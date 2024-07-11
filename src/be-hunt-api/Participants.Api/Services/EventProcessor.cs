using Participants.Api.DTOs;
using Participants.Api.DTOs.Hunt;
using Participants.Domain.Entities;
using System.Text.Json;

namespace Participants.Api.Services
{
    public class EventProcessor(ICache cache) : IEventProcessor
    {
        private readonly ICache _cache = cache;

        public void ProcessEvent(string message)
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
            var hunt = DetermineHunt(message);
            if (hunt is null)
                return;

            switch (evenType)
            {
                case EventType.Undefined:
                    Console.WriteLine("A non valid event was received. Discarding it...");
                    break;
                case EventType.HuntUpdated:

                    // TODO: Retrieve old hunt (if exists). Retrieve participations with said hunt.
                    // TODO: Check if a participation has done assignment that now doesn't exist anymore.
                    // TODO: Change status of a participation to "invalid" if so.

                    _cache.UpdateHuntAsync(hunt.Id, hunt.Assignments);
                    break;
                
                case EventType.HuntCreated:
                    _cache.SaveHuntAsync(hunt);
                    break;
                
                case EventType.HuntDeleted:

                    // TODO: Retrieve old hunt (if exists). Retrieve participations with said hunt.
                    // TODO: Change status of a participation to "invalid" (hunt doesn't exist anymore).

                    _cache.DeleteHuntAsync(hunt.Id);
                    break;
            }
        }

        private static Hunt? DetermineHunt(string message)
        {
            var huntPublishDto = JsonSerializer.Deserialize<HuntPublishDto>(message);

            if (huntPublishDto is null)
                return null;

            return new()
            {
                Id = huntPublishDto.Id,
                Assignments = [..huntPublishDto.Assignments.Select(assignment => assignment.Id)]
            };
        }

        private static EventType DetermineEvent(string message)
        {
            var evenType = JsonSerializer.Deserialize<GenericEventDto>(message);

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
