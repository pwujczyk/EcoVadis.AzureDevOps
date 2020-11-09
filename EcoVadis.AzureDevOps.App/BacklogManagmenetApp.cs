using EcoVadis.AzureDevOps.Facade;
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
        private string BacklogAN = "faf2fea2-0224-4966-962c-4e8efea77609";
        private Action<string> Verbose;

        public BacklogManagmenetApp(string tfsUrl, string pat, Action<string> verbose)
        {
            this.TfsUrl = tfsUrl;
            this.PAT = pat;
            this.TFS = new BacklogManagement(this.TfsUrl, this.PAT, verbose);
            this.Verbose = verbose;
        }

        public async void MoveElementsToNext(int targetSprint, int fromStackRank)
        {
            var result = this.TFS.GetBacklog(BacklogAN);
            Verbose($"Found {result.UserStories.Count} in Backlog");
            foreach (var us in result.UserStories.OrderBy(x=>x.StackRank))
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
                    if (task.Type=="Eco Task")
                    {
                        await TFS.UpdateIsPlanned(task.Id, value);
                    }
                    foreach(var subtask in task.WorkItems)
                    {
                        if (task.Type == "Eco Task")
                        {
                            await TFS.UpdateIsPlanned(subtask.Id, value);
                        }
                    }
                }
            }
        }
    }
}
