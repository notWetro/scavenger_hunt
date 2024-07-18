namespace Participants.Api.Services
{
    public interface IEventProcessor
    {
        public void ProcessEvent(string message);
    }
}
