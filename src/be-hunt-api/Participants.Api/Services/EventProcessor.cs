using Participants.Api.DTOs;
using Participants.Api.DTOs.Events;
using Participants.Domain.Entities;
using Participants.Domain.Repositories;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace Participants.Api.Services
{
    public sealed class EventProcessor(ICache cache, IServiceProvider serviceProvider) : IEventProcessor
    {
        private readonly ICache _cache = cache;
        private readonly IServiceProvider _serviceProvider = serviceProvider;

        public void ProcessEvent(string message)
        {
            // Decode message that contains ä, ö, ü, etc.
            string decodedString = Regex.Unescape(message);
            //Console.WriteLine(decodedString);

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

                    var updatingHunt = _cache.GetHuntAsync(hunt.Id).Result;
                    if (updatingHunt is not null)
                    {
                        using (var scope = _serviceProvider.CreateScope())
                        {
                            var participationService = scope.ServiceProvider.GetService<IParticipationRepository>();
                            participationService?.MakeInvalidByHuntIdAsync(hunt.Id).Wait();
                        }
                    }

                    _cache.UpdateHuntAsync(hunt.Id, hunt.Title, hunt.Assignments);
                    break;

                case EventType.HuntCreated:
                    _cache.SaveHuntAsync(hunt);
                    break;

                case EventType.HuntDeleted:
                    _cache.DeleteHuntAsync(hunt.Id);
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var participationService = scope.ServiceProvider.GetService<IParticipationRepository>();
                        participationService?.DeleteByIdAsync(hunt.Id);
                    }
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
                Title = huntPublishDto.Title,
                Assignments = huntPublishDto.Assignments.Select(x => new Assignment
                {
                    Id = x.Id,
                    Hint = new Hint
                    {
                        HintType = x.Hint.HintType,
                        Data = x.Hint.Data,
                        AdditionalData = x.Hint.additionalData
                    },
                    Solution = new Solution
                    {
                        SolutionType = x.Solution.SolutionType,
                        Data = x.Solution.Data
                    }
                }).ToList()
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
