using EcoVadis.AzureDevOps.Commands;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Management.Automation;
using System.Reflection;

namespace EcoVadis.AzureDevOps
{
    [Cmdlet(VerbsCommon.New, "BEStealing")]
    [Description("Adds stealing in the stealings")]
    public class NewBEStealingcmdlet : NewStealing
    {
        public NewBEStealingcmdlet()
        {
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_BindingRedirect;
            AppDomain.CurrentDomain.TypeResolve += CurrentDomain_TypeResolve;
        }

        private Assembly CurrentDomain_TypeResolve(object sender, ResolveEventArgs args)
        {
            throw new NotImplementedException();
        }

        protected override void BeginProcessing()
        {
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_BindingRedirect;
            AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve += CurrentDomain_ReflectionOnlyAssemblyResolve;
            base.AddCommand(new ReportStealingCommand(this, true));
        }

        private Assembly CurrentDomain_ReflectionOnlyAssemblyResolve(object sender, ResolveEventArgs args)
        {
            throw new NotImplementedException();
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

        protected override void ProcessRecord()
        {
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_BindingRedirect;
            base.ProcessCommands();
            base.ProcessRecord();
        }
    }
}
