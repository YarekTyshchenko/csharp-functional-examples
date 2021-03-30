// <copyright file="NonEmptyStringTests.cs" company="HARK">
// Copyright (c) HARK. All rights reserved.
// </copyright>

namespace LanguageExtTest.PrimitiveValueTypes
{
    using System;
    using LanguageExtTest.RecordsAndValues.Clean;
    using Toolkit.Core.Extensions;
    using Toolkit.Core.Logging;
    using Xunit;

    public class NonEmptyStringTests
    {
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

        [Fact]
        public void Construct_String_WithValue()
        {
            var s = "=== string ===";
            var value = new BusinessStringValue(s);
            Assert.IsType<BusinessStringValue>(value);
            Assert.Equal(s, value);
        }

        public record Foo(NonEmptyString Bar);

        [Fact]
        public void Cast_String_WithEmpty()
        {
            // NonEmptyString a = (NonEmptyString)"";
            // Logger.LogInformation(a);
            // var obj = JsonConvert.DeserializeObject<Foo?>(@"{""Bar"":""""}");
            // if (obj is not null)
            // {
            //     Assert.IsType<Guid>(obj.Id);
            //     Logger.LogInformation($"ID is {obj.Id}");
            // }
            //
            //var bar = obj.Bar;
            // Logger.LogInformation($"Obj is {obj}");
            // Assert.NotNull(obj);
            // var nes = obj!.Bar;
            // //Assert.Equal("a", nes);
            // Assert.IsType<NonEmptyString>(nes);

        }
    }

    public record BusinessStringValue(string Value) : NonEmptyString(Value);
}
