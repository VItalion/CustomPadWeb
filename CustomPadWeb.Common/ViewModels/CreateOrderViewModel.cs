namespace CustomPadWeb.Common.ViewModels
{
    public class CreateOrderViewModel
    {
        public string CustomerEmail { get; set; } = default!;
        public string Notes { get; set; } = default!;
        public List<CustomizationOptionViewModel> Options { get; set; } = new();
    }

    public class CustomizationOptionViewModel
    {
        public string Name { get; set; } = default!;
        public string Value { get; set; } = default!;
    }
}
