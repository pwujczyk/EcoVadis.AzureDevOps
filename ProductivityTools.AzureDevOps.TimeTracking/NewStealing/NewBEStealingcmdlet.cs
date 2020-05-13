using ProductivityTools.AzureDevOps.TimeTracking.Commands;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Management.Automation;

namespace ProductivityTools.AzureDevOps.TimeTracking
{
    [Cmdlet(VerbsCommon.New, "BEStealing")]
    [Description("Adds stealing in the stealings")]
    public class NewBEStealingcmdlet : NewStealing
    {
        public NewBEStealingcmdlet()
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
