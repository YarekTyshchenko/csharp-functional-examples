// <copyright file="NonEmptyStringTests.cs" company="HARK">
// Copyright (c) HARK. All rights reserved.
// </copyright>

namespace LanguageExtTest.Tests.PrimitiveValueTypes
{
    using System;
    using LanguageExtTest.PrimitiveValueTypes;
    using Xunit;

    public class NonEmptyStringTests
    {
        [Fact]
        public void Construct_String_WithValue()
        {
            var s = "=== string ===";
            var value = new BusinessStringValue(s);
            Assert.IsType<BusinessStringValue>(value);
            Assert.Equal(s, value);
        }

        [Fact]
        public void Construct_String_WithNull()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new BusinessStringValue(null!));
        }

        [Fact]
        public void Construct_String_WithEmpty()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new BusinessStringValue(string.Empty));
        }

        public record Foo(NonEmptyString Bar);

        [Fact]
        public void DeserializeNewtonsoft_String_WithValue()
        {
            var foo = Newtonsoft.Json.JsonConvert.DeserializeObject<Foo>(@"{""Bar"":""bar""}");
            Assert.NotNull(foo);
            Assert.IsType<NonEmptyString>(foo.Bar);
            Assert.Equal("bar", foo.Bar);
        }

        [Fact]
        public void DeserializeNewtonsoft_String_WithEmpty()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Newtonsoft.Json.JsonConvert.DeserializeObject<Foo>(@"{""Bar"":""""}"));
        }

        [Fact]
        public void DeserializeNewtonsoft_String_WithNull()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Newtonsoft.Json.JsonConvert.DeserializeObject<Foo>(@"{""Bar"":null}"));
        }
        
        [Fact]
        public void DeserializeSystem_String_WithValue()
        {
            var foo = System.Text.Json.JsonSerializer.Deserialize<Foo>(@"{""Bar"":""bar""}");
            Assert.NotNull(foo);
            Assert.IsType<NonEmptyString>(foo!.Bar);
            Assert.Equal("bar", foo.Bar);
        }

        [Fact]
        public void DeserializeSystem_String_WithEmpty()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => System.Text.Json.JsonSerializer.Deserialize<Foo>(@"{""Bar"":""""}"));
        }

        [Fact(Skip = "System Deserialization is broken")]
        public void DeserializeSystem_String_WithNull()
        {
            var a = System.Text.Json.JsonSerializer.Deserialize<Foo>(@"{""Bar"":null}");
            Assert.NotNull(a);
            Assert.NotNull(a!.Bar);
            
            // Assert.Throws<ArgumentOutOfRangeException>(() => System.Text.Json.JsonSerializer.Deserialize<Foo>(@"{""Bar"":null}"));
        }

        [Fact]
        public void SerializeNewtonsoft_String_WithValue()
        {
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(new Foo(new NonEmptyString("bar")));
            Assert.Equal(@"{""Bar"":""bar""}", json);
        }

        [Fact]
        public void SerializeSystem_String_WithValue()
        {
            var json = System.Text.Json.JsonSerializer.Serialize(new Foo(new NonEmptyString("bar")));
            Assert.Equal(@"{""Bar"":""bar""}", json);
        }
    }

    public record BusinessStringValue(string Value) : NonEmptyString(Value);
}
