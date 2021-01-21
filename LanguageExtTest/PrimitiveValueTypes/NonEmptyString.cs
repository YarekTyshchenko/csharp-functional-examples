namespace LanguageExtTest.PrimitiveValueTypes
{
    using System;

    public record NonEmptyString
    {
        private readonly string _value;

        public NonEmptyString(string value)
        {
            if (value == string.Empty)
            {
                throw new ArgumentOutOfRangeException(nameof(_value));
            }

            _value = value;
        }

        public static implicit operator string(NonEmptyString value) => value._value;
    }
}
