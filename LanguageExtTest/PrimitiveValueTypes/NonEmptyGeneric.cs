// <copyright file="NonEmptyGeneric.cs" company="HARK">
// Copyright (c) HARK. All rights reserved.
// </copyright>

namespace LanguageExtTest.PrimitiveValueTypes
{
    using System;

    /// <summary>
    /// Primitive Generic type representing a non null or Empty value.
    /// </summary>
    /// <typeparam name="T">Base type</typeparam>
    public record NonEmptyGeneric<T>
    {
        private readonly T value;

        public NonEmptyGeneric(T value)
        {
            // For reference types default returns null, for value types it returns a default value
            // Boolean for example has false as default value.
            if (value == null || value.Equals(default(T)))
            {
                throw new ArgumentOutOfRangeException(nameof(value));
            }
            this.value = value;
        }

        public static implicit operator T(NonEmptyGeneric<T> value) => value.value;
    }
}
