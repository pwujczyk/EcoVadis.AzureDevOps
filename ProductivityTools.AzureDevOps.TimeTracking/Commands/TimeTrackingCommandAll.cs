using Microsoft.TeamFoundation.Work.WebApi;
using ProductivityTools.AzureDevOps.TimeTracking.App;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityTools.AzureDevOps.TimeTracking.Commands
{
    public class ReportStealingCommand : PSCmdlet.PSCommandPT<NewStealing>
    {
        private const string ProjectName = "EcoVadisApp";
        private readonly string Activity;

        public ReportStealingCommand(NewStealing cmdletType, bool bestealing) : base(cmdletType)
        {
            if (bestealing)
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
            var tfsAddress = Environment.GetEnvironmentVariable("TTTFSAddress");
            var pat = Environment.GetEnvironmentVariable("TTPAT");
            var username = Environment.GetEnvironmentVariable("TTuserName");

            TimeTrackingApp app = new TimeTrackingApp(tfsAddress, pat);
            app.CreateStealing(ProjectName, username, this.Cmdlet.Name, this.Activity, this.Cmdlet.LeaveActive);
        }
    }
}
