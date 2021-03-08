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

            List<string> anFlags = new List<string>();
            foreach(var flag in flags)
            {
                
                if (flag.Name.StartsWith("AN") || flag.Name.StartsWith("angry_nerds"))
                {
                    this.Cmdlet.WriteVerbose($"Processing AN Flag {flag.Name}");
                    anFlags.Add(flag.Name);
                }
            }

            app.CreateFTRemoval(ProjectName, anFlags);

        }
    }
}
