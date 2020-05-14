using EcoVadis.AzureDevOps.GetItem.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Management.Automation;
using System.Text;
using ProductivityTools.PSCmdlet;

namespace EcoVadis.AzureDevOps.GetItem
{

    [Cmdlet(VerbsCommon.Get, "TFSItem")]
    [Description("Get item details")]
    public class GetItemCmdlet : PSCmdletPT
    {
        [Parameter(Mandatory =true,Position =0, HelpMessage ="Work item id")]
        public int Id { get; set; }

        public GetItemCmdlet()
        {
            base.AddCommand(new GetItemById(this));
        }

        protected override void ProcessRecord()
        {
            base.ProcessCommands();
            base.ProcessRecord();
        }
    }
}
