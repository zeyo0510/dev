Import-Module ".\DateUtils.psm1"
##################################################
function Get-Broker {
  param()
  ##################################################
  $retValue = $null
  ##################################################
  $API_BASE             = "https://www.twse.com.tw"
  $BROKER_SERVICE_AUDIT = "rwd/zh/brokerService/brokerServiceAudit"
  ##################################################
  $timestamp = Get-NowTimestamp
  $url       = "${API_BASE}/${BROKER_SERVICE_AUDIT}?showType=main&response=json&_=${timestamp}"
  ##################################################
  $session = New-Object Microsoft.PowerShell.Commands.WebRequestSession
  ##################################################
  $response = Invoke-WebRequest -UseBasicParsing     `
                                -Uri "${url}"        `
                                -WebSession $session `
                                -Headers @{
                                    # do nothing...
                                } `
  ##################################################
  return $response
}