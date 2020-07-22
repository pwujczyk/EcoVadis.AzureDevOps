using EcoVadis.AzureDevOps.Facade;
using System;
using System.Collections.Generic;
using System.Text;

namespace EcoVadis.AzureDevOps.App
{
    public class BacklogManagmenetApp
    {
        private readonly string TfsUrl;
        private readonly string PAT;
        private BacklogManagement TFS;

        public BacklogManagmenetApp(string tfsUrl, string pat)
        {
            this.TfsUrl = tfsUrl;
            this.PAT = pat;
            this.TFS = new BacklogManagement(this.TfsUrl, this.PAT);
        }

        public void MoveElementsToNext()
        {
            TFS.MoveItemsToSprint(57, 63901);
        }
    }
}
