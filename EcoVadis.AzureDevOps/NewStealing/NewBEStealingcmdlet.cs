using EcoVadis.AzureDevOps.Commands;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Management.Automation;
using System.Reflection;

namespace EcoVadis.AzureDevOps
{
    [Cmdlet(VerbsCommon.New, "BEStealing")]
    [Description("Adds stealing in the stealings")]
    public class NewBEStealingCmdlet : NewStealing
    {
        public NewBEStealingCmdlet() { }

        protected override void BeginProcessing()
        {
            base.BeginProcessing();
            base.AddCommand(new ReportStealingCommand(this, true));
        }

        protected override void ProcessRecord()
        {
            base.ProcessCommands();
            base.ProcessRecord();
        }
    }
}
