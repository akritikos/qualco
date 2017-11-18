#pragma warning disable 649

namespace EzPay.IO.ImportWrappers
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;

    using EzPay.IO.Interfaces;
    using EzPay.Model.Entities;

    using FileHelpers;

    /// <summary>
    /// Wrapper class for parsing CSV records
    /// </summary>
    [IgnoreFirst, DelimitedRecord(";")]
    [SuppressMessage("ReSharper", "StyleCop.SA1600", Justification = "Fields are documented in respective classes")]
    public class DebtRecord : IEntityRecord
    {
        [FieldHidden]
        private Citizen c;

        [FieldHidden]
        private Bill b;

        #region Citizen
        [FieldNotEmpty]
        private long citizenVat;

        [FieldNotEmpty]
        private string citizenName;

        [FieldNotEmpty]
        private string citizenSurname;

        [FieldNotEmpty]
        private string citizenEmail;

        [FieldNotEmpty]
        private int citizenPhone;

        [FieldNotEmpty]
        private string citizenAddress;

        [FieldNotEmpty]
        private string citizenCounty;
        #endregion Citizen

        #region Bill
        [FieldNotEmpty]
        private string billId;

        [FieldNotEmpty]
        private string billDescription;

        [FieldNotEmpty]
        private string billAmount;

        [FieldNotEmpty]
        private string billDate;
        #endregion

        /// <summary>
        /// Parses data from this record into a new <see cref="Citizen"/> object
        /// </summary>
        /// <returns>Valid <see cref="Citizen"/> object</returns>
        public Citizen ParseCitizen() => c = c ?? new Citizen
                                                      {
                                                          Id = citizenVat,
                                                          FirstName = citizenName,
                                                          LastName = citizenSurname,
                                                          Email = citizenEmail,
                                                          PhoneNumber = citizenPhone.ToString(),
                                                          Address = citizenAddress,
                                                          County = citizenCounty
        };

        /// <summary>
        /// Parses data from this record into a new <see cref="Bill"/> object
        /// </summary>
        /// <returns>Valid <see cref="Bill"/> object</returns>
        public Bill ParseBill() => b = b ?? new Bill
                                                {
                                                    Id = Guid.Parse(billId.ToUpper()),
                                                    CitizenId = citizenVat,
                                                    Amount = decimal.Parse(
                                                        billAmount.Replace(",", "."),
                                                        NumberStyles.Float,
                                                        CultureInfo.InvariantCulture),
                                                    Description = billDescription,
                                                    DueDate = DateTime.ParseExact(
                                                        billDate,
                                                        "yyyyMMdd",
                                                        CultureInfo.InvariantCulture)
                                                };
    }
}
