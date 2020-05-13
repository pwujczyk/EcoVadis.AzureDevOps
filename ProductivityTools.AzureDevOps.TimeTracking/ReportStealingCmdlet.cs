using ProductivityTools.AzureDevOps.TimeTracking.Commands;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Management.Automation;

namespace ProductivityTools.AzureDevOps.TimeTracking
{
    [Cmdlet(VerbsCommon.Add, "Stealing")]
    [Description("Adds stealing in the stealings")]
    public class ReportStealingCmdlet : PSCmdlet.PSCmdletPT
    {
        [Parameter(HelpMessage = "Name of the stealing", Mandatory = true)]
        public string Name { get; set; }

        [Parameter(HelpMessage = "Amout of time to be reported", Mandatory = true)]
        public float Time { get; set; }

        [Parameter(HelpMessage = "By default stealing is closed if you want to leave it active set this flag")]
        public SwitchParameter Unclosed { get; set; }

        public ReportStealingCmdlet()
        {
            base.AddCommand(new ReportStealingCommand(this));
        }

        protected override void ProcessRecord()
        {
            base.ProcessCommands();
            base.ProcessRecord();
        }
    }
}
