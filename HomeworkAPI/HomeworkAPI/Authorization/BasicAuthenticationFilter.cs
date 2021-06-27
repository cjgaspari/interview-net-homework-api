using System;
using System.Net;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using HomeworkAPI.Authorization.Services;

namespace HomeworkAPI.Authorization
{
  public class BasicAuthenticationFilter : IAuthorizationFilter 
  {

    private readonly string realm;
    public BasicAuthenticationFilter(string realm)
    {
      this.realm = realm;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
      try
      {
        //Get the HTTP Request header for the authorization
        //Using HTTP Basic standard to demo authorization
        //This would not be suitable for production
        string authHeader = context.HttpContext.Request.Headers["Authorization"];
        if (authHeader != null)
        {
          var authHeaderValue = AuthenticationHeaderValue.Parse(authHeader);
          if (authHeaderValue.Scheme.Equals(AuthenticationSchemes.Basic.ToString(), StringComparison.OrdinalIgnoreCase))
          {
            //Convert from Base64, and get the user:pass
            var credentials = Encoding.UTF8
                                .GetString(Convert.FromBase64String(authHeaderValue.Parameter ?? string.Empty))
                                .Split(':', 2);
            if (credentials.Length == 2)
            {
              if (IsAuthorized(context, credentials[0], credentials[1]))
              {
                GenericPrincipal currentPrincipal;
                currentPrincipal = new GenericPrincipal(new GenericIdentity(credentials[0]), null);
                context.HttpContext.User = currentPrincipal;
                return;
              }
            }
          }
        }

        ReturnUnauthorizedResult(context);
      }
      catch (FormatException)
      {
        ReturnUnauthorizedResult(context);
      }
    }

    public bool IsAuthorized(AuthorizationFilterContext context, string username, string password)
    {
      //This calls a stub "IsValidUser"
      //IsValidUser will return true if the user provided any user/pass combo
      //This is just to demonstrate how we would secure the student and teacher endpoints
      var userService = context.HttpContext.RequestServices.GetRequiredService<IUserService>();
      return userService.IsValidUser(username, password, realm);
    }

    private void ReturnUnauthorizedResult(AuthorizationFilterContext context)
    {
      //If user has not been authorized, prompt the browser for login credentials
      //context.HttpContext.Response.Headers["WWW-Authenticate"] = $"Basic realm=\"{realm}\"";
      context.Result = new UnauthorizedResult();
    }
  }
}
