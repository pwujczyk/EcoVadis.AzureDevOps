using EcoVadis.AzureDevOps.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Management.Automation;
using System.Text;

namespace EcoVadis.AzureDevOps.AddProgressiveRollout
{
    [Cmdlet(VerbsCommon.Add, "ProgressiveRollout")]
    [Description("Adds eco task to us with name Progressive Rollout")]
    public class AddProgressiveRolloutCmdlet : CmdletBase
    {
        [Parameter(Position = 1, HelpMessage = "User story Id, for which task should be added.", Mandatory = true)]
        public int UsId { get; set; }

        public AddProgressiveRolloutCmdlet()
        {
        }

        protected override void BeginProcessing()
        {
            base.BeginProcessing();
        }

        protected override void ProcessRecord()
        {
            base.AddCommand(new Add(this));
            base.ProcessCommands();
            base.ProcessRecord();
        }
    }
}
