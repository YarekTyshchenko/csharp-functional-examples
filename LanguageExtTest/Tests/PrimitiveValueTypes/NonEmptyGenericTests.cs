// <copyright file="NonEmptyGenericTests.cs" company="HARK">
// Copyright (c) HARK. All rights reserved.
// </copyright>

namespace LanguageExtTest.Tests.PrimitiveValueTypes
{
    using System;
    using Newtonsoft.Json;
    using Toolkit.Core.Extensions;
    using Toolkit.Core.Logging;
    using XScan.Domain.Primitives;
    using Xunit;

    public class NonEmptyGenericTests
    {
        /**
        // Value type cannot be set to null
        [Fact]
        public void Construct_Guid_WithNull()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new NonEmptyGuid(null!));
        }
        */

        [Fact]
        public void Construct_Guid_WithEmpty()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new NonEmptyGuid(Guid.Empty));
        }

        [Fact]
        public void Construct_Guid_WithValue()
        {
            var guid = Guid.NewGuid();
            var value = new NonEmptyGuid(guid);
            Assert.IsType<NonEmptyGuid>(value);
            Assert.Equal(guid, value);
        }

        [Fact]
        public void Construct_String_WithNull()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new NonNullString(null!));
        }

        // String is a reference type so its default value is null
        [Fact]
        public void Construct_String_WithEmpty()
        {
            var value = new NonNullString(string.Empty);
            Assert.NotNull(value);
            Assert.Equal(string.Empty, value);
        }

        [Fact]
        public void Construct_String_WithValue()
        {
            var s = "=== string ===";
            var value = new NonNullString(s);
            Assert.IsType<NonNullString>(value);
            Assert.Equal(s, value);
        }

        [Fact]
        public void Construct_Bool_WithEmpty()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new NonDefaultBool(default));
        }

        [Fact]
        public void Construct_Bool_WithValue()
        {
            var boolean = true;
            var value = new NonDefaultBool(boolean);
            Assert.IsType<NonDefaultBool>(value);
            Assert.Equal(boolean, value);
        }

        private record Foo(
            NonEmptyGuid Id);

        [Fact(Skip = "C# Refuses to use cast operator or constructor when field is missing")]
        public void Cast_Guid_WithNull()
        {
            var json = @"{""Foo"":null}";
            var a = JsonConvert.DeserializeObject<Foo>(json, new JsonSerializerSettings()
            {
                //NullValueHandling = NullValueHandling.Include,
                //MissingMemberHandling = MissingMemberHandling.Error,
            });
            Logger.LogInformation($"A is : {a}");
            Assert.NotNull(a);
            Assert.IsType<NonDefaultGeneric<Guid>>(a!.Id);
            Assert.Throws<JsonSerializationException>(() => json.FromJson<Foo>());
        }

        [Fact]
        public void Cast_Guid_WithEmpty()
        {
            var json = @"{""Id"":""00000000-0000-0000-0000-000000000000""}";
            Assert.Throws<JsonSerializationException>(() => json.FromJson<Foo>());
        }

        [Fact]
        public void Cast_Guid_WithValue()
        {
            var guid = Guid.NewGuid();
            var json = $@"{{""Id"":""{guid}""}}";
            var a = json.FromJson<Foo>();
            Assert.NotNull(a);
            Assert.IsType<NonEmptyGuid>(a.Id);
            Assert.Equal(guid, a.Id);
        }

        // Value type
        private record NonEmptyGuid(Guid Value) : NonDefaultGeneric<Guid>(Value)
        {
            // Json Deserializer
            public static explicit operator NonEmptyGuid(string value) => new(Guid.Parse(value));
        }

        // Reference type
        private record NonNullString(string Value) : NonDefaultGeneric<string>(Value);

        // Value type
        private record NonDefaultBool(bool Value) : NonDefaultGeneric<bool>(Value);
    }
}
