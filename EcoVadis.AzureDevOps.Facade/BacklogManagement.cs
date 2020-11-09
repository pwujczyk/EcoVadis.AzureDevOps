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

        public void UpdateIterationPath(int id, int iterationPath)
        {
            Dictionary<string, object> fields = new Dictionary<string, object>();

            fields.Add("System.IterationPath", $"EcoVadisApp\\Sprint {iterationPath}");
            UpdateElement(id, fields);
        }

        public void UpdateIsPlanned(int id, bool isPlanned)
        {
            Dictionary<string, object> fields = new Dictionary<string, object>();

            fields.Add("Ecovadis.IsPlanned", isPlanned.ToString());
            UpdateElement(id, fields);
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

        public async Task<Backlog> GetBacklog(string queryId)
        {
            var result = new Backlog();

            Action<WorkItemReference> AddElement = async (targetElementLink) =>
           {
               if (targetElementLink == null) return;
               var targetElement = await WitClient.GetWorkItemAsync(targetElementLink.Id, expand: WorkItemExpand.All);

               if (targetElement.Fields["System.WorkItemType"].ToString() == "User Story")
               {
                   result.AddUserStory(targetElement);
               }

               if (targetElement.Fields["System.WorkItemType"].ToString() == "Eco Task")
               {
                   result.AddWorkItem(targetElement);
               }
           };

            Verbose("Loading Backlog to structure");
            var items = WitClient.QueryByIdAsync(new Guid(queryId)).Result;
            Parallel.ForEach(items.WorkItemRelations, item =>
             {
                 AddElement(item.Target);
                 AddElement(item.Source);
             });
            return result;
        }

    }
}
