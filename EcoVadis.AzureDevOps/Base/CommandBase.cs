using System;
using System.Collections.Generic;
using System.Text;
using EcoVadis.AzureDevOps.App;
using Microsoft.Extensions.Configuration;
using ProductivityTools.MasterConfiguration;
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
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddMasterConfiguration(force:true)
                .Build(); 

            TfsAddress = configuration["TTTFSAddress"];
            PAT = configuration["TTPAT"];
            UserName = configuration["TTuserName"];

            this.Cmdlet.WriteVerbose($"TfsAddress {TfsAddress}");
            this.Cmdlet.WriteVerbose($"PAT {PAT}");
            this.Cmdlet.WriteVerbose($"UserName {UserName}");

            App = new TimeTrackingApp(TfsAddress, PAT);
        }


    }
}
