function Get-NowTimestamp {
  param()
  ##################################################
  $retValue = $null
  ##################################################
  $now = Get-Date
  ##################################################
  $year        = $now.Year
  $year        = $year.ToString("0000")
  $month       = $now.Month
  $month       = $month.ToString("00")
  $day         = $now.Day
  $day         = $day.ToString("00")
  $hour        = $now.Hour
  $hour        = $hour.ToString("00")
  $minute      = $now.Minute
  $minute      = $minute.ToString("00")
  $second      = $now.Second
  $second      = $second.ToString("00")
  $millisecond = $now.Millisecond
  $millisecond = $millisecond.ToString("000")
  ##################################################
  $start = "1970-01-01 00:00:00.000"
  $end   = "${year}-${month}-${day} ${hour}:${minute}:${second}.${millisecond}"
  ##################################################
  $retValue = New-TimeSpan -Start $start -End $end
  $retValue = $retValue.TotalMilliseconds
  ##################################################
  return $retValue
}