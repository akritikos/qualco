using System;
using System.Collections.Generic;
using System.Text;

namespace EzPay.IO.Wrapper
{
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;

    using EzPay.Model.Entities;

    using FileHelpers;

    /// <summary>
    /// Wrapper class for parsing CSV records
    /// </summary>
    [IgnoreFirst, DelimitedRecord(";")]
    [SuppressMessage("ReSharper", "StyleCop.SA1600", Justification = "Properties are private & documented in Citizen class")]
    class DebtRecord
    {
        [FieldHidden]
        private Citizen c;

        [FieldHidden]
        private Bill b;
        #region Citizen
        private long CitizenVat { get; set; }

        private string CitizenName { get; set; }

        private string CitizenSurname { get; set; }

        private string CitizenEmail { get; set; }

        private int CitizenPhone { get; set; }

        private string CitizenAddress { get; set; }

        private string CitizenCounty { get; set; }
        #endregion Citizen

        #region Bill
        private string BillId { get; set; }

        private string BillDescription { get; set; }

        private decimal BillAmount { get; set; }

        private string BillDate { get; set; }
        #endregion

        /// <summary>
        /// Parses data from this record into a new <see cref="Citizen"/> object
        /// </summary>
        /// <returns>Valid <see cref="Citizen"/> object</returns>
        public Citizen ParseCitizen() => c = c ?? new Citizen()
                                                      {
                                                          Id = CitizenVat,
                                                          FirstName = CitizenName,
                                                          LastName = CitizenSurname,
                                                          Email = CitizenEmail,
                                                          PhoneNumber = CitizenPhone.ToString(),
                                                          Address = CitizenAddress,
                                                          County = CitizenCounty
                                                      };

        /// <summary>
        /// Parses data from this record into a new <see cref="Bill"/> object
        /// </summary>
        /// <returns>Valid <see cref="Bill"/> object</returns>
        public Bill ParseBill() => b = b ?? new Bill()
                                                {
                                                    Id = Guid.Parse(BillId.ToUpper()),
                                                    CitizenId = CitizenVat,
                                                    Amount = BillAmount,
                                                    Description = BillDescription,
                                                    DueDate = DateTime.ParseExact(
                                                        BillDate,
                                                        "yyyyMMdd",
                                                        CultureInfo.InvariantCulture)
                                                };
    }
}
