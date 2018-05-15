Write-Host "EmpyrionManager Deployment Script"
Write-Host "================================="

$deployShare = Read-Host "Deploy location [\\192.168.2.15\steamcmd\]: "
$deployDir = Read-Host "Deploy directory [EmpyrionManager\]: "
$deployUser = Read-Host "Deploy username [PHOENIX\pklingman]: "
if ([string]::IsNullOrEmpty($deployShare)) {
    $deployShare = "\\192.168.2.15\steamcmd\"
}
if ([string]::IsNullOrEmpty($deployUser)) {
    $deployUser = "PHOENIX\pklingman"
}
if ([string]::IsNullOrEmpty($deployDir)) {
    $deployDir = "EmpyrionManager\"
}

$password = Read-Host "Password: " -AsSecureString

net use $deployShare /USER:$deployUser $password

Copy-Item "./src/EmpyrionManager/bin/Debug/*" "filesystem::$deployShare$deployDir" -Verbose

Write-Host "Complete."