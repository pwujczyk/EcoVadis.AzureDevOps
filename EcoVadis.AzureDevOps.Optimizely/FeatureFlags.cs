using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace EcoVadis.AzureDevOps.Optimizely
{
    public class FeatureFlags 
    {
        private const string featureToggleAddress = "https://www.ecovadis-survey.com/Anakin.WebApi/api/featureToggle/featureToggle/getFeatureFlags";
        private readonly IHttpClientFactory clientFactory;

        public FeatureFlags()
        {
            
        }

        public async Task<List<FeatureToggle>> Get()
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(featureToggleAddress);
            var responseBody = await response.Content.ReadAsStringAsync();
            var queryResponse = JsonConvert.DeserializeObject<List<FeatureToggle>>(responseBody);
            return queryResponse;
        }
    }
}
