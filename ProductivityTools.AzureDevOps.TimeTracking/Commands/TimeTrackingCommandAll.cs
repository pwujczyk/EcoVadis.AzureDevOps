using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityTools.AzureDevOps.TimeTracking.Commands
{
    public class TimeTrackingCommandAll : PSCmdlet.PSCommandPT<TimeTrackingCmdlet>
    {
        public TimeTrackingCommandAll(TimeTrackingCmdlet cmdletType) : base(cmdletType)
        {
        }

        protected override bool Condition => true;

        protected override void Invoke()
        {
            Console.WriteLine("Hello from TimeTrackingCommandAll");
        }
    }
}
