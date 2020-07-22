using EcoVadis.AzureDevOps.App.Facade;
using EcoVadis.AzureDevOps.Facade.Model;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi;
using Microsoft.VisualStudio.Services.WebApi.Patch;
using Microsoft.VisualStudio.Services.WebApi.Patch.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Reflection;
using System.Text;

namespace EcoVadis.AzureDevOps.Facade
{
    public class BacklogManagement
    {
        static WorkItemTrackingHttpClient WitClient;

        public BacklogManagement(string tfsAddress, string personalAccessToken)
        {
            VssConnection connection = new VssConnection(new Uri(tfsAddress), new VssBasicCredential(string.Empty, personalAccessToken));
            WitClient = connection.GetClient<WorkItemTrackingHttpClient>();
        }

        public void MoveItemsToSprint(int targetSprint, int fromStackRank)
        {
            var result = GetBacklog();
            foreach (var us in result.UserStories)
            {
                if (us.StackRank >= fromStackRank)
                {
                    foreach (var element in us.WorkItems)
                    {
                        UpdateIterationPath(element.Id, targetSprint);
                    }
                    UpdateIterationPath(us.Id, targetSprint);
                }
            }
        }

        private void UpdateIterationPath(int id, int iterationPath)
        {
            if (id == 83434 || id == 84332) return;
            Dictionary<string, object> fields = new Dictionary<string, object>();

            fields.Add("System.IterationPath", $"EcoVadisApp\\Sprint {iterationPath}");
            UpdateElement(id, fields);

        }



        public Backlog GetBacklog()
        {
            var result = new Backlog();

            var items = WitClient.QueryByIdAsync(new Guid("faf2fea2-0224-4966-962c-4e8efea77609")).Result;
            foreach (var item in items.WorkItemRelations)
            {
                AddElement(result, item.Target);
                AddElement(result, item.Source);
                //Dictionary<string, object> fields = new Dictionary<string, object>();
                //Console.WriteLine(targetElement.Fields["System.Title"]);
                //fields.Add("System.Title", "dd");
                //UpdateElement(targetElement.Id.Value, fields);


            }
            return result;
        }

        private static void AddElement(Backlog result, WorkItemReference targetElementLink)
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
        }

        public void UpdateElement(int id, Dictionary<string, object> fields)
        {
            JsonPatchDocument patchDocument = new JsonPatchDocument();

            foreach (var key in fields.Keys)
                patchDocument.Add(new JsonPatchOperation()
                {
                    Operation = Operation.Replace,
                    Path = "/fields/" + key,
                    Value = fields[key]
                });

            var x = WitClient.UpdateWorkItemAsync(patchDocument, id).Result;
        }
    }
}
