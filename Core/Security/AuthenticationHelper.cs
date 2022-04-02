using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Core.Security
{
   public  class AuthenticationHelper
    {
        public static void CreateAuthCookie(Guid id,string username,string [] roles,DateTime expiration,bool rememberMe)
        {
            //var authTicket = new FormsAuthentication();
        }
    }
}
