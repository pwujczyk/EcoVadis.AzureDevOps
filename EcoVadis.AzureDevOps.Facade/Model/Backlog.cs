using EcoVadis.AzureDevOps.App.Facade;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EcoVadis.AzureDevOps.Facade.Model
{
    public class Backlog
    {
        public List<UserStory> UserStories { get; set; }

        public Backlog()
        {
            this.UserStories = new List<UserStory>();
        }

        public void AddUserStory(WorkItem workItem)
        {
            UserStory us = new UserStory();
            us.Id = workItem.Id.Value;
            us.Title = workItem.Fields["System.Title"].ToString();
            us.Iteration = workItem.Fields["System.IterationPath"].ToString();
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

        public void AddWorkItem(WorkItem workitem)
        {
            int parentid = -1;
            foreach (var x in workitem.Relations)
            {
                if (x.Rel == "System.LinkTypes.Hierarchy-Reverse")
                {
                    parentid = int.Parse(x.Url.Substring(x.Url.LastIndexOf("/") + 1));
                }
            }

            WorkItemElement element = new WorkItemElement();
            element.Id = workitem.Id.Value;
            element.Title = workitem.Fields["System.Title"].ToString();
            element.Iteration = workitem.Fields["System.IterationPath"].ToString();
            element.Type= workitem.Fields["System.WorkItemType"].ToString();


            if (workitem.Fields.ContainsKey("Microsoft.VSTS.Scheduling.OriginalEstimate"))
            {
                element.Estimation = float.Parse(workitem.Fields["Microsoft.VSTS.Scheduling.OriginalEstimate"].ToString());
            }
            element.Activity = workitem.Fields["Microsoft.VSTS.Common.Activity"].ToString();


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
