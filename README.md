# EcoVadis.AzureDevOps

Install the module from [PowerShell Gallery](https://www.powershellgallery.com/packages/EcoVadis.AzureDevOps/)


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
Get-TFSItem 101856
 ```
![Stealing](Images/StealingInTFS.png)



