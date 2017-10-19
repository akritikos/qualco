using System;
using System.Collections.Generic;
using System.Text;

namespace EzPay.Model
{
    /// <summary>
    /// Simple User states to handle login logic by the WebPortal
    /// </summary>
    public enum UserState
    {
        /// <summary>
        /// User has been asigned a random password, should be sent by email
        /// and be forced to change it on first login
        /// </summary>
        New,

        /// <summary>
        /// Default state
        /// </summary>
        Normal,

        /// <summary>
        /// User has been locked out by too many attempts with wrong credentials
        /// </summary>
        Locked,

        /// <summary>
        /// Client has requested disabling this user
        /// </summary>
        Disabled
    }
}
