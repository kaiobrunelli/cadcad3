Get-ChildItem -Path $PSScriptRoot -Include bin,obj -Recurse -Directory |
    ForEach-Object {
        Write-Host "Removendo $($_.FullName)"
        Remove-Item $_.FullName -Recurse -Force
    }

Write-Host "Concluido."
