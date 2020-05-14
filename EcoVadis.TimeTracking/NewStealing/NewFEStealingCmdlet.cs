using EcoVadis.TimeTracking.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Management.Automation;
using System.Text;

namespace EcoVadis.TimeTracking
{
    [Cmdlet(VerbsCommon.New, "FEStealing")]
    [Description("Adds stealing in the stealings")]
    public class NewFEStealingcmdlet : NewStealing
    {
        public NewFEStealingcmdlet()
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
