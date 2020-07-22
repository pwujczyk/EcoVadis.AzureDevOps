using System;
using System.Collections.Generic;
using System.Text;

namespace EcoVadis.AzureDevOps.App.Facade
{
    public class UserStory
    {
        public string Title { get; set; }
        public int Id { get; set; }
        public int StackRank { get; set; }
        public string Iteration { get; set; }

        public  UserStory()
        {
            this.WorkItems = new List<WorkItemElement>();
        }

        public List<WorkItemElement> WorkItems { get; set; }
    }
}
