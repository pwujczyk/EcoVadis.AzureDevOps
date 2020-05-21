using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace EcoVadis.AzureDevOps
{
    public static class AssemblyResolver
    {
        static AssemblyResolver()
        {
            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(Method);
        }

        private static Assembly Method(object sender, ResolveEventArgs args)
        {
            var name = new AssemblyName(args.Name);
            if (name.Name == "FooLibrary")
            {
                return typeof(string).Assembly;
            }
            return null;
        }

        public static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            var name = new AssemblyName(args.Name);
            if (name.Name == "FooLibrary")
            {
                return typeof(string).Assembly;
            }
            return null;
        }
    }
}
