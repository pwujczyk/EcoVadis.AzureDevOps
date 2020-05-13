using ProductivityTools.AzureDevOps.TimeTracking.App;
using ProductivityTools.PSCmdlet;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityTools.AzureDevOps.TimeTracking.Base
{
    public abstract class CommandBase<CmdletType> : PSCmdlet.PSCommandPT<CmdletType> where CmdletType : PSCmdletPT
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
