using System;
using System.Collections.Generic;
using System.Text;

namespace EcoVadis.AzureDevOps.Optimizely
{
    public class FeatureToggle
    {
        public string Name { get; set; }
        [JsonIgnore]
        public int Week { get; set; }

        [JsonIgnore]
        public DateTime Executed { get; set; }
        [JsonIgnore]
        public string Team { get; set; }
        public bool IsEnabled { get; set; }
    }
}
