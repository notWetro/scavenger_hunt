namespace Participants.Api.Services
{
    public interface IEventProcessor
    {
        public string ProcessEvent(string message);
    }
}
