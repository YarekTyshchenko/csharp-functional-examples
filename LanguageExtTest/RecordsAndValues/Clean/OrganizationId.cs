namespace LanguageExtTest.RecordsAndValues.Clean
{
    using LanguageExtTest.PrimitiveValueTypes;

    public record OrganizationId(string Id) : NonEmptyString(Id);
}
