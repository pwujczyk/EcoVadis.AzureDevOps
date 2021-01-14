using EcoVadis.AzureDevOps.App.Facade;
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
            List<string> moveUsStatuses = new List<string> { "Dev Active", "L2 Approved" };
            List<string> moveTasksStatuses = new List<string> { "New", "Active", "Code Review" };

            var result = this.TFS.GetBacklog(BacklogAN, true);
            Verbose($"Found {result.UserStories.Count} in Backlog");
            foreach (var us in result.UserStories)
            {
                // if (us.Id == 83434 || us.Id == 84332) continue;
                Verbose($"UserStory {us.Id} - {us.Title} \t has status {us.Status}");
                if (moveUsStatuses.Contains(us.Status))
                {
                    Verbose($"UserStory {us.Id} \t moved to sprint {targetSprint}");
                    await this.TFS.UpdateIterationPath(us.Id, targetSprint);
                }

                foreach (var element in us.WorkItems)
                {
                    if (moveTasksStatuses.Contains(element.Status))
                    {
                        CloneElement(element);
                        await this.TFS.UpdateIterationPath(element.Id, targetSprint);
                    }

                    foreach (var subelement in element.WorkItems)
                    {
                        if (moveTasksStatuses.Contains(subelement.Status))
                        {
                            await this.TFS.UpdateIterationPath(subelement.Id, targetSprint);
                        }
                    }
                }

            }
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

        public void CloneElement(WorkItemElement element)
        {

            Dictionary<string, object> fields = new Dictionary<string, object>();
            fields.Add("Title", element.Title);
            fields.Add("Activity", element.Activity);
            fields.Add("Priority", 1);
            fields.Add("System.AssignedTo", element.AssignedTo);
            fields.Add("System.AreaPath", element.AreaPath);
            fields.Add("System.IterationPath", element.Iteration);
            fields.Add("Microsoft.VSTS.Scheduling.CompletedWork", element.CompletedWork);
            fields.Add("Microsoft.VSTS.common.BugFoundOn", element.FoundOn);

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
            fields.Add("State", "Closed");
            var r = TFS2.UpdateWorkItem(item.Id.Value, fields);
            Console.WriteLine(item.Url);
        }

        public void CreateTask(string projectName, int parentUsId, string title, string activity, bool silent)
        {
            var parentUs = TFS2.GetWorkItemWithRelations(parentUsId);
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

            Dictionary<string, object> fields = new Dictionary<string, object>();
            fields.Add("Title", title);
            fields.Add("Activity", activity);
            fields.Add("Priority", 1);
            // fields.Add("System.AssignedTo", username);
            fields.Add("System.AreaPath", parentUs.Fields["System.AreaPath"]);
            fields.Add("System.IterationPath", parentUs.Fields["System.IterationPath"]);
            //fields.Add("Microsoft.VSTS.Scheduling.CompletedWork", time);

            var item = TFS2.CreateWorkItem(projectName, "Eco Task", fields);

            TFS2.AddParentLink(item.Id.Value, parentUsId);

            fields.Clear();
            Console.WriteLine(item.Url);
        }
    }
}
