using EcoVadis.AzureDevOps.App;
using EcoVadis.AzureDevOps.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace EcoVadis.AzureDevOps.AddTesting.Commands
{
    public class Add : CommandBase<AddTestingCmdlet>
    {
        private readonly string Activity="Testing";
        public Add(AddTestingCmdlet cmdlet, bool be) : base(cmdlet)
        {
           
        }

        protected override bool Condition => true;

        protected override void Invoke()
        {
            BacklogManagmenetApp app = new BacklogManagmenetApp(TfsAddress, PAT, this.Cmdlet.WriteVerbose);
            app.AddTesting(ProjectName, this.Cmdlet.UsId, this.Activity, this.Cmdlet.Silent.IsPresent);
        }
    }
}
