//using Microsoft.TeamFoundation.Work.WebApi;
using EcoVadis.AzureDevOps.App;
using EcoVadis.AzureDevOps.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace EcoVadis.AzureDevOps.Commands
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
            this.Cmdlet.WriteVerbose($"ProjectName {ProjectName}");
            this.Cmdlet.WriteVerbose($"UserName {UserName}");
            this.Cmdlet.WriteVerbose($"Name {this.Cmdlet.Name}");
            this.Cmdlet.WriteVerbose($"Time {this.Cmdlet.Time}");
            this.Cmdlet.WriteVerbose($"Activity {this.Activity}");
            this.Cmdlet.WriteVerbose($"LeaveActive {this.Cmdlet.LeaveActive}");
            base.App.CreateStealing(ProjectName, UserName, this.Cmdlet.Name, this.Cmdlet.Time, this.Activity, this.Cmdlet.LeaveActive);
        }
    }
}
