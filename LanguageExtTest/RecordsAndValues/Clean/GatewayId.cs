namespace LanguageExtTest.RecordsAndValues.Clean
{
    using System;
    using LanguageExtTest.PrimitiveValueTypes;

    public record GatewayId(Guid value) : NonEmptyGuid(value);
}
