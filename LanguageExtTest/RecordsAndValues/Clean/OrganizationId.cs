namespace LanguageExtTest.RecordsAndValues.Clean
{
    using LanguageExtTest.PrimitiveValueTypes;

    public record OrganizationId : NonEmptyString
    {
        public OrganizationId(string value) : base(value) {}
    }
}
