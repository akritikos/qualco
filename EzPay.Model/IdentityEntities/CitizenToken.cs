using System;
using System.Collections.Generic;
using System.Text;

namespace EzPay.Model.IdentityEntities
{
    using Microsoft.AspNetCore.Identity;
    public class CitizenToken : IdentityUserToken<long>
    {
        /// <summary>
        /// VAT Number is used as Identifier
        /// </summary>
        public long Id { get; set; }
    }
}
