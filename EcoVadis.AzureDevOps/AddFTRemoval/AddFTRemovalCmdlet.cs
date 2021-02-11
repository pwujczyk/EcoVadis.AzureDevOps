using EcoVadis.AzureDevOps.AddFTRemoval.Commands;
using EcoVadis.AzureDevOps.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Management.Automation;
using System.Text;

namespace EcoVadis.AzureDevOps.AddFTRemoval
{
    [Cmdlet(VerbsCommon.Add, "FTRemoval")]
    [Description("Add user stories for all FT which exists on production")]
    public class AddFTRemovalCmdlet : CmdletBase
    {
        public AddFTRemovalCmdlet()
        {
        }

        protected override void BeginProcessing()
        {
            this.AddCommand(new Add(this));
            this.ProcessCommands();
            base.BeginProcessing();
        }
    }
}
