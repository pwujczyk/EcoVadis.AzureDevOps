# EcoVadis.AzureDevOps

If you don't have PowerShell core install it. (https://github.com/PowerShell/Powershell) (I don't know why it is not working on PowerShell 6.0 - maybe somebody help me error is  ``Could not load file or assembly 'System.Net.Http.Formatting, Version=5.2.4.0``)

Next install the module from [PowerShell Gallery](https://www.powershellgallery.com/packages/EcoVadis.AzureDevOps/)


```powershell
Install-Module -Name EcoVadis.AzureDevOps	
```


Set environments variables:
```
$env:TTTFSAddress="https://address.com//"
$env:TTpat="PAT"
$env:TTuserName="Pawel Wujczyk <PRD\pwujczyk>"
```

Use it.

```
New-FEStealing "StealingFE" 13
New-BEStealing "StealingBE" 14
 ```
![Stealing](Images\StealingInTFS.png)



