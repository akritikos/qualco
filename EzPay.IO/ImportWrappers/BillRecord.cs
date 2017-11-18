namespace EzPay.IO.ImportWrappers
{
    using System.Diagnostics.CodeAnalysis;

    using EzPay.IO.Interfaces;

    using FileHelpers;
    using EzPay.Model.Entities;

    [SuppressMessage("ReSharper", "StyleCop.SA1600", Justification = "Fields are documented in respective classes")]
    [SuppressMessage("ReSharper", "StyleCop.SA1401", Justification = "Entity is meant for record parsing, access modifier is Public for FileHelpers interop on fields")]
    [DelimitedRecord(";")]
    public class BillRecord : IEntityRecord
    {
        public string Id;

        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal Amount;

        public long Citizen;

        public string Description;

        public string DueDate;
    }
}
