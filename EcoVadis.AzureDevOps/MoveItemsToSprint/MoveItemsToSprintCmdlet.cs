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
    [Description("XX")]
    public class MoveItemsToSprintCmdlet : CmdletBase
    {
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
