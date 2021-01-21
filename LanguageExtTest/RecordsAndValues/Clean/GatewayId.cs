namespace LanguageExtTest.RecordsAndValues.Clean
{
    using System;
    using LanguageExtTest.PrimitiveValueTypes;

    public record GatewayId : NonEmptyGuid
    {
        public GatewayId(Guid value) : base(value) {}
    }
}
