// <copyright file="RecordsAndValuesTest.cs" company="HARK">
// Copyright (c) HARK. All rights reserved.
// </copyright>

namespace LanguageExtTest.RecordsAndValues
{
    using System;
    using LanguageExtTest.PrimitiveValueTypes;
    using LanguageExtTest.RecordsAndValues.Clean;
    using Xunit;

    public class RecordsAndValuesTest
    {
        [Fact]
        public void CanCastToGuid()
        {
            var guid = Guid.NewGuid();
            var nonEmptyGuid = new NonEmptyGuid(guid);
            Assert.StrictEqual(nonEmptyGuid, guid);
            Guid plainGuid = nonEmptyGuid;
            Assert.StrictEqual(plainGuid, guid);
        }

        [Fact]
        public void ThrowsOnConstruction()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new NonEmptyGuid(Guid.Empty));
        }

        [Fact]
        public void CannotBeWithed()
        {
            var guid = new NonEmptyGuid(Guid.NewGuid());
            // Does not compile:
            // var b = guid with
            // {
            //     _value = Guid.Empty,
            // };
        }

        [Fact]
        public void CleanGateway()
        {
            var rawGuid = Guid.NewGuid();
            var gateway = new Gateway(new GatewayId(rawGuid), new OrganizationId("Foo"));
            Guid guid = gateway.Id;

            Assert.StrictEqual(rawGuid, guid);
        }

        [Fact]
        public void CleanGateway_CannotBeInvalid()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                new Gateway(new GatewayId(Guid.Empty), new OrganizationId("a")));
        }
    }
}
