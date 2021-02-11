using EcoVadis.AzureDevOps.App.Facade;
using EcoVadis.AzureDevOps.Facade.Model;
using Microsoft.TeamFoundation.Build.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi;
using Microsoft.VisualStudio.Services.WebApi.Patch;
using Microsoft.VisualStudio.Services.WebApi.Patch.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EcoVadis.AzureDevOps.Facade
{
    public class BacklogManagement
    {
        static WorkItemTrackingHttpClient WitClient;
        Action<string> Verbose;

        public BacklogManagement(string tfsAddress, string personalAccessToken, Action<string> verbose)
        {
            VssConnection connection = new VssConnection(new Uri(tfsAddress), new VssBasicCredential(string.Empty, personalAccessToken));
            WitClient = connection.GetClient<WorkItemTrackingHttpClient>();
            this.Verbose = verbose;
        }

        public async Task UpdateIterationPath(int id, int iterationPath)
        {
            Dictionary<string, object> fields = new Dictionary<string, object>();

            fields.Add("System.IterationPath", $"EcoVadisApp\\Sprint {iterationPath}");
            await UpdateElement(id, fields);
        }

        public async Task UpdateIsPlanned(int id, bool isPlanned)
        {
            Dictionary<string, object> fields = new Dictionary<string, object>();

            fields.Add("Ecovadis.IsPlanned", isPlanned.ToString());
            await UpdateElement(id, fields);
        }

        public async Task UpdateElement(int id, Dictionary<string, object> fields)
        {
            JsonPatchDocument patchDocument = new JsonPatchDocument();

            foreach (var key in fields.Keys)
                patchDocument.Add(new JsonPatchOperation()
                {
                    Operation = Operation.Replace,
                    Path = "/fields/" + key,
                    Value = fields[key]
                });

            var x = await WitClient.UpdateWorkItemAsync(patchDocument, id);
        }

        public string GetCurrentSprint(string queryId)
        {
            Verbose("Loading Backlog to structure");
            var items = WitClient.QueryByIdAsync(new Guid(queryId)).Result;
            foreach(var element in items.WorkItemRelations)
            {
                if (element.Source != null)
                {
                    var targetElement = WitClient.GetWorkItemAsync(element.Source.Id, expand: WorkItemExpand.All).Result;

                    if (targetElement.Fields["System.WorkItemType"].ToString() == "User Story")
                    {
                        var r=targetElement.Fields["System.IterationPath"];
                        return r.ToString();
                    }
                }
            }
            throw new Exception("Missing iteration path");
        }

        public Backlog GetBacklog(string queryId, bool withBugs)
        {
            var result = new Backlog();

            Action<WorkItemReference> AddElement = (targetElementLink) =>
            {
                if (targetElementLink == null) return;
                var targetElement = WitClient.GetWorkItemAsync(targetElementLink.Id, expand: WorkItemExpand.All).Result;

                if (targetElement.Fields["System.WorkItemType"].ToString() == "User Story")
                {
                    result.AddUserStory(targetElement);
                }

                if (targetElement.Fields["System.WorkItemType"].ToString() == "Eco Task")
                {
                    result.AddWorkItem(targetElement);
                }

                if (withBugs && targetElement.Fields["System.WorkItemType"].ToString() == "eco Bug")
                {
                    result.AddWorkItem(targetElement);
                }
            };

            Verbose("Loading Backlog to structure");
            var items = WitClient.QueryByIdAsync(new Guid(queryId)).Result;

            items.WorkItemRelations.ForEach(item =>
            {
                AddElement(item.Target);
                AddElement(item.Source);
            });

            //it cannot be paraller because sometimes we can add task without adding pant
            //Parallel.ForEach(items.WorkItemRelations, item =>
            // {
            //     AddElement(item.Target);
            //     AddElement(item.Source);
            // });

            return result;
        }

    }
}
