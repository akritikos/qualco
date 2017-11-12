using System;
using System.Collections.Generic;
using System.Text;

namespace EzPay.Model.IdentityEntities
{
    using EzPay.Model.Interfaces;

    using Microsoft.AspNetCore.Identity;

    /// <inheritdoc cref="IdentityRole{TKey}" />
    public class Role : IdentityRole<long>, IEntity
    {
        /// <summary>
        /// VAT Number is used as Identifier
        /// </summary>
        public long RoleId { get; set; }
    }
}
