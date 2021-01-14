using EcoVadis.AzureDevOps.App.Facade;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EcoVadis.AzureDevOps.Facade.Model
{
    public class Backlog
    {
        public ConcurrentBag<UserStory> UserStories { get; set; }

        public Backlog()
        {
            this.UserStories = new ConcurrentBag<UserStory>();
        }



        public void AddUserStory(WorkItem workItem)
        {
            UserStory us = new UserStory();
            us.Id = workItem.Id.Value;
            us.Title = workItem.Fields["System.Title"].ToString();
            us.Iteration = workItem.Fields["System.IterationPath"].ToString();
            us.Status = workItem.Fields["System.State"].ToString();
            if (workItem.Fields.ContainsKey("Microsoft.VSTS.Common.StackRank"))
            {
                us.StackRank = int.Parse(workItem.Fields["Microsoft.VSTS.Common.StackRank"].ToString());
            }
            else
            {
                us.StackRank = int.MaxValue;
            }

            if (!this.UserStories.Any(x => x.Id == us.Id))
            {
                this.UserStories.Add(us);
            }
        }

        private object GetField(WorkItem workItem, string fieldName)
        {
            if (workItem.Fields.ContainsKey(fieldName))
            {
                return workItem.Fields[fieldName];
            }
            else
            {
                return null;
            }

        }

        public void AddWorkItem(WorkItem workItem)
        {
            int parentid = -1;
            foreach (var x in workItem.Relations)
            {
                if (x.Rel == "System.LinkTypes.Hierarchy-Reverse")
                {
                    parentid = int.Parse(x.Url.Substring(x.Url.LastIndexOf("/") + 1));
                }
            }

            WorkItemElement element = new WorkItemElement();
            element.Id = workItem.Id.Value;
            element.Title = workItem.Fields["System.Title"].ToString();
            element.Iteration = workItem.Fields["System.IterationPath"].ToString();
            element.Type = workItem.Fields["System.WorkItemType"].ToString();
            element.Status = workItem.Fields["System.State"].ToString();
            element.AreaPath = workItem.Fields["System.AreaPath"].ToString();
            element.Project = workItem.Fields["System.TeamProject"].ToString();
            element.AssignedTo = GetField(workItem, "System.AssignedTo");
            element.CompletedWork = GetField(workItem, "Microsoft.VSTS.Scheduling.CompletedWork");
            element.FoundOn = GetField(workItem, "Microsoft.VSTS.common.BugFoundOn");
            element.ParentElementId = parentid;

            if (workItem.Fields.ContainsKey("Microsoft.VSTS.Scheduling.OriginalEstimate"))
            {
                element.Estimation = float.Parse(workItem.Fields["Microsoft.VSTS.Scheduling.OriginalEstimate"].ToString());
            }
            element.Activity = workItem.Fields.ContainsKey("Microsoft.VSTS.Common.Activity") ? workItem.Fields["Microsoft.VSTS.Common.Activity"].ToString() : string.Empty;


            foreach (var us in this.UserStories)
            {
                //pw: add hierachy
                if (us.Id == parentid)
                {
                    var x1 = us.WorkItems.FirstOrDefault(x => x.Id == element.Id);
                    if (x1 == null)
                    {
                        us.WorkItems.Add(element);
                    }
                }
                else
                {
                    foreach (var task in us.WorkItems)
                    {
                        if (task.Id == parentid)
                        {
                            task.WorkItems.Add(element);
                        }
                    }
                }
            }
        }
    }
}
