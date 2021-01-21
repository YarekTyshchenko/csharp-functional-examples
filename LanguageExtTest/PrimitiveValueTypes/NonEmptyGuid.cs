namespace LanguageExtTest.PrimitiveValueTypes
{
    using System;

    public record NonEmptyGuid
    {
        private readonly Guid _value;

        public NonEmptyGuid(Guid value)
        {
            if (value == default)
            {
                throw new ArgumentOutOfRangeException(nameof(_value));
            }
            _value = value;
        }

        public static implicit operator Guid(NonEmptyGuid value) => value._value;
    }
}
