using System.Web.Mvc;
using CC.Core.Html;
using KnowYourTurf.Web.Controllers;

namespace KnowYourTurf.Web.Filters
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            if (filterContext.Result is HttpUnauthorizedResult)
            {
                filterContext.Result = new AjaxAwareRedirectResult(UrlContext.GetUrlForAction<LoginController>(x => x.Login(null)));

            }
        }
    }
}