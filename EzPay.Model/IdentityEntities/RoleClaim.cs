using System;
using System.Collections.Generic;
using System.Text;

namespace EzPay.Model.IdentityEntities
{
    using Microsoft.AspNetCore.Identity;
    public class RoleClaim : IdentityRoleClaim<long>
    {
        /// <summary>
        /// VAT Number is used as Identifier
        /// </summary>
        public long RoleClaimId { get; set; }
    }
}
