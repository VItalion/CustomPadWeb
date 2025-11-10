using CustomPadWeb.Common.Enums.PadOptions;

namespace CustomPadWeb.Common.ViewModels
{
    public class CustomPadViewModelBase
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
