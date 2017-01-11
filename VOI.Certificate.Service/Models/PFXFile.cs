//------------------------------------------------------------------------
// <copyright file="PFXFile.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.Certificate.Service.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    /// <summary>
    /// PFXFile class
    /// </summary>
    public class PFXFile
    {
        /// <summary>
        /// Gets or sets the PFX path.
        /// </summary>
        /// <value>
        /// The PFX path.
        /// </value>
        public string PFXPath { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>
        /// The text.
        /// </value>
        public string Text { get; set; }
    }
}