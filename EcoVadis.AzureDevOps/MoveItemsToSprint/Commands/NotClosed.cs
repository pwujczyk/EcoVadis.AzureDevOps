using EcoVadis.AzureDevOps.App;
using EcoVadis.AzureDevOps.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace EcoVadis.AzureDevOps.MoveItemsToSprint.Commands
{
    public class NotClosed : CommandBase<MoveItemsToSprintCmdlet>
    {
        protected override bool Condition => this.Cmdlet.NotClosed.IsPresent;

        public NotClosed(MoveItemsToSprintCmdlet cmdlet) : base(cmdlet) { }

        protected override void Invoke()
        {
            BacklogManagmenetApp app = new BacklogManagmenetApp(TfsAddress, PAT, this.Cmdlet.WriteVerbose);
            app.MoveNotClosedElementsToNext(this.Cmdlet.TargetSprint);
        }
    }
}
