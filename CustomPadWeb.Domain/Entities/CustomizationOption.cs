using CustomPadWeb.Domain.Abstractions;

namespace CustomPadWeb.Domain.Entities
{
    public class CustomizationOption : IEntity
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public Guid OrderId { get; private set; }
        public Order? Order { get; private set; }
        public string Key { get; private set; } = string.Empty;
        public string Value { get; private set; } = string.Empty;


        protected CustomizationOption() { }


        public CustomizationOption(string key, string value)
        {
            Key = key;
            Value = value;
        }
    }
}
