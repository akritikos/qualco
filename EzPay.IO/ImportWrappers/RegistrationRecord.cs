#pragma warning disable 649

namespace EzPay.IO.ImportWrappers
{
    using System.Diagnostics.CodeAnalysis;

    using EzPay.IO.Interfaces;

    using FileHelpers;

    [SuppressMessage("ReSharper", "StyleCop.SA1600", Justification = "Fields are documented in respective classes")]
    [SuppressMessage("ReSharper", "StyleCop.SA1401", Justification = "Entity is meant for record parsing, access modifier is Public for FileHelpers interop on fields")]
    [IgnoreFirst, DelimitedRecord(";")]
    public class RegistrationRecord : IEntityRecord
    {
        public long Vat;

        public string NormalizedEmail;

        public string NormalizedUsername;

        public string PasswordHash;

        public string ConcurencyStamp;

        public string SecurityStamp;
    }
}
