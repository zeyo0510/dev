Import-Module ".\DateUtils.psm1"
##################################################
function Get-Broker {
  param()
  ##################################################
  $retValue = @()
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
                                }
  $response = $response.Content | ConvertFrom-Json
  ##################################################
  $fields = $response.fields
  $data   = $response.data
  ##################################################
  foreach ($entry in $data) {
    $row = @{}
    for ($i = 0; $i -lt $fields.Count; $i++) {
      $row[$fields[$i]] = $entry[$i]
    }
    $retValue += $row
  }
  $retValue = $retValue | ConvertTo-Json
  ##################################################
  return $retValue
}