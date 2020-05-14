using EcoVadis.AzureDevOps.App;
using System;
using System.Collections.Generic;
using System.Text;
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
            TfsAddress = Environment.GetEnvironmentVariable("TTTFSAddress");
            PAT = Environment.GetEnvironmentVariable("TTPAT");
            UserName = Environment.GetEnvironmentVariable("TTuserName");

            App = new TimeTrackingApp(TfsAddress, PAT);
        }
    }
}
