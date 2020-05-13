using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Text;

namespace ProductivityTools.AzureDevOps.TimeTracking
{
    public interface INewStealing
    {
        string Name { get; set; }
        float Time { get; set; }
        SwitchParameter LeaveActive { get; set; }
    }
}
