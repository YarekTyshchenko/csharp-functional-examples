namespace LanguageExtTest.RecordsAndValues.Boilerplaty
{
    using System;

    public record Gateway
    {
        public Gateway(
            Guid Id,
            string OrganizationId)
        {
            this.Id = Id;
            this.OrganizationId = OrganizationId;
        }

        public Guid Id
        {
            get => this.Id;
            init
            {
                if (value == default)
                {
                    throw new ArgumentException(nameof(this.Id));
                }
            }
        }

        public string OrganizationId
        {
            get => this.OrganizationId;
            init
            {
                if (value == string.Empty)
                {
                    throw new ArgumentException(nameof(this.OrganizationId));
                }
            }
        }
    }
}
