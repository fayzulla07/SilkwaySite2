using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using CKSource.CKFinder.Connector.Core;
using CKSource.CKFinder.Connector.Core.Authentication;
using WebSiteCkEditor.Helpers;

namespace WebSiteCkEditor
{
    //  [Authorize(Roles = "admin")]
    public class CKFinderAuthenticator : IAuthenticator
    {
        //   [Authorize(Roles = "admin")]
        public Task<IUser> AuthenticateAsync(ICommandRequest commandRequest, CancellationToken cancellationToken)
        {
            bool IsAutorized = false; // Cookies["dvuhsivhihh"];
            string val = null;
            bool result = commandRequest.Cookies.ContainsKey("dvuhsivhihh");
            if (result)
            {
                val = commandRequest.Cookies["dvuhsivhihh"];
            }

            if (val != null)
            {
                string value = Coder.DecodeServerName(val);
                if (value == "admin")
                {
                    IsAutorized = true;
                }

            }
            var user = new User(IsAutorized, new List<string>());
            return Task.FromResult((IUser)user);
        }
    }
}