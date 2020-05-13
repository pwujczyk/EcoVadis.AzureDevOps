using Microsoft.TeamFoundation.Work.WebApi;
using ProductivityTools.AzureDevOps.TimeTracking.App;
using ProductivityTools.AzureDevOps.TimeTracking.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityTools.AzureDevOps.TimeTracking.Commands
{
    public class ReportStealingCommand : CommandBase<NewStealing>
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


            TimeTrackingApp app = new TimeTrackingApp(TfsAddress, PAT);
            app.CreateStealing(ProjectName, UserName, this.Cmdlet.Name, this.Activity, this.Cmdlet.LeaveActive);
        }
    }
}
