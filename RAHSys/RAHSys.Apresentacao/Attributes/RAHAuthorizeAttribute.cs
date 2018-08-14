using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using RAHSys.Apresentacao.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace RAHSys.Apresentacao.Attributes
{
    /// <summary>
    /// Autoriza algum perfil a algum recurso da aplicação, redirecionando para a página de 
    /// acesso negado caso seja necessário.
    /// Usuários no perfil Admin estão sempre autorizados a qualquer função!
    /// Uso: 
    ///     [RAHAuthorize] ==> Apenas Admin está autorizado
    ///     [RAHAuthorize(Roles = "NomeDoPerfil1[, NomeDoPerfil2, NomeDoPerfil3]")] ==> Apenas este(s) perfil(s) e o Admin
    /// </summary>
    public class RAHAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {

            // Regra para Autorização: 
            // Um usuário está autorizado se o seu perfil condiz com 
            // um dos perfis autorizado da função, ou se ele for um Admin 
            if (String.IsNullOrEmpty(Roles))
                Roles = "Admin";
            else
                Roles += ", Admin";

            var isAuthorized = base.AuthorizeCore(httpContext);
            
            return isAuthorized;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary(
                        new
                        {
                            controller = "Account",
                            action = "Unauthorized"
                        }
                    )
                );
            }
            else
            {
                base.HandleUnauthorizedRequest(filterContext);
            }
        }
    }
}