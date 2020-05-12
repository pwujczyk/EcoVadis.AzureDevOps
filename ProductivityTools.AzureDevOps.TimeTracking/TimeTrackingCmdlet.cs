using ProductivityTools.AzureDevOps.TimeTracking.Commands;
using System;
using System.ComponentModel;
using System.Management.Automation;

namespace ProductivityTools.AzureDevOps.TimeTracking
{
    [Cmdlet(VerbsCommon.Get, "AssignedItems")]
    [Description("This is module used to help track time")]
    public class TimeTrackingCmdlet : PSCmdlet.PSCmdletPT
    {
        [Parameter(HelpMessage = "This is name")]
        public string Name { get; set; }

        public TimeTrackingCmdlet()
        {
            base.AddCommand(new TimeTrackingCommandAll(this));
        }

        protected override void ProcessRecord()
        {
            base.ProcessCommands();
            base.ProcessRecord();
        }
    }
}
