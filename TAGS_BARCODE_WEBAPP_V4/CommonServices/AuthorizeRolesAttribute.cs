using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TAGS_BARCODE_WEBAPP_V4.Models;

namespace TAGS_BARCODE_WEBAPP_V4.CommonServices
{
    public class AuthorizeRolesAttribute : AuthorizeAttribute
    {
        private readonly string[] userAssignedRoles;
        public AuthorizeRolesAttribute(params string[] roles)
        {
            this.userAssignedRoles = roles;
        }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool authorize = false;
                    authorize = IsUserAdmin();
                    if (authorize)
                        return authorize;
                
            
            return authorize;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectResult("~/Home/UnAuthorized");
        }

        public bool IsUserAdmin()
        {
            using (var db = new TagsDataModel())
            {
                //TODO check if logged in user is admin
            }
            return true;
        }


    }
}