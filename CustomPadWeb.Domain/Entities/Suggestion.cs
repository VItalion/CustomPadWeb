using CustomPadWeb.Domain.Abstractions;

namespace CustomPadWeb.Domain.Entities
{
    public class Suggestion : IEntity
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public Guid? UserId { get; private set; }
        public string Title { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;


        protected Suggestion() { }


        public Suggestion(string title, string description, Guid? userId = null)
        {
            Title = title ?? throw new ArgumentNullException(nameof(title));
            Description = description ?? string.Empty;
            UserId = userId;
        }


        public void Update(string title, string description)
        {
            Title = title ?? Title;
            Description = description ?? Description;
        }
    }
}
