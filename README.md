# PowershellModuleInCSharp

Usage of commands
-----------------

```powershell
Get-MyTestCommand -Name Test -Verbose | ConvertTo-Json | Out-File test.json
Get-Content .\test.json | ConvertFrom-Json | Write-MyTestCommand
````

