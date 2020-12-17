using EcoVadis.AzureDevOps.AddTesting.Commands;
using EcoVadis.AzureDevOps.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Management.Automation;
using System.Text;

namespace EcoVadis.AzureDevOps.AddTesting
{
    [Cmdlet(VerbsCommon.Add, "Testing")]
    [Description("Adds Testing and TestCase task for given US")]
    public class AddTestingCmdlet : CmdletBase
    {
        [Parameter(Position = 1, HelpMessage = "User story Id, for which task should be added.", Mandatory = true)]
        public int UsId { get; set; }

        [Parameter(Position = 1, HelpMessage = "If used, it won't throw exception if task already exists", Mandatory = false)]
        public SwitchParameter Silent { get; set; }

        public AddTestingCmdlet()
        {
        }

        protected override void BeginProcessing()
        {
            base.BeginProcessing();
        }

        protected override void ProcessRecord()
        {
            base.AddCommand(new Add(this, false));
            base.ProcessCommands();
            base.ProcessRecord();
        }
    }
}
