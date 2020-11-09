using System;
using System.Collections.Generic;
using System.Text;

namespace EcoVadis.AzureDevOps.App.Facade
{
    public class WorkItemElement
    {
        public string Title { get; set; }
        public int Id { get; set; }
        public string Iteration { get; set; }
        public float Estimation { get; set; }
        public string Activity { get; set; }
        public string Type { get; set; }

        public List<WorkItemElement> WorkItems { get; set; }

        public WorkItemElement()
        {
            this.WorkItems = new List<WorkItemElement>();
        }
    }
}
