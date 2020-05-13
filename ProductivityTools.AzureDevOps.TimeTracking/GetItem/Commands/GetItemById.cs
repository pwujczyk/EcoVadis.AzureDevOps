using ProductivityTools.AzureDevOps.TimeTracking.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityTools.AzureDevOps.TimeTracking.GetItem.Commands
{
    public class GetItemById : CommandBase<GetItemCmdlet>
    {
        public GetItemById(GetItemCmdlet cmdletType) : base(cmdletType)
        {
        }

        protected override bool Condition => true;

        protected override void Invoke()
        {
            var x=base.App.GetItemDetails(this.Cmdlet.Id);
            var dump = ObjectDumper.Dump(x);
            Console.WriteLine(dump);
        }
    }
}
