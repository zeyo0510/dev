$API_BSAE = "https://www.twse.com.tw"
$API_BROKER_SERVICE_AUDIT = "rwd/zh/brokerService/brokerServiceAudit"

Invoke-WebRequest -Uri "${API_BSAE}/${API_BROKER_SERVICE_AUDIT}?showType=main&response=json&_=1736954421484" -OutFile ./main.json
Invoke-WebRequest -Uri "${API_BSAE}/${API_BROKER_SERVICE_AUDIT}?showType=detail&stkNo=1020&response=json&_=1737477368648" -OutFile ./detail.json
Invoke-WebRequest -Uri "${API_BSAE}/${API_BROKER_SERVICE_AUDIT}?showType=list&stkNo=1020&response=json&_=1737477413519" -OutFile ./list.json