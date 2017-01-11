//------------------------------------------------------------------------
// <copyright file="Response.cs" company="Volaris">
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
    /// Response class
    /// </summary>
    public class Response
    {
        /// <summary>
        /// Gets or sets the encoded.
        /// </summary>
        /// <value>
        /// The encoded.
        /// </value>
        public string Encoded { get; set; }

        /// <summary>
        /// Gets or sets the error.
        /// </summary>
        /// <value>
        /// The error.
        /// </value>
        public string Error { get; set; }
    }
}