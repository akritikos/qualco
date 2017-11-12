using System;
using System.Collections.Generic;
using System.Text;

namespace EzPay.Model.IdentityEntities
{
    using EzPay.Model.Interfaces;

    using Microsoft.AspNetCore.Identity;

    /// <inheritdoc cref="IdentityRoleClaim{TKey}" />
    public class RoleClaim : IdentityRoleClaim<long>, IEntity
    {
        /// <summary>
        /// VAT Number is used as Identifier
        /// </summary>
        public long RoleClaimId { get; set; }
    }
}
