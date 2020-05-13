using ProductivityTools.AzureDevOps.TimeTracking.App;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityTools.AzureDevOps.TimeTracking.Commands
{
    public class ReportStealingCommand : PSCmdlet.PSCommandPT<ReportStealingCmdlet>
    {
        public ReportStealingCommand(ReportStealingCmdlet cmdletType) : base(cmdletType)
        {
        }

        protected override bool Condition => true;

        protected override void Invoke()
        {
            var tfsAddress=Environment.GetEnvironmentVariable("TFSAddress");
            var pat = Environment.GetEnvironmentVariable("PAT");
            TimeTrackingApp app = new TimeTrackingApp(tfsAddress, pat);
            app.CreateStealing("EcoVadisApp", "ToDelete123", "FE Development",true);
            Console.WriteLine("Hello from TimeTrackingCommandAll!X1");

            
        }
    }
}
