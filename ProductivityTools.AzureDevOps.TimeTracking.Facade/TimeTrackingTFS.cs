using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi;
using System;

namespace ProductivityTools.AzureDevOps.TimeTracking.Facade
{
    public class TFS
    {

        private void LoginWithPersonalAccessToken(string tfsAddress, string personalAccessToken)
        {
            VssConnection connection = new VssConnection(new Uri(tfsAddress), new VssBasicCredential(string.Empty, personalAccessToken));
        }
    }
}
