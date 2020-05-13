using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Management.Automation;
using System.Text;

namespace ProductivityTools.AzureDevOps.TimeTracking.GetItem
{

    [Cmdlet(VerbsCommon.Get, "Item")]
    [Description("Get item details")]
    public class GetItemCmdlet : PSCmdlet.PSCmdletPT
    {
        public GetItemCmdlet()
        {
           // base.AddCommand(new GetItemById(this, true));
        }

        protected override void ProcessRecord()
        {
            base.ProcessCommands();
            base.ProcessRecord();
        }
    }
}
