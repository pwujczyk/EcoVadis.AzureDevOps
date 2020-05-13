using ProductivityTools.AzureDevOps.TimeTracking.App;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityTools.AzureDevOps.TimeTracking.Commands
{
    public class ReportStealingCommand : PSCmdlet.PSCommandPT<ReportStealingCmdlet>
    {
        private const string ProjectName = "EcoVadisApp";
        public ReportStealingCommand(ReportStealingCmdlet cmdletType) : base(cmdletType)
        {
        }

        protected override bool Condition => true;

        protected override void Invoke()
        {
            var tfsAddress=Environment.GetEnvironmentVariable("TTTFSAddress");
            var pat = Environment.GetEnvironmentVariable("TTPAT");
            var username = Environment.GetEnvironmentVariable("TTuserName");

            TimeTrackingApp app = new TimeTrackingApp(tfsAddress, pat);
            app.CreateStealing(ProjectName, username, this.Cmdlet.Name, "FE Development",this.Cmdlet.LeaveActive);
        }
    }
}
