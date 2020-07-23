using EcoVadis.AzureDevOps.App;
using EcoVadis.AzureDevOps.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace EcoVadis.AzureDevOps.SetIsPlanned.Commands
{
    public class Set : CommandBase<SetIsPlannedCmdlet>
    {
        public Set(SetIsPlannedCmdlet cmdlet) : base(cmdlet)
        {
        }

        protected override bool Condition => true;

        protected override void Invoke()
        {
            BacklogManagmenetApp app = new BacklogManagmenetApp(TfsAddress, PAT);
            app.SetIsPlanned(this.Cmdlet.Value);
        }
    }
}
