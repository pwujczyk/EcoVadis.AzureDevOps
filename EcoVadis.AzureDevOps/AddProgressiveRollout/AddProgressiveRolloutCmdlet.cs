using EcoVadis.AzureDevOps.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Management.Automation;
using System.Text;

namespace EcoVadis.AzureDevOps.AddProgressiveRollout
{
    public  abstract class AddProgressiveRolloutCmdlet : CmdletBase
    {
        [Parameter(Position = 1, HelpMessage = "User story Id, for which task should be added.", Mandatory = true)]
        public int UsId { get; set; }

        [Parameter(Position = 1, HelpMessage = "If used, it won't throw exception if task already exists", Mandatory = false)]
        public SwitchParameter Silent { get; set; }

        public AddProgressiveRolloutCmdlet()
        {
        }

        protected override void BeginProcessing()
        {
            base.BeginProcessing();
        }
    }
}
