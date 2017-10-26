using System;
using System.Collections.Generic;
using System.Text;

namespace EzPay.Model.IdentityEntities
{
    using Microsoft.AspNetCore.Identity;
    public class CitizenClaim : IdentityUserClaim<long>
    {
        /// <summary>
        /// VAT Number is used as Identifier
        /// </summary>
        public long CitizenClaimId { get; set; }
    }
}
