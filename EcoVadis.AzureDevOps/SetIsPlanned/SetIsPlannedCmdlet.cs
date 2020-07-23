using EcoVadis.AzureDevOps.Base;
using EcoVadis.AzureDevOps.SetIsPlanned.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Management.Automation;
using System.Text;

namespace EcoVadis.AzureDevOps.SetIsPlanned
{
    [Cmdlet(VerbsCommon.Set, "IsPlanned")]
    [Description("Set for all task in sprint is planned property")]
    public class SetIsPlannedCmdlet : CmdletBase
    {
        [Parameter(Position = 1, HelpMessage = "Defines if IsPlanned should be true or false", Mandatory = false)]
        public bool Value { get; set; } = true;

        public SetIsPlannedCmdlet()
        {
        }

        protected override void BeginProcessing()
        {
            base.BeginProcessing();
            base.AddCommand(new Set(this));
        }

        protected override void ProcessRecord()
        {
            base.ProcessCommands();
            base.ProcessRecord();
        }
    }
}
