using CustomPadWeb.Common.Enums.PadOptions;
using CustomPadWeb.Common.ViewModels;

namespace CustomPadWeb.Application
{
    public static class MapperExtentions
    {
        public static TDestination MapTo<TDestination>(this Enum source)
            where TDestination : struct, Enum
        {
            ArgumentNullException.ThrowIfNull(source, nameof(source));

            if (Enum.TryParse<TDestination>(source.ToString(), true, out var destination))
            {
                return destination;
            }

            throw new InvalidOperationException($"Mapping from {source.GetType().Name} to {typeof(TDestination).Name} failed.");
        }

        public static CustomPadViewModel MapToVm(this Domain.Entities.GamepadConfiguration source)
        {
            ArgumentNullException.ThrowIfNull(source, nameof(source));
            return new CustomPadViewModel
            {
                Id = source.Id,
                Name = source.Name,
                Description = source.Description,
                ABXYButtons = source.ABXYButtons.MapTo<ButtonType>(),
                AdditionalButtons = source.AdditionalButtons.MapTo<ButtonType>(),
                ConnectionType = source.ConnectionType.MapTo<ConnectionType>(),
                DPad = source.DPad.MapTo<DPadType>(),
                InputType = source.InputType.MapTo<InputType>(),
                Power = source.Power.MapTo<PowerOption>(),
                Sticks = source.Sticks.MapTo<StickType>(),
                Triggers = source.Triggers.MapTo<TriggerType>()
            };
        }

        public static IEnumerable<CustomPadViewModel> MapToVms(this IEnumerable<Domain.Entities.GamepadConfiguration> source)
        {
            ArgumentNullException.ThrowIfNull(source, nameof(source));
            foreach (var item in source)
            {
                yield return item.MapToVm();
            }
        }
    }
}
