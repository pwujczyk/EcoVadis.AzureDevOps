using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Text;

namespace ProductivityTools.AzureDevOps.TimeTracking
{
    public abstract class NewStealing : PSCmdlet.PSCmdletPT
    {
        [Parameter(Position = 0, HelpMessage = "Name of the stealing", Mandatory = true)]
        public string Name { get; set; }

        [Parameter(Position = 1, HelpMessage = "Amout of time to be reported", Mandatory = true)]
        public float Time { get; set; }

        [Parameter(HelpMessage = "By default stealing is closed if you want to leave it active set this flag")]
        public SwitchParameter LeaveActive { get; set; }
    }
}
