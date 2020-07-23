using EcoVadis.AzureDevOps.Facade;
using System;
using System.Collections.Generic;
using System.Dynamic;

namespace EcoVadis.AzureDevOps.App
{
    public class TimeTrackingApp
    {
        private readonly string TfsUrl;
        private readonly string PAT;

        private static int StealingId = 84332;
        private TFS TFS;

        public TimeTrackingApp(string tfsUrl, string pat)
        {
            this.TfsUrl = tfsUrl;
            this.PAT = pat;
            this.TFS = new TFS(this.TfsUrl, this.PAT);
        }

        public dynamic GetItemDetails(int id)
        {
            var item = this.TFS.GetWorkItemWithRelations(id);
            return item;
        }

        public void CreateStealing(string projectName, string username, string title, float time, string activity, bool leaveActive)
        {
            var stealingsUS = TFS.GetWorkItemWithRelations(StealingId);

            Dictionary<string, object> fields = new Dictionary<string, object>();
            fields.Add("Title", title);
            fields.Add("Activity", activity);
            fields.Add("Priority", 1);
            fields.Add("System.AssignedTo", username);
            fields.Add("System.AreaPath", stealingsUS.Fields["System.AreaPath"]);
            fields.Add("System.IterationPath", stealingsUS.Fields["System.IterationPath"]);
            fields.Add("Microsoft.VSTS.Scheduling.CompletedWork", time);

            var item = TFS.CreateWorkItem(projectName, "Eco Task", fields);

            TFS.AddParentLink(item.Id.Value, StealingId);

            fields.Clear();
            fields.Add("State", "Active");
            TFS.UpdateWorkItem(item.Id.Value, fields);

            if (leaveActive == false)
            {
                fields.Clear();
                fields.Add("State", "Closed");
                TFS.UpdateWorkItem(item.Id.Value, fields);
            }
        }
    }
}
