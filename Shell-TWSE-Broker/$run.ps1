Import-Module ".\Broker.psm1"
##################################################
# $timestamp = Get-NowTimestamp
##################################################
# $main = "${API_BSAE}/${API_BROKER_SERVICE_AUDIT}?showType=main&response=json&_=${timestamp}"
# $detail = "${API_BSAE}/${API_BROKER_SERVICE_AUDIT}?showType=detail&stkNo=1020&response=json&_=${timestamp}"
# $list = "${API_BSAE}/${API_BROKER_SERVICE_AUDIT}?showType=list&stkNo=1020&response=json&_=${timestamp}"
# ##################################################
# Write-Host "main: ${main}"
# Invoke-WebRequest -Uri "${main}" -OutFile ./main.json
# Write-Host "detail: ${detail}"
# Invoke-WebRequest -Uri "${detail}" -OutFile ./detail.json
# Write-Host "list: ${list}"
# Invoke-WebRequest -Uri "${list}" -OutFile ./list.json
##################################################
$a = Get-Broker
Write-Host "a: ${a}"
##################################################
pause