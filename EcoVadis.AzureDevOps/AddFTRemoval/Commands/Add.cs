using EcoVadis.AzureDevOps.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace EcoVadis.AzureDevOps.AddFTRemoval.Commands
{
    class Add : CommandBase<AddFTRemovalCmdlet>
    {
        public Add(AddFTRemovalCmdlet cmdlet) : base(cmdlet)
        {
        }

        protected override bool Condition => true;

        protected override void Invoke()
        {
            this.Cmdlet.WriteVerbose("Hello from Add FT Removal");
        }
    }
}
