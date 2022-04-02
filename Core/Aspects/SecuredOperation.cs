using PostSharp.Aspects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Core.Aspects
{
    [Serializable]
    public class SecuredOperation : OnMethodBoundaryAspect
    {
        public string Roles { get; set; }
        public override void OnEntry(MethodExecutionArgs args)
        {
            string[] roles = Roles.Split(',');
            bool authorized = false;
            foreach(var  item in roles){
                if (System.Threading.Thread.CurrentPrincipal.IsInRole(item))
                {
                    authorized = true;

                }
                if (authorized)
                {
                    throw new SecurityException("not authorized");
                }

            }
        }
    }
}
