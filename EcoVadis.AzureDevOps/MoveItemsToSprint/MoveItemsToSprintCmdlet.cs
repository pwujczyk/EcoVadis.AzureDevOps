using EcoVadis.AzureDevOps.Base;
using EcoVadis.AzureDevOps.MoveItemsToSprint.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Management.Automation;
using System.Text;

namespace EcoVadis.AzureDevOps.MoveItemsToSprint
{
    [Cmdlet(VerbsCommon.Move, "ItemsToSprint")]
    [Description("Cmdlet will move all items greater or equal to given stack rank to provided sprint")]
    public class MoveItemsToSprintCmdlet : CmdletBase
    {
        [Parameter(Position = 0, HelpMessage = "Moves US with task to this sprint", Mandatory = true)]
        public int TargetSprint { get; set; }

        [Parameter(Position = 1, HelpMessage = "Items greater or equal to this stack rank will be moved", Mandatory = true)]
        public int FromStackRank { get; set; }

        protected override void BeginProcessing()
        {
            base.AddCommand(new Move(this));
        }

        protected override void ProcessRecord()
        {
            base.ProcessCommands();
            base.ProcessRecord();
        }
    }
}
