
namespace ScavengerHunt.Domain.Entities
{
    public sealed class Station
    {
        public string Name { get; set; } = string.Empty;

        // Note: Instead of Latitude and Longitude we might use Points from
        // NetTopologySuite.Geometries.Point Namespace (for calculating distances, etc.)

        public double Latitude { get; set; }
        public double Longitude { get; set; }

        private List<TaskText> _textTasks;
        private List<TaskQuestionAnswer> _questionTasks;

        public IReadOnlyCollection<TaskText> TextTasks => _textTasks.AsReadOnly();
        public IReadOnlyCollection<TaskQuestionAnswer> QuestionTasks => _questionTasks.AsReadOnly();

        public bool _isDraft;

        public static Station NewStation() => new() { _isDraft = true };

        private Station()
        {
            _isDraft = false;
            _textTasks = [];
            _questionTasks = [];
        }

        public void AddTask(TaskBase task)
        {
            if (task is TaskQuestionAnswer taskQuestion)
            {
                _questionTasks.Add(taskQuestion);
            }
            else if (task is TaskText taskText)
            {
                _textTasks.Add(taskText);
            }
        }
    }
}
