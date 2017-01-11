//------------------------------------------------------------------------
// <copyright file="CertificateController.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.Certificate.Service.Controllers
{
    using System;
    using System.Diagnostics;
    using System.Security.Cryptography;
    using System.Security.Cryptography.X509Certificates;
    using System.Text;
    using System.Web.Configuration;
    using System.Web.Http;


    public class CertificateController : ApiController
    {
        /// <summary>
        /// Get
        /// </summary>
        /// <returns></returns>
        // GET: API/CERTIFICATE
        public string Get()
        {
            return "value";
        }

        /// <summary>
        /// Get
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        // GET: API/CERTIFICATE/5
        public string Get(string text)
        {
            var encoded = string.Empty;

            try
            {
                encoded = this.CetificatePFXFile(text);
            }
            catch (Exception ex)
            {
                Trace.TraceError(string.Format("Error {0}", DateTime.Now.ToString("dd MMMM yyyy hh:mm:ss")));
                Trace.TraceError("Error: Get");
                Trace.TraceError(ex.Message, ex);
                encoded = ex.ToString();
            }

            return encoded;
        }

        /// <summary>
        /// Post
        /// </summary>
        /// <param name="text"></param>
        // POST: api/Certificate
        public string Post([FromBody]string text)
        {
            var encoded = string.Empty;

            try
            {
                encoded = this.CetificatePFXFile(text);
            }
            catch (Exception ex)
            {
                Trace.TraceError(string.Format("Error {0}", DateTime.Now.ToString("dd MMMM yyyy hh:mm:ss")));
                Trace.TraceError("Error: Post");
                Trace.TraceError(ex.Message, ex);
                encoded = ex.ToString();
            }

            return encoded;
        }

        private string CetificatePFXFile(string text)
        {
            var encoded = string.Empty;
            var cert = new X509Certificate2();
            var byteConverter = new ASCIIEncoding();
            var collection = new X509Certificate2Collection();
            var path = string.Empty;
            var password = string.Empty;

            try
            {
                path = WebConfigurationManager.AppSettings["PFXPath"];
                password = WebConfigurationManager.AppSettings["Password"];
                var data = byteConverter.GetBytes(text);
                collection.Import(path, password, X509KeyStorageFlags.PersistKeySet);
                cert = collection[0];

                using (ECDsa ecdsa = cert.GetECDsaPrivateKey())
                {
                    if (ecdsa != null)
                    {
                        var result = ecdsa.SignData(data, HashAlgorithmName.SHA256);
                        var sign = Convert.ToBase64String(result);
                        encoded = text + "^1" + sign.Length.ToString("X") + sign;
                        var verify = ecdsa.VerifyData(data, result, HashAlgorithmName.SHA256);
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError(string.Format("Error {0}", DateTime.Now.ToString("dd MMMM yyyy hh:mm:ss")));
                Trace.TraceError("Error: SISAC API - Launcher");
                Trace.TraceError(ex.Message, ex);
            }

            return encoded;
        }

    }
}
