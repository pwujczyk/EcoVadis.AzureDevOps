using EcoVadis.AzureDevOps.Facade;
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

        public BacklogManagmenetApp(string tfsUrl, string pat)
        {
            this.TfsUrl = tfsUrl;
            this.PAT = pat;
            this.TFS = new BacklogManagement(this.TfsUrl, this.PAT);
        }

        public void MoveElementsToNext(int targetSprint, int fromStackRank)
        {
            var result = this.TFS.GetBacklog(BacklogAN);
            foreach (var us in result.UserStories)
            {
                if (us.Id == 83434 || us.Id == 84332) continue;
                if (us.StackRank >= fromStackRank)
                {
                    foreach (var element in us.WorkItems)
                    {
                        this.TFS.UpdateIterationPath(element.Id, targetSprint);
                    }

                    this.TFS.UpdateIterationPath(us.Id, targetSprint);
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

        public void SetIsPlanned(bool value)
        {
            var backlog = TFS.GetBacklog(BacklogAN);
            foreach (var us in backlog.UserStories)
            {
                foreach (var task in us.WorkItems)
                {
                    if (task.Type=="Eco Task")
                    {
                        TFS.UpdateIsPlanned(task.Id, value);
                    }
                }
            }
        }
    }
}
