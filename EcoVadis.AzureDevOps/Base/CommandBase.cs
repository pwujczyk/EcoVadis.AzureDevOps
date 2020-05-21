using System;
using System.Collections.Generic;
using System.Text;
using EcoVadis.AzureDevOps.App;
using ProductivityTools.PSCmdlet;

namespace EcoVadis.AzureDevOps.Base
{
    public abstract class CommandBase<CmdletType> : PSCommandPT<CmdletType> where CmdletType : PSCmdletPT
    {
        protected string TfsAddress;
        protected string PAT;
        protected string UserName;
        protected TimeTrackingApp App;

        public CommandBase(CmdletType cmdlet) : base(cmdlet)
        {
            Func<string, string> GetVariable = (name) =>
               {
                   var @var = Environment.GetEnvironmentVariable(name);
                   if (string.IsNullOrEmpty(var))
                   {
                       throw new Exception($"You need to setup environment variable with the name {name}");
                   }
                   return var;
               };

            TfsAddress = GetVariable("TTTFSAddress");
            PAT = GetVariable("TTPAT");
            UserName = GetVariable("TTuserName");

            App = new TimeTrackingApp(TfsAddress, PAT);
        }


    }
}
