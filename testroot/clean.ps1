$ignorePattern = @("run.bat", "clean.ps1")

Get-ChildItem -Recurse | Where-Object {
    $ignorePattern -notcontains $_.Name
} | Remove-Item -Recurse -Force -ErrorAction SilentlyContinue
