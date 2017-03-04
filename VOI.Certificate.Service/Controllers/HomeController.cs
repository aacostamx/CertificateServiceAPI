//------------------------------------------------------------------------
// <copyright file="HomeController.cs" company="AACOSTA">
//     Copyright (c) AACOSTA. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.Certificate.Service.Controllers
{
    using System.Web.Mvc;

    /// <summary>
    /// Home class
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
    }
}
