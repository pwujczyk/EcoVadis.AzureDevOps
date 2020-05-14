using System;
using System.Collections.Generic;
using System.Text;

namespace EcoVadis.TimeTracking.Facade
{
    class RelConstants
    {
        //https://docs.microsoft.com/en-us/azure/devops/boards/queries/link-type-reference?view=vsts

        public const string RelatedRefStr = "System.LinkTypes.Related";
        public const string ChildRefStr = "System.LinkTypes.Hierarchy-Forward";
        public const string ParentRefStr = "System.LinkTypes.Hierarchy-Reverse";
        public const string DuplicateRefStr = "System.LinkTypes.Duplicate-Forward";
        public const string DuplicateOfRefStr = "System.LinkTypes.Duplicate-Reverse";
        public const string SuccessorRefStr = "System.LinkTypes.Dependency-Forward";
        public const string PredecessorRefStr = "System.LinkTypes.Dependency-Reverse";
        public const string TestedByRefStr = "Microsoft.VSTS.Common.TestedBy-Forward";
        public const string TestsRefStr = "Microsoft.VSTS.Common.TestedBy-Reverse";
        public const string TestCaseRefStr = "Microsoft.VSTS.TestCase.SharedStepReferencedBy-Forward";
        public const string SharedStepsRefStr = "Microsoft.VSTS.TestCase.SharedStepReferencedBy-Reverse";
        public const string AffectsRefStr = "Microsoft.VSTS.Common.Affects-Forward";
        public const string AffectedByRefStr = "Microsoft.VSTS.Common.Affects-Reverse";
        public const string AttachmentRefStr = "AttachedFile";
        public const string HyperLinkRefStr = "Hyperlink";
        public const string ArtifactLinkRefStr = "ArtifactLink";

        public const string LinkKeyForDict = "<NewLink>"; // key for dictionary to separate a link from fields            
    }
}
