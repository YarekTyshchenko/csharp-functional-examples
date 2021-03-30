namespace LanguageExtTest.PrimitiveValueTypes
{
    using System;
    using Newtonsoft.Json;

    public class NonEmptyNewtonsoftJsonConverter : JsonConverter<NonEmptyString>
    {
        /// <inheritdoc />
        public override NonEmptyString ReadJson(JsonReader reader, Type objectType, NonEmptyString? existingValue, bool hasExistingValue,
            JsonSerializer serializer)
        {
            return new NonEmptyString(reader.Value?.ToString()!);
        }

        /// <inheritdoc />
        public override void WriteJson(JsonWriter writer, NonEmptyString? value, JsonSerializer serializer)
        {
            writer.WriteValue((string)value!);
        }
    }
}
