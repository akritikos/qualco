namespace EzPay.IO.ImportWrappers
{
    using System.Diagnostics.CodeAnalysis;

    using EzPay.IO.Interfaces;
    using EzPay.Model.Entities;

    using FileHelpers;

    [SuppressMessage("ReSharper", "StyleCop.SA1600", Justification = "Fields are documented in respective classes")]
    [SuppressMessage("ReSharper", "StyleCop.SA1401", Justification = "Entity is meant for record parsing, access modifier is Public for FileHelpers interop on fields")]
    [DelimitedRecord(";")]
    public class CitizenUpdateRecord : IEntityRecord
    {
        public long Id;

        public string FirstName;

        public string LastName;

        public string Email;

        public string Phone;

        public string Address;

        public string County;

        public string Username;

        public int AccessFailedCount = 0;

        public int EmailConfirmed = 1;

        public int LockoutEnabled = 0;

        public int PhoneNumberConfirmed = 1;

        public int TwoFactorEnabled = 0;

    }
}
