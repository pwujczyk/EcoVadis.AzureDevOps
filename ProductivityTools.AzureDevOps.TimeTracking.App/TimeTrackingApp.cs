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

        public TimeTrackingApp(string tfsUrl, string pat)
        {
            this.TfsUrl = tfsUrl;
            this.PAT = pat;
        }

        public void CreateEcoTask(string projectName, string title, string activity)
        {
            Dictionary<string, object> fields = new Dictionary<string, object>();
            fields.Add("Title", title);
            fields.Add("Activity", activity);
            fields.Add("Priority", 1);

            TFS tfs = new TFS(this.TfsUrl, this.PAT);
            var item= tfs.CreateWorkItem(projectName, "Eco Task", fields);
        }
    }
}
