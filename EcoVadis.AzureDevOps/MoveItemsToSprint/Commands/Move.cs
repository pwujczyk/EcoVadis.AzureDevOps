﻿using EcoVadis.AzureDevOps.App;
using EcoVadis.AzureDevOps.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace EcoVadis.AzureDevOps.MoveItemsToSprint.Commands
{
    public class Move : CommandBase<MoveItemsToSprintCmdlet>
    {
        protected override bool Condition => true;

        public Move(MoveItemsToSprintCmdlet cmdlet) : base(cmdlet) { }

        protected override void Invoke()
        {
            BacklogManagmenetApp app = new BacklogManagmenetApp(TfsAddress, PAT, this.Cmdlet.WriteVerbose);
            app.MoveElementsToNext(this.Cmdlet.TargetSprint, this.Cmdlet.FromStackRank);
        }
    }
}
