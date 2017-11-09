using System;
using System.Collections.Generic;
using System.Text;

namespace EzPay.Model.IdentityEntities
{
    using EzPay.Model.Interfaces;

    using Microsoft.AspNetCore.Identity;

    /// <inheritdoc cref="IdentityUserRole{TKey}" />
    public class CitizenRole : IdentityUserRole<long>, IIdentityEntity
    {
        /// <summary>
        /// VAT Number is used as Identifier
        /// </summary>
        public long Id { get; set; }

    }
}
