using EcoVadis.AzureDevOps.App;
using EcoVadis.AzureDevOps.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace EcoVadis.AzureDevOps.AddProgressiveRollout
{
    public class Add : CommandBase<AddProgressiveRolloutCmdlet>
    {
        public Add(AddProgressiveRolloutCmdlet cmdlet) : base(cmdlet)
        {
        }

        protected override bool Condition => true;

        protected override void Invoke()
        {
            BacklogManagmenetApp app = new BacklogManagmenetApp(TfsAddress, PAT, this.Cmdlet.WriteVerbose);
            app.AddProgressiveRollout(ProjectName, this.Cmdlet.UsId);
        }
    }
}
