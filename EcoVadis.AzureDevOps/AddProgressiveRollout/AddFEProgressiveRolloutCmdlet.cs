using EcoVadis.AzureDevOps.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Management.Automation;
using System.Text;

namespace EcoVadis.AzureDevOps.AddProgressiveRollout
{
    [Cmdlet(VerbsCommon.Add, "FEProgressiveRollout")]
    [Description("Adds eco task to us with name Progressive Rollout")]
    public class AddFEProgressiveRolloutCmdlet : AddProgressiveRolloutCmdlet
    {

        public AddFEProgressiveRolloutCmdlet()
        {
        }

        protected override void BeginProcessing()
        {
            base.BeginProcessing();
        }

        protected override void ProcessRecord()
        {
            base.AddCommand(new Add(this, false));
            base.ProcessCommands();
            base.ProcessRecord();
        }
    }
}
