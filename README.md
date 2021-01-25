<!--Category:PowerShell--> 
 <p align="right">
    <a href="https://www.powershellgallery.com/packages/EcoVadis.AzureDevOps/"><img src="Images/Header/Powershell_border_40px.png" /></a>
    <a href="http://productivitytools.tech/"><img src="Images/Header/ProductivityTools_green_40px_2.png" /><a> 
    <a href="https://github.com/pwujczyk/EcoVadis.AzureDevOps"><img src="Images/Header/Github_border_40px.png" /></a>
</p>
<p align="center">
    <a href="http://http://productivitytools.tech/">
        <img src="Images/Header/LogoTitle_green_500px.png" />
    </a>
</p>


# EcoVadis AzureDevOps

Module is written to help manage tasks on the AzureDevOps.  It is module dedicated for my team at EcoVadis.

<!--more-->

## Setup MasterConfiguration
- Install PowerShell MasterConfiguration Module

```powershell
Install-Module -Name ProductivityTools.PSMasterConfiguration
```
- Setup EnvironmentVariable

```powershell
Set-MasterConfiguration
```
Check if in the path c:\\Users\\pwujczyk\\Documents\\ProductivityTools.MasterConfiguration\\
 you have json file

 ![MasterConfiguration](Images/Masterconfiguration.png)




## Setup AzureDevops

- Install Module from [PowerShell Gallery](https://www.powershellgallery.com/packages/EcoVadis.AzureDevOps/)

```powershell
Install-Module -Name EcoVadis.AzureDevOps	
```
- Create new file in the MasterConfiguration location **EcoVadis.AzureDevOps.json**

```
{
    "TTTFSAddress":"https://azuredevops.ecovadis.com/EcoVadisApp_TeamProjectCollection/",
    "TTpat":"ti45xwu6mmeakxrcdmdpg7kadefdsk6gc4sxa",
    "TTuserName":"Pawel Wujczyk <PRD\\pwujczyk>",
}
```

![MasterConfiguration](Images/AzureConfiguration.png)


Use it.

```
New-FEStealing "StealingFE" 13
New-BEStealing "StealingBE" 14
Get-TFSItem 101856
 ```
![Stealing](Images/StealingInTFS.png)


## Move-ItemsToSprint - from stack rank


Moving all users stories which will greater or equal 99999999

```PowerShell
Move-ItemsToSprint -TargetSprint 64 -FromStackRank 99999999 -Verbose
```

## Move-ItemsToSprint - not closed


Moving all users stories which are in the status "Dev Active","L2 Approved", move all tasks and eco Bugs which are in the status "New", "Active", "Code Review", to given sprint.

```PowerShell
Move-ItemsToSprint -NotClosed -TargetSprint 68 -Verbose
```

Statuses of US which will be moved:
- Dev Active
- L2 Approved

Statuses of tasks and bugs which will be moved:
- New
- Active
- Code Review
- L2 Approved

Statuses of the tasks and bugs which will be cloned
- Active
- Code Review


## Calculate Sprint hours

```PowerShell
Get-CurrentBacklogEstimation
```

## Set-IsPlanned

Command sets **IsPlanned** flag to all tasks in given query.

```PowerShell
Set-IsPlanned 
Set-IsPlanned -Value $true
```

## Add-ProgressiveRollout

```PowerShell
Add-FEProgressiveRollout 113937
Add-FEProgressiveRollout 113937 -Silent
```

If the task already exist cmdlet will throw exception, unless you will use -Silent switch.

## Add-Testing

It creates two items: Test Case and Testing task for given US.

```PowerShell
Add-Testing113937
Add-Testing 113937 -Silent
```

If the task already exist cmdlet will throw exception, unless you will use -Silent switch.