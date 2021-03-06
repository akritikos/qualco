﻿namespace EzPay.Model.Entities
{
    using System.Collections.Generic;

    using EzPay.Model.Interfaces;

    using Microsoft.AspNetCore.Identity;

    /// <summary>
    /// Citizen mapper class
    /// </summary>
    public sealed class Citizen : IdentityUser<long>, IEntity
    {
        // /// <summary>
        // /// Initializes a new instance of the <see cref="Citizen"/> class
        // /// </summary>
        // public Citizen()
        // {
        //     this.Bills = new HashSet<Bill>();
        // }

        /// <summary>
        /// VAT Number is used as Identifier
        /// </summary>
        public override long Id { get; set; }

        /// <summary>
        /// Citizen's given name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Citizen's Surname
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Secure hash of password used to login
        /// </summary>
        public override string PasswordHash { get; set; }

        /// <summary>
        /// Email to use when sending temporary passwords
        /// </summary>
        public override string Email { get; set; }

        /// <summary>
        /// Registered citizen address
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Registered citizen county
        /// </summary>
        public string County { get; set; }

        /// <summary>
        /// Telephone number, string format to allow country code prefix following '+'
        /// </summary>
        public override string PhoneNumber { get; set; }

        /// <summary>
        /// Navigational field to access bills belonging to this citizen
        /// </summary>
        public ICollection<Bill> Bills { get; set; }

        /// <summary>
        /// Navigational property mapping the one-to-many relationship of <see cref="Citizen"/>
        /// with <see cref="Settlement"/>
        /// </summary>
        public ICollection<Settlement> Settlements { get; set; }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (!(obj is Citizen))
            {
                return false;
            }
            var o = (Citizen)obj;
            return o.Id.Equals(Id);
        }

        /// <inheritdoc />
        public override int GetHashCode() => Id.GetHashCode();
    }
}
