namespace LanguageExtTest.PrimitiveValueTypes
{
    using System;

    public record NonEmptyString
    {
        private readonly string value;

        public NonEmptyString(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentOutOfRangeException(nameof(value));
            }

            this.value = value;
        }

        public static implicit operator string(NonEmptyString value) => value.value;
    }
}
