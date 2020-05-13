using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityTools.AzureDevOps.TimeTracking.GetItem.Commands
{
    public class GetItemById : PSCmdlet.PSCommandPT<GetItemCmdlet>
    {
        public GetItemById(GetItemCmdlet cmdletType) : base(cmdletType)
        {
        }

        protected override bool Condition => true;

        protected override void Invoke()
        {
           
        }
    }
}
