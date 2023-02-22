using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CertigonTask_API_V3.Models.Accounts;
using CertigonTask_API_V3.Helpers.AuthenticationAuthorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CertigonTask_API_V3.Helpers.AuthenticationAuthorization
{
    public class AutorizacijaAttribute : TypeFilterAttribute
    {
        public AutorizacijaAttribute(bool admin, bool manager)
            : base(typeof(MyAuthorizeImpl))
        {
            Arguments = new object[] {  };
        }
    }


    public class MyAuthorizeImpl : IActionFilter
    {
        private readonly bool _admin;
        private readonly bool _manager;

        public MyAuthorizeImpl(bool admin, bool manager)
        {
            _admin = admin;
            _manager = manager;
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {


        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {

            if (filterContext.HttpContext.GetLoginInfo().isLogiran)
            {
                filterContext.Result = new UnauthorizedResult();
                return;
            }

            KretanjePoSistemu.Save(filterContext.HttpContext);
            
            if (filterContext.HttpContext.GetLoginInfo().isLogiran)
            {
                return;//ok - ima pravo pristupa
            }

            if (filterContext.HttpContext.GetLoginInfo().isPermisijaAdmin)
            {
                return;//ok - ima pravo pristupa
            }

            if (filterContext.HttpContext.GetLoginInfo().isPermisijaManager)
            {
                return;//ok - ima pravo pristupa
            }

            //else nema pravo pristupa
            filterContext.Result = new UnauthorizedResult();
        }
    }
}
