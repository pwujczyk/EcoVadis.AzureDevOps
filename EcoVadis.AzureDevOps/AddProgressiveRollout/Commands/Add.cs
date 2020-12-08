using EcoVadis.AzureDevOps.App;
using EcoVadis.AzureDevOps.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace EcoVadis.AzureDevOps.AddProgressiveRollout
{
    public class Add : CommandBase<AddProgressiveRolloutCmdlet>
    {
        private readonly string Activity;
        public Add(AddProgressiveRolloutCmdlet cmdlet, bool be) : base(cmdlet)
        {
            if (be)
            {
                this.Activity = "BE Development";
            }
            else
            {
                this.Activity = "FE Development";
            }
        }

        protected override bool Condition => true;

        protected override void Invoke()
        {
            BacklogManagmenetApp app = new BacklogManagmenetApp(TfsAddress, PAT, this.Cmdlet.WriteVerbose);
            app.AddProgressiveRollout(ProjectName, this.Cmdlet.UsId, this.Activity, this.Cmdlet.Silent.IsPresent);
        }
    }
}
