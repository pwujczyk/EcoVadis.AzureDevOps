using ProductivityTools.PSCmdlet;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace EcoVadis.AzureDevOps.Base
{
    public abstract class CmdletBase: PSCmdletPT
    {
        protected override void BeginProcessing()
        {
           AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_BindingRedirect;
            base.BeginProcessing();
        }

        private Assembly CurrentDomain_BindingRedirect(object sender, ResolveEventArgs args)
        {
            var name = new AssemblyName(args.Name);
            switch (name.Name)
            {
                case "Newtonsoft.Json":
                    return typeof(Newtonsoft.Json.JsonSerializer).Assembly;
                default:
                    return null;
            }
        }
    }
}
