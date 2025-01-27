Import-Module "..\Lib-PS\DateUtils.psm1"
##################################################
function Get-Broker {
  param()
  ##################################################
  $retValue = @()
  ##################################################
  $API_BASE             = "https://www.twse.com.tw"
  $BROKER_SERVICE_AUDIT = "rwd/zh/brokerService/brokerServiceAudit"
  ##################################################





  $step_1_session = New-Object Microsoft.PowerShell.Commands.WebRequestSession
  ##################################################





  $step_2_timestamp = Get-NowTimestamp
  $step_2_url       = "${API_BASE}/${BROKER_SERVICE_AUDIT}?showType=main&response=json&_=${step_2_timestamp}"
  ##################################################
  $step2_content = Invoke-WebRequest  -UseBasicParsing            `
                                      -Uri "${step_2_url}"        `
                                      -WebSession $step_1_session `
                                      -Headers @{
                                        # do nothing...
                                      }
  ##################################################
  $step2_json = $step2_content.Content | ConvertFrom-Json
  ##################################################
  $step_2_fields = $step2_json.fields
  $step_2_data   = $step2_json.data
  ##################################################
  $retValue = Convert-TWSE -data $step_2_data -fields $step_2_fields






  $retValue | ForEach-Object {
    Start-Sleep -Seconds 5
    $step_3_stkNo     = $_["證券商代號"]
    $step_3_timestamp = Get-NowTimestamp
    $step_3_url       = "${API_BASE}/${BROKER_SERVICE_AUDIT}?showType=list&stkNo=${step_3_stkNo}&response=json&_=${step_3_timestamp}"
    ##################################################
    $step3_content = Invoke-WebRequest  -UseBasicParsing            `
                                        -Uri "${step_3_url}"        `
                                        -WebSession $step_1_session `
                                        -Headers @{
                                          # do nothing...
                                        }
    ##################################################
    $step3_json = $step3_content.Content | ConvertFrom-Json
    ##################################################
    $step_3_fields = $step3_json.fields
    $step_3_data   = $step3_json.data
    ##################################################
    $retValue += Convert-TWSE -data $step_3_data -fields $step_3_fields
  }



  $retValue = $retValue | Sort-Object -Property "證券商代號"
  $retValue = $retValue | ConvertTo-Json
  ##################################################
  return $retValue
}

function Convert-TWSE {
  param($data, $fields)
  ##################################################
  $retValue = @()
  ##################################################
  foreach ($entry in $data) {
    $tmp = @{}
    ##################################################
    for ($i = 0; $i -lt $fields.Count; $i++) {
      $tmp[$fields[$i]] = $entry[$i]
    }
    ##################################################
    $retValue += $tmp
  }
  ##################################################
  return $retValue
}