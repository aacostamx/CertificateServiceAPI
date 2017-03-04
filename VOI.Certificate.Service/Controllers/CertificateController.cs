//------------------------------------------------------------------------
// <copyright file="CertificateController.cs" company="AACOSTA">
//     Copyright (c) AACOSTA. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.Certificate.Service.Controllers
{
    using Models;
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Security.Cryptography;
    using System.Security.Cryptography.X509Certificates;
    using System.Text;
    using System.Web;
    using System.Web.Configuration;
    using System.Web.Http;


    /// <summary>
    /// CertificateController
    /// </summary>
    public class CertificateController : ApiController
    {
        /// <summary>
        /// Get Method
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        // GET: API/CERTIFICATE?BordingPass=
        public CertificateResponse Get([FromUri]UserData data)
        {
            var response = new CertificateResponse();

            if(string.IsNullOrEmpty(data.BoardingPass))
            {
                response.Error = "Boarding pass parameter is empty!";
            }
            else
            {
                response = this.CetificatePFXFile(data.BoardingPass);
            }

            return response;
        }

        /// <summary>
        /// Post Method
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        // POST: api/Certificate
        public CertificateResponse Post(UserData data)
        {
            var response = new CertificateResponse();

            if (string.IsNullOrEmpty(data.BoardingPass))
            {
                response.Error = "Boarding pass parameter is empty!";
            }
            else
            {
                response = this.CetificatePFXFile(data.BoardingPass);
            }

            return response;
        }

        /// <summary>
        /// Cetificates the PFX file.
        /// </summary>
        /// <param name="BoardingPass">The boarding pass.</param>
        /// <returns></returns>
        private CertificateResponse CetificatePFXFile(string BoardingPass)
        {
            var response = new CertificateResponse();
            var cert = new X509Certificate2();
            var byteConverter = new ASCIIEncoding();
            var collection = new X509Certificate2Collection();
            var path = string.Empty;
            var password = string.Empty;
            var data = new byte[0];

            try
            {
                path = HttpContext.Current.Server.MapPath(WebConfigurationManager.AppSettings["PFXPath"]);

                using (StreamReader sr = new StreamReader(HttpContext.Current.Server.MapPath(WebConfigurationManager.AppSettings["PasswordPath"])))
                {
                    password = sr.ReadToEnd();
                }

                data = byteConverter.GetBytes(BoardingPass);
                collection.Import(path, password, X509KeyStorageFlags.PersistKeySet);
                cert = collection[0];

                using (ECDsa ecdsa = cert.GetECDsaPrivateKey())
                {
                    if (ecdsa != null)
                    {
                        var result = ecdsa.SignData(data, HashAlgorithmName.SHA256);
                        var sign = Convert.ToBase64String(result);
                        response.Encoded = BoardingPass + "^1" + sign.Length.ToString("X") + sign;
                        //var verify = ecdsa.VerifyData(data, result, HashAlgorithmName.SHA256);
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError(string.Format("Error {0}", DateTime.Now.ToString("dd MMMM yyyy hh:mm:ss")));
                Trace.TraceError("Error: SISAC API - Launcher");
                Trace.TraceError(ex.Message, ex);
                response.Error = ex.ToString();
            }

            return response;
        }

    }
}
