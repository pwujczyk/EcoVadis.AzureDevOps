﻿using EcoVadis.AzureDevOps.Facade;
using Microsoft.TeamFoundation.TestManagement.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EcoVadis.AzureDevOps.App
{
    public class BacklogManagmenetApp
    {
        private readonly string TfsUrl;
        private readonly string PAT;
        private BacklogManagement TFS;
        private TFS TFS2;
        private string BacklogAN = "faf2fea2-0224-4966-962c-4e8efea77609";
        private Action<string> Verbose;

        public BacklogManagmenetApp(string tfsUrl, string pat, Action<string> verbose)
        {
            this.TfsUrl = tfsUrl;
            this.PAT = pat;
            this.TFS = new BacklogManagement(this.TfsUrl, this.PAT, verbose);
            this.TFS2 = new TFS(this.TfsUrl, this.PAT);
            this.Verbose = verbose;
        }

        public async void MoveElementsToNext(int targetSprint, int fromStackRank)
        {
            var result = this.TFS.GetBacklog(BacklogAN);
            Verbose($"Found {result.UserStories.Count} in Backlog");
            foreach (var us in result.UserStories.OrderBy(x => x.StackRank))
            {
                // if (us.Id == 83434 || us.Id == 84332) continue;
                Verbose($"UserStory {us.Id} \t has stack rank {us.StackRank}");
                if (us.StackRank >= fromStackRank)
                {
                    foreach (var element in us.WorkItems)
                    {
                        await this.TFS.UpdateIterationPath(element.Id, targetSprint);

                        foreach (var subelement in element.WorkItems)
                        {
                            await this.TFS.UpdateIterationPath(subelement.Id, targetSprint);
                        }
                    }

                    Verbose($"UserStory {us.Id} \t moved to sprint {targetSprint}");
                    await this.TFS.UpdateIterationPath(us.Id, targetSprint);
                }
            }
        }

        public void GetEstimations()
        {
            float feEstimation = 0, beEstimation = 0;
            var result = TFS.GetBacklog(BacklogAN);
            foreach (var us in result.UserStories.OrderBy(x => x.StackRank))
            {
                foreach (var element in us.WorkItems)
                {
                    if (element.Activity == "FE Development")
                    {
                        feEstimation += element.Estimation;
                    }

                    if (element.Activity == "BE Development")
                    {
                        beEstimation += element.Estimation;
                    }
                }
                Console.WriteLine($"FE Estimation: {feEstimation}, BE Estimation: {beEstimation} - UserStory: {us.Title}");
            }
        }

        public async void SetIsPlanned(bool value)
        {
            var backlog = TFS.GetBacklog(BacklogAN);
            foreach (var us in backlog.UserStories)
            {
                foreach (var task in us.WorkItems)
                {
                    if (task.Type == "Eco Task")
                    {
                        await TFS.UpdateIsPlanned(task.Id, value);
                    }
                    foreach (var subtask in task.WorkItems)
                    {
                        if (task.Type == "Eco Task")
                        {
                            await TFS.UpdateIsPlanned(subtask.Id, value);
                        }
                    }
                }
            }
        }

        public void AddProgressiveRollout(string projectName, int usId, string activity, bool silent)
        {
            CreateTask(projectName, usId, "Progressive rollout", activity, silent);
        }

        public void CreateTask(string projectName, int parentUS, string title, string activity, bool silent)
        {
            var stealingsUS = TFS2.GetWorkItemWithRelations(parentUS);
            foreach (var link in stealingsUS.Relations)
            {
                int usid = int.Parse(link.Url.Split('/').Last());
                var task = TFS2.GetWorkItemWithRelations(usid);
                if (task.Fields["System.Title"].ToString() == title && task.Fields["Microsoft.VSTS.Common.Activity"].ToString() == activity)
                {
                    if (silent)
                    {
                        return;
                    }
                    throw new Exception($"Task with this name {title} and activity {activity} already exists");
                }
            }

            Dictionary<string, object> fields = new Dictionary<string, object>();
            fields.Add("Title", title);
            fields.Add("Activity", activity);
            fields.Add("Priority", 1);
            // fields.Add("System.AssignedTo", username);
            fields.Add("System.AreaPath", stealingsUS.Fields["System.AreaPath"]);
            fields.Add("System.IterationPath", stealingsUS.Fields["System.IterationPath"]);
            //fields.Add("Microsoft.VSTS.Scheduling.CompletedWork", time);

            var item = TFS2.CreateWorkItem(projectName, "Eco Task", fields);

            TFS2.AddParentLink(item.Id.Value, parentUS);

            fields.Clear();
            Console.WriteLine(item.Url);
        }
    }
}
