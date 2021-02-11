using EcoVadis.AzureDevOps.App;
using EcoVadis.AzureDevOps.Base;
using EcoVadis.AzureDevOps.Facade;
using EcoVadis.AzureDevOps.Optimizely;
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
            BacklogManagmenetApp app = new BacklogManagmenetApp(TfsAddress, PAT, this.Cmdlet.WriteVerbose);

            this.Cmdlet.WriteVerbose("Hello from Add FT Removal");
            var featureFlags = new FeatureFlags();
            var flags = featureFlags.Get().Result;
            foreach (var flag in flags)
            {
                this.Cmdlet.WriteObject(flag);
                var usID = app.CreateUserStory(ProjectName, app.GetCurrentSprint(), "pawel", "fda", true);
                app.CreateTask(ProjectName, usID, "Frontend", "FE Activity", false);
            }
        }
    }
}
