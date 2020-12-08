using EcoVadis.AzureDevOps.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Management.Automation;
using System.Text;

namespace EcoVadis.AzureDevOps.AddProgressiveRollout
{
    [Cmdlet(VerbsCommon.Add, "BEProgressiveRollout")]
    [Description("Adds eco task to us with name Progressive Rollout")]
    public class AddBEProgressiveRolloutCmdlet : AddProgressiveRolloutCmdlet
    {
        public AddBEProgressiveRolloutCmdlet()
        {
        }

        protected override void BeginProcessing()
        {
            base.BeginProcessing();
        }

        protected override void ProcessRecord()
        {
            base.AddCommand(new Add(this, true));
            base.ProcessCommands();
            base.ProcessRecord();
        }
    }
}
