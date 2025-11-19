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
    }
}
