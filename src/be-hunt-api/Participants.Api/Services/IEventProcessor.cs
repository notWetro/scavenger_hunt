namespace Participants.Api.Services
{
    public interface IEventProcessor
    {
        /// <summary>
        /// Processes the specified event message.
        /// </summary>
        /// <param name="message">The event message to process.</param>
        void ProcessEvent(string message);
    }
}
