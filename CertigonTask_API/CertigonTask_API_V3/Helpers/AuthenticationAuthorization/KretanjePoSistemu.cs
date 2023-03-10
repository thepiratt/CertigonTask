using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CertigonTask_API_V3.Data;
using CertigonTask_API_V3.Entities;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace CertigonTask_API_V3.Helpers.AuthenticationAuthorization
{
    public class KretanjePoSistemu
    {
        public static int Save(HttpContext httpContext, IExceptionHandlerPathFeature exceptionMessage = null)
        {
            UserAccount korisnik = httpContext.GetLoginInfo().userAccount;

            var request = httpContext.Request;

            var queryString = request.Query;

            if (queryString.Count == 0 && !request.HasFormContentType)
                return 0;

            //IHttpRequestFeature feature = request.HttpContext.Features.Get<IHttpRequestFeature>();
            string detalji = "";
            if (request.HasFormContentType)
            {
                foreach (string key in request.Form.Keys)
                {
                    detalji += " | " + key + "=" + request.Form[key];
                }
            }

            var x = new LogKretanjePoSistemu
            {
                User = korisnik,
                Time = DateTime.Now,
                queryPath = request.GetEncodedPathAndQuery(),
                postData = detalji,
                IpAdress = request.HttpContext.Connection.RemoteIpAddress?.ToString(),
            };

            if (exceptionMessage != null)
            {
                x.isException = true;
                x.ExceptionMessage = exceptionMessage.Error.Message + " |" + exceptionMessage.Error.InnerException;
            }

            ApplicationDbContext db = httpContext.RequestServices.GetService<ApplicationDbContext>();

            db.Add(x);
            db.SaveChanges();

            return x.Id;
        }




    }
}
