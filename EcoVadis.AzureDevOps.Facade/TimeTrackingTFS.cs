using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.Graph.Client;
using Microsoft.VisualStudio.Services.WebApi;
using Microsoft.VisualStudio.Services.WebApi.Patch;
using Microsoft.VisualStudio.Services.WebApi.Patch.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EcoVadis.AzureDevOps.Facade
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


        public WorkItem AddParentLink(int id, int parentid)
        {
            WorkItem wi = WitClient.GetWorkItemAsync(id, expand: WorkItemExpand.Relations).Result;
            bool parentExists = false;

            // check existing parent link
            if (wi.Relations != null)
                if (wi.Relations.Where(x => x.Rel == RelConstants.ParentRefStr).FirstOrDefault() != null)
                    parentExists = true;

            if (!parentExists)
            {
                WorkItem parentWi = WitClient.GetWorkItemAsync(parentid).Result; // get parent to retrieve its url

                Dictionary<string, object> fields = new Dictionary<string, object>();

                fields.Add(RelConstants.LinkKeyForDict + RelConstants.ParentRefStr + parentWi.Id, // to use as unique key
                CreateNewLinkObject(RelConstants.ParentRefStr, parentWi.Url, "Parent " + parentWi.Id));

                return SubmitWorkItem(fields, id);
            }

            Console.WriteLine("Work Item " + id + " contains a parent link");

            return null;
        }

        public void AddRelated(int source, int target)
        {
            WorkItem parentWi = WitClient.GetWorkItemAsync(target).Result; // get parent to retrieve its url

            Dictionary<string, object> fields = new Dictionary<string, object>();

            fields.Add(RelConstants.LinkKeyForDict + RelConstants.RelatedRefStr + target, // to use as unique key
            CreateNewLinkObject(RelConstants.RelatedRefStr, parentWi.Url, "Parent " + parentWi.Id));

            SubmitWorkItem(fields, source);
        }

        object CreateNewLinkObject(string relName, string RelUrl, string Comment = null, bool IsLocked = false)
        {
            return new
            {
                rel = relName,
                url = RelUrl,
                attributes = new
                {
                    comment = Comment,
                    isLocked = IsLocked // you must be an administrator to lock a link
                }
            };
        }

        public WorkItem GetWorkItemWithRelations(int Id)
        {
            return WitClient.GetWorkItemAsync(Id, expand: WorkItemExpand.Relations).Result;
        }

        static WorkItem SubmitWorkItem(Dictionary<string, object> Fields, int WIId = 0, string TeamProjectName = "", string WorkItemTypeName = "")
        {
            JsonPatchDocument patchDocument = new JsonPatchDocument();

            foreach (var key in Fields.Keys)
                patchDocument.Add(new JsonPatchOperation()
                {
                    Operation = Operation.Add,
                    Path = (key.StartsWith(RelConstants.LinkKeyForDict)) ? "/relations/-" : "/fields/" + key,
                    Value = Fields[key]
                });

            if (WIId == 0) return WitClient.CreateWorkItemAsync(patchDocument, TeamProjectName, WorkItemTypeName).Result; // create new work item

            return WitClient.UpdateWorkItemAsync(patchDocument, WIId).Result; // return updated work item
        }

        public WorkItem UpdateWorkItem(int id, Dictionary<string, object> fields)
        {
            JsonPatchDocument patchDocument = new JsonPatchDocument();

            foreach (var key in fields.Keys)
                patchDocument.Add(new JsonPatchOperation()
                {
                    Operation = Operation.Add,
                    Path = "/fields/" + key,
                    Value = fields[key]
                });

            return WitClient.UpdateWorkItemAsync(patchDocument, id).Result;
        }

    }
}
