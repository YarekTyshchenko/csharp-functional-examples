namespace LanguageExtTest.PrimitiveValueTypes
{
    using System;

    [Newtonsoft.Json.JsonConverter(typeof(NonEmptyNewtonsoftJsonConverter))]
    [System.Text.Json.Serialization.JsonConverter(typeof(NonEmptyStringJsonConverter))]
    public record NonEmptyString
    {
        private readonly string value;

        [System.Text.Json.Serialization.JsonConstructor]
        [Newtonsoft.Json.JsonConstructor]
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
