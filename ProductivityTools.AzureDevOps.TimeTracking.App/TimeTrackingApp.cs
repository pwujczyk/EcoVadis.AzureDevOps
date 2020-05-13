using ProductivityTools.AzureDevOps.TimeTracking.Facade;
using System;
using System.Collections.Generic;
using System.Dynamic;

namespace ProductivityTools.AzureDevOps.TimeTracking.App
{
    public class TimeTrackingApp
    {
        private readonly string TfsUrl;
        private readonly string PAT;

        private static int StealingId = 84332;

        public TimeTrackingApp(string tfsUrl, string pat)
        {
            this.TfsUrl = tfsUrl;
            this.PAT = pat;
        }

        public void CreateStealing(string projectName,string username, string title, string activity, bool leaveActive)
        {
            TFS tfs = new TFS(this.TfsUrl, this.PAT);
            var stealingsUS = tfs.GetWorkItemWithRelations(StealingId);

            Dictionary<string, object> fields = new Dictionary<string, object>();
            fields.Add("Title", title);
            fields.Add("Activity", activity);
            fields.Add("Priority", 1);

            fields.Add("System.AssignedTo", username);
            fields.Add("System.AreaPath", stealingsUS.Fields["System.AreaPath"]);

            fields.Add("System.IterationPath", stealingsUS.Fields["System.IterationPath"]);
            var item = tfs.CreateWorkItem(projectName, "Eco Task", fields);

            tfs.AddParentLink(item.Id.Value, StealingId);

            fields.Clear();
            fields.Add("State", "Active");
            tfs.UpdateWorkItem(item.Id.Value, fields);

            if (leaveActive == false)
            {
                fields.Clear();
                fields.Add("State", "Closed");
                tfs.UpdateWorkItem(item.Id.Value, fields);
            }
        }
    }
}
