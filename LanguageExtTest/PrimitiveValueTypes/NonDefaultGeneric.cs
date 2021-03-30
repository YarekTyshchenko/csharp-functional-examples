// <copyright file="NonEmptyGeneric.cs" company="HARK">
// Copyright (c) HARK. All rights reserved.
// </copyright>

namespace XScan.Domain.Primitives
{
    using System;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Primitive Generic type representing a non null or Empty value.
    /// </summary>
    /// <typeparam name="T">Value Type.</typeparam>
    public record NonDefaultGeneric<T>
    {
        private readonly T value;

        /// <summary>
        /// Initializes a new instance of the <see cref="NonDefaultGeneric{T}"/> class.
        /// </summary>
        /// <param name="value">Value.</param>
        [JsonConstructor]
        [Newtonsoft.Json.JsonConstructor]
        //[JsonConverter(typeof(NonDefaultGenericJsonConverter<T>))]
        public NonDefaultGeneric(T value)
        {
            // For reference types default returns null, for value types it returns a default value
            // Boolean for example has false as default value.
            if (value == null || value.Equals(default(T)))
            {
                throw new ArgumentOutOfRangeException(nameof(value));
            }

            this.value = value;
        }

        public static implicit operator T(NonDefaultGeneric<T> value) => value.value;

        public static explicit operator NonDefaultGeneric<T>(T value) => new(value);
    }
}
