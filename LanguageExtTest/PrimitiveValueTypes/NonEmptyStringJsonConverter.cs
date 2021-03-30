// <copyright file="NonEmptyStringJsonConverter.cs" company="HARK">
// Copyright (c) HARK. All rights reserved.
// </copyright>

namespace LanguageExtTest.PrimitiveValueTypes
{
    using System;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    public class NonEmptyStringJsonConverter : JsonConverter<NonEmptyString>
    {
        /// <inheritdoc />
        public override NonEmptyString? Read(ref Utf8JsonReader reader, Type typeToConvert,
            JsonSerializerOptions options) =>
            new NonEmptyString(reader.GetString()!);

        /// <inheritdoc />
        public override void Write(Utf8JsonWriter writer, NonEmptyString value, JsonSerializerOptions options) =>
            writer.WriteStringValue(value);
    }
}
