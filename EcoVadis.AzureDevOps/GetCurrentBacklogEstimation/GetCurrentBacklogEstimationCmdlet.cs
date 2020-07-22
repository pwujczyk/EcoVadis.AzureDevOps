using EcoVadis.AzureDevOps.App;
using EcoVadis.AzureDevOps.Base;
using EcoVadis.AzureDevOps.GetCurrentBacklogEstimation.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Management.Automation;
using System.Text;

namespace EcoVadis.AzureDevOps.GetCurrentBacklogEstimation
{
    [Cmdlet(VerbsCommon.Get, "CurrentBacklogEstimation")]
    [Description("XX")]
    public class GetCurrentBacklogEstimationCmdlet : CmdletBase
    {
        public GetCurrentBacklogEstimationCmdlet()
        {
        }

        protected override void BeginProcessing()
        {
            base.BeginProcessing();
            base.AddCommand(new GetEstimation(this));
        }

        protected override void ProcessRecord()
        {
            base.ProcessCommands();
            base.ProcessRecord();
        }
    }
}
