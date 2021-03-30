// <copyright file="NonDefaultGenericJsonConverter.cs" company="HARK">
// Copyright (c) HARK. All rights reserved.
// </copyright>

namespace XScan.Domain.Primitives
{
    using System;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    public class NonDefaultGenericJsonConverter<T> : JsonConverter<NonDefaultGeneric<T>>
    {
        /// <inheritdoc />
        public override NonDefaultGeneric<T>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var s = reader.GetString();
            if (s is not null)
            {
                var a = JsonSerializer.Deserialize<T>(s);
                if (a is not null)
                {
                    return new NonDefaultGeneric<T>(a);
                }
            }

            throw new ArgumentNullException("Foo");
        }

        /// <inheritdoc />
        public override void Write(Utf8JsonWriter writer, NonDefaultGeneric<T> value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }

}
