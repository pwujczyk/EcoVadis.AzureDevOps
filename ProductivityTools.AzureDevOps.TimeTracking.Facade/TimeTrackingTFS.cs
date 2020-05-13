using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi;
using Microsoft.VisualStudio.Services.WebApi.Patch;
using Microsoft.VisualStudio.Services.WebApi.Patch.Json;
using System;
using System.Collections.Generic;

namespace ProductivityTools.AzureDevOps.TimeTracking.Facade
{
    public class TFS
    {
        static WorkItemTrackingHttpClient WitClient;

        public TFS(string tfsAddress, string personalAccessToken)
        {
            VssConnection connection = new VssConnection(new Uri(tfsAddress), new VssBasicCredential(string.Empty, personalAccessToken));
            WitClient = connection.GetClient<WorkItemTrackingHttpClient>();
        }


        public WorkItem CreateWorkItem(string projectName, string workItemTypeName, Dictionary<string, object> fields)
        {
            JsonPatchDocument patchDocument = new JsonPatchDocument();

            foreach (var key in fields.Keys)
                patchDocument.Add(new JsonPatchOperation()
                {
                    Operation = Operation.Add,
                    Path = "/fields/" + key,
                    Value = fields[key]
                });

            return WitClient.CreateWorkItemAsync(patchDocument, projectName, workItemTypeName).Result;
        }
    }
}
