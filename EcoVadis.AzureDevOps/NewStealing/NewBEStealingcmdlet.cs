using EcoVadis.AzureDevOps.Commands;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Management.Automation;

namespace EcoVadis.AzureDevOps
{
    [Cmdlet(VerbsCommon.New, "BEStealing")]
    [Description("Adds stealing in the stealings")]
    public class NewBEStealingcmdlet : NewStealing
    {
        public NewBEStealingcmdlet() { }

        protected override void BeginProcessing()
        {
            base.AddCommand(new ReportStealingCommand(this, true));
        }

        protected override void ProcessRecord()
        {
            base.ProcessCommands();
            base.ProcessRecord();
        }
    }
}
