﻿using EcoVadis.AzureDevOps.App.Facade;
using EcoVadis.AzureDevOps.Facade;
using Microsoft.TeamFoundation.TestManagement.WebApi;
using Microsoft.VisualStudio.Services.Common;
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
        private string FTRemoval = "7056c08b-dd84-48a7-8d37-9eb22d404518";
        private Action<string> Verbose;

        public BacklogManagmenetApp(string tfsUrl, string pat, Action<string> verbose)
        {
            this.TfsUrl = tfsUrl;
            this.PAT = pat;
            this.TFS = new BacklogManagement(this.TfsUrl, this.PAT, verbose);
            this.TFS2 = new TFS(this.TfsUrl, this.PAT);
            this.Verbose = verbose;
        }

        public async void MoveNotClosedElementsToNext(int targetSprint)
        {
            List<string> moveUsStatuses = new List<string> { "New", "Dev Active", "L2 Approved" };
            List<string> moveTasksStatuses = new List<string> { "New", "Active", "Code Review", "L2 Approved", "Blocked", "Draft" };
            List<string> cloneTasksStatuses = new List<string> { "Active", "Code Review" };

            var result = this.TFS.GetBacklog(BacklogAN, true);
            Verbose($"Found {result.UserStories.Count} in Backlog");
            foreach (var us in result.UserStories)
            {
                // if (us.Id == 83434 || us.Id == 84332) continue;
                Verbose($"UserStory {us.Id} - {us.Title} \t has status {us.Status}");

                foreach (var element in us.WorkItems)
                {
                    decimal completedWork = element.CompletedWork == null ? 0 : decimal.Parse(element.CompletedWork.ToString());
                    if (cloneTasksStatuses.Contains(element.Status) && completedWork > 0)
                    {
                        CloneElement(element);
                        RemoveCompleted(element);
                    }

                    if (moveTasksStatuses.Contains(element.Status))
                    {
                        await this.TFS.UpdateIterationPath(element.Id, targetSprint);
                    }

                    foreach (var subelement in element.WorkItems)
                    {
                        if (moveTasksStatuses.Contains(subelement.Status))
                        {
                            //subtask in second iteration
                            //CloneElement(element);
                            //RemoveCompleted(element);
                            await this.TFS.UpdateIterationPath(subelement.Id, targetSprint);
                        }
                    }
                }

                if (moveUsStatuses.Contains(us.Status))
                {
                    Verbose($"UserStory {us.Id} \t moved to sprint {targetSprint}");
                    await this.TFS.UpdateIterationPath(us.Id, targetSprint);
                }
            }
        }

        public string GetCurrentSprint()
        {
            var result = this.TFS.GetCurrentSprint(BacklogAN);
            return result;
        }

        public async void MoveElementsToNext(int targetSprint, int fromStackRank)
        {
            var result = this.TFS.GetBacklog(BacklogAN, false);
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
            var result = TFS.GetBacklog(BacklogAN, false);
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
            var backlog = TFS.GetBacklog(BacklogAN, false);
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

        public void AddTesting(string projectName, int usId, string activity, bool silent)
        {
            CreateTask(projectName, usId, "Testing", activity, silent);
            CreateTask(projectName, usId, "Test Case", activity, silent);
        }

        public void AddProgressiveRollout(string projectName, int usId, string activity, bool silent)
        {
            CreateTask(projectName, usId, "Progressive rollout", activity, silent);
        }

        private void RemoveCompleted(WorkItemElement element)
        {
            Dictionary<string, object> fields = new Dictionary<string, object>();
            fields.Add("Microsoft.VSTS.Scheduling.CompletedWork", 0);
            var r = TFS2.UpdateWorkItem(element.Id, fields);
        }

        public void CloneElement(WorkItemElement element)
        {
            Dictionary<string, object> fields = new Dictionary<string, object>();

            Action<string, object> addIfNotNull = (name, o) =>
              {
                  if (o != null)
                  {
                      fields.Add(name, o);
                  }
              };

            fields.Add("Title", element.Title);
            fields.Add("Activity", element.Activity);
            fields.Add("Priority", 1);
            fields.Add("System.AssignedTo", element.AssignedTo);
            fields.Add("System.AreaPath", element.AreaPath);
            fields.Add("System.IterationPath", element.Iteration);
            addIfNotNull("Microsoft.VSTS.Scheduling.CompletedWork", element.CompletedWork);
            addIfNotNull("Microsoft.VSTS.common.BugFoundOn", "Test");

            var item = TFS2.CreateWorkItem(element.Project, element.Type, fields);

            TFS2.AddParentLink(item.Id.Value, element.ParentElementId);
            fields.Clear();

            if (element.Type == "eco Bug")
            {
                fields.Add("State", "Resolved");
                fields.Add("Ecovadis.TargetRelease", "10.66");
                TFS2.UpdateWorkItem(item.Id.Value, fields);
                fields.Clear();
            }

            if (element.Type == "Eco Task")
            {
                fields.Add("State", "Active");
                TFS2.UpdateWorkItem(item.Id.Value, fields);
                fields.Clear();
            }
            fields.Add("State", "Closed");
            var r = TFS2.UpdateWorkItem(item.Id.Value, fields);
            Console.WriteLine(item.Url);
        }

        public void CreateTask(string projectName, int parentUsId, string title, string activity, bool silent, int? plannedWork = null)
        {
            var parentUs = TFS2.GetWorkItemWithRelations(parentUsId);
            if (parentUs.Relations != null)
            {
                foreach (var link in parentUs.Relations)
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
            }

            Dictionary<string, object> fields = new Dictionary<string, object>();
            fields.Add("Title", title);
            fields.Add("Activity", activity);
            fields.Add("Priority", 1);
            // fields.Add("System.AssignedTo", username);
            fields.Add("System.AreaPath", parentUs.Fields["System.AreaPath"]);
            fields.Add("System.IterationPath", parentUs.Fields["System.IterationPath"]);
            if (plannedWork != null)
            {
                fields.Add("Microsoft.VSTS.Scheduling.OriginalEstimate", plannedWork.Value);
            }
            //fields.Add("Microsoft.VSTS.Scheduling.CompletedWork", time);

            var item = TFS2.CreateWorkItem(projectName, "Eco Task", fields);

            TFS2.AddParentLink(item.Id.Value, parentUsId);

            fields.Clear();
            Console.WriteLine(item.Url);
        }

        public void CreateFTRemoval(string ProjectName, List<string> featureToggleList)
        {
            List<string> ftRemovalItems = TFS.GetUsNames(this.FTRemoval);
            string currentSprint = GetCurrentSprint();
            foreach (var featureToggle in featureToggleList)
            {
                string name = $"Removal of FT {featureToggle} - Technical";
                if (ftRemovalItems.Contains(name) == false)
                {
                    var technicalId = CreateUserStory(ProjectName, currentSprint, name);
                    CreateTask(ProjectName, technicalId, "Frontend", "FE Development", false, 1);
                    CreateTask(ProjectName, technicalId, "Backend", "BE Development", false, 1);

                    name = $"Removal of FT {featureToggle} - Business";
                    var businessID = CreateUserStory(ProjectName, currentSprint, name);

                    TFS2.AddRelated(technicalId, businessID);
                }
            }
        }

        public int CreateUserStory(string projectName, string sprint, string title)
        {
            Dictionary<string, object> fields = new Dictionary<string, object>();
            fields.Add("Title", title);
            fields.Add("System.Tags", "FTRemoval");
            fields.Add("System.State", "New");
            fields.Add("System.AreaPath", @"EcoVadisApp\Angry Nerds");
            fields.Add("System.IterationPath", sprint);

            var item = TFS2.CreateWorkItem(projectName, "User Story", fields);
            return item.Id.Value;
        }
    }
}
