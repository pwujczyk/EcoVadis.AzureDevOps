#
# Module manifest for module 'EcoVadis.AzureDevOps'
#
# Generated by: pwujczyk
#
# Generated on: 13.05.2020
#

@{

# Script module or binary module file associated with this manifest.
RootModule = 'EcoVadis.AzureDevOps.dll'

# Version number of this module.
ModuleVersion = '0.0.6'

# Supported PSEditions
# CompatiblePSEditions = @()

# ID used to uniquely identify this module
GUID = 'f30d0d6d-7705-4913-9ae5-2d59047b73d8'

# Author of this module
Author = 'Pawel Wujczyk'


# Copyright statement for this module
Copyright = '(c) 2020 pwujczyk. All rights reserved.'

# Description of the functionality provided by this module
Description = 'Creates item in the stealing category in the EcoVadis TFS.'

# Minimum version of the Windows PowerShell engine required by this module
# PowerShellVersion = ''

# Name of the Windows PowerShell host required by this module
# PowerShellHostName = ''

# Minimum version of the Windows PowerShell host required by this module
# PowerShellHostVersion = ''

# Minimum version of Microsoft .NET Framework required by this module. This prerequisite is valid for the PowerShell Desktop edition only.
# DotNetFrameworkVersion = ''

# Minimum version of the common language runtime (CLR) required by this module. This prerequisite is valid for the PowerShell Desktop edition only.
# CLRVersion = ''

# Processor architecture (None, X86, Amd64) required by this module
# ProcessorArchitecture = ''

# Modules that must be imported into the global environment prior to importing this module
# RequiredModules = @()

# Assemblies that must be loaded prior to importing this module
# RequiredAssemblies = @()

# Script files (.ps1) that are run in the caller's environment prior to importing this module.
# ScriptsToProcess = @()

# Type files (.ps1xml) to be loaded when importing this module
# TypesToProcess = @()

# Format files (.ps1xml) to be loaded when importing this module
# FormatsToProcess = @()

# Modules to import as nested modules of the module specified in RootModule/ModuleToProcess
# NestedModules = @()

# Cmdlets to export from this module, for best performance, do not use wildcards and do not delete the entry, use an empty array if there are no cmdlets to export.
CmdletsToExport = @('New-BEStealing',
'New-FEStealing',
'Get-TFSItem')

# List of all files packaged with this module
FileList = @(
,'EcoVadis.AzureDevOps.App.dll'
,'EcoVadis.AzureDevOps.App.pdb'
,'EcoVadis.AzureDevOps.deps.json'
,'EcoVadis.AzureDevOps.dll'
,'EcoVadis.AzureDevOps.Facade.pdb'
,'EcoVadis.AzureDevOps.pdb'
,'Microsoft.CSharp.dll'
,'Microsoft.TeamFoundation.Build2.WebApi.dll'
,'Microsoft.TeamFoundation.Common.dll'
,'Microsoft.TeamFoundation.Core.WebApi.dll'
,'Microsoft.TeamFoundation.Dashboards.WebApi.dll'
,'Microsoft.TeamFoundation.DistributedTask.Common.Contracts.dll'
,'Microsoft.TeamFoundation.Policy.WebApi.dll'
,'Microsoft.TeamFoundation.SourceControl.WebApi.dll'
,'Microsoft.TeamFoundation.Test.WebApi.dll'
,'Microsoft.TeamFoundation.TestManagement.WebApi.dll'
,'Microsoft.TeamFoundation.Wiki.WebApi.dll'
,'Microsoft.TeamFoundation.Work.WebApi.dll'
,'Microsoft.TeamFoundation.WorkItemTracking.Process.WebApi.dll'
,'Microsoft.TeamFoundation.WorkItemTracking.WebApi.dll'
,'Microsoft.VisualStudio.Services.Common.dll'
,'Microsoft.VisualStudio.Services.TestResults.WebApi.dll'
,'Microsoft.VisualStudio.Services.WebApi.dll'
,'Microsoft.Win32.Registry.dll'
,'Newtonsoft.Json.Bson.dll'
,'Newtonsoft.Json.dll'
,'ObjectDumping.dll'
,'ProductivityTools.DescriptionValue.dll'
,'ProductivityTools.PSCmdlet.dll'
,'System.ComponentModel.Annotations.dll'
,'System.Configuration.ConfigurationManager.dll'
,'System.Data.SqlClient.dll'
,'System.Diagnostics.DiagnosticSource.dll'
,'System.IO.FileSystem.Primitives.dll'
,'System.Management.Automation.dll'
,'System.Net.Http.Formatting.dll'
,'System.Security.AccessControl.dll'
,'System.Security.Cryptography.Cng.dll'
,'System.Security.Cryptography.OpenSsl.dll'
,'System.Security.Cryptography.ProtectedData.dll'
,'System.Security.Principal.Windows.dll'
,'System.Text.Encoding.CodePages.dll'
,'System.Text.RegularExpressions.dll'
,'System.Threading.dll'
,'System.Threading.Tasks.Extensions.dll'
,'System.Xml.ReaderWriter.dll'
,'System.Xml.XmlDocument.dll'
,'System.Xml.XPath.dll'
,'System.Xml.XPath.XmlDocument.dll'
)

# Private data to pass to the module specified in RootModule/ModuleToProcess. This may also contain a PSData hashtable with additional module metadata used by PowerShell.
PrivateData = @{

    PSData = @{

        # Tags applied to this module. These help with module discovery in online galleries.
        Tags = @('EcoVadis')

        # A URL to the license for this module.
        # LicenseUri = ''

        # A URL to the main website for this project.
        ProjectUri = 'https://github.com/pwujczyk/EcoVadis.AzureDevOps'

        # A URL to an icon representing this module.
        # IconUri = ''

        # ReleaseNotes of this module
        # ReleaseNotes = ''

    } # End of PSData hashtable

} # End of PrivateData hashtable

# HelpInfo URI of this module
HelpInfoURI = 'https://github.com/pwujczyk/EcoVadis.AzureDevOps'

# Default prefix for commands exported from this module. Override the default prefix using Import-Module -Prefix.
# DefaultCommandPrefix = ''

}

