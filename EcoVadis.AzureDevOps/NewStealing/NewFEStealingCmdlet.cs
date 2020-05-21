using EcoVadis.AzureDevOps.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Management.Automation;
using System.Text;

namespace EcoVadis.AzureDevOps
{
    [Cmdlet(VerbsCommon.New, "FEStealing")]
    [Description("Adds stealing in the stealings")]
    public class NewFEStealingcmdlet : NewStealing
    {
        public NewFEStealingcmdlet()
        {
          
        }

        protected override void BeginProcessing()
        {
            base.AddCommand(new ReportStealingCommand(this, false));
        }

        protected override void ProcessRecord()
        {
            base.ProcessCommands();
            base.ProcessRecord();
        }
    }
}
