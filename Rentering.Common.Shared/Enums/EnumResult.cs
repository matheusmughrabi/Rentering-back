namespace Rentering.Common.Shared.Enums
{
    public class EnumResult<TEnum> where TEnum: System.Enum
    {
        public TEnum Value { get; set; }
        public string Description { get; set; }
    }
}
