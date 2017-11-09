using System;
using System.Collections.Generic;
using System.Text;

namespace EzPay.Model.IdentityEntities
{
    using EzPay.Model.Interfaces;

    using Microsoft.AspNetCore.Identity;

    /// <inheritdoc cref="IdentityUserClaim{TKey}"/>
    public class CitizenClaim : IdentityUserClaim<long>, IEntity
    {
        /// <summary>
        /// VAT Number is used as Identifier
        /// </summary>
        public long CitizenClaimId { get; set; }
    }
}
