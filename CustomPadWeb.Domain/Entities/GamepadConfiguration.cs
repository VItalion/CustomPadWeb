using CustomPadWeb.Domain.Abstractions;
using CustomPadWeb.Domain.Enums.PadOptions;

namespace CustomPadWeb.Domain.Entities
{
    public class GamepadConfiguration: Entity, IEntity
    {
        public required string Name { get; set; }
        public string Description { get; set; } = string.Empty;
        public ButtonType ABXYButtons { get; set; }
        public ButtonType AdditionalButtons { get; set; }
        public DPadType DPad { get; set; }
        public StickType Sticks { get; set; }
        public TriggerType Triggers { get; set; }
        public ConnectionType ConnectionType { get; set; }
        public InputType InputType { get; set; }
        public PowerOption Power { get; set; }
    }
}
