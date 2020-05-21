using EcoVadis.AzureDevOps.GetItem.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Management.Automation;
using System.Text;
using ProductivityTools.PSCmdlet;
using EcoVadis.AzureDevOps.Base;

namespace EcoVadis.AzureDevOps.GetItem
{

    [Cmdlet(VerbsCommon.Get, "TFSItem")]
    [Description("Get item details")]
    public class GetItemCmdlet : CmdletBase
    {
        [Parameter(Mandatory = true, Position = 0, HelpMessage = "Work item id")]
        public int Id { get; set; }

        public GetItemCmdlet() { }

        protected override void BeginProcessing()
        {
            base.BeginProcessing();
            base.AddCommand(new GetItemById(this));
        }

        protected override void ProcessRecord()
        {
            base.ProcessCommands();
            base.ProcessRecord();
        }
    }
}
