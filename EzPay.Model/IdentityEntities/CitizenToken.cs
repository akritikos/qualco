using System;
using System.Collections.Generic;
using System.Text;

namespace EzPay.Model.IdentityEntities
{
    using EzPay.Model.Interfaces;

    using Microsoft.AspNetCore.Identity;

    /// <inheritdoc cref="IdentityUserToken{TKey}" />
    public class CitizenToken : IdentityUserToken<long>, IIdentityEntity
    {
        /// <summary>
        /// VAT Number is used as Identifier
        /// </summary>
        public long Id { get; set; }
    }
}
