<?php
include('../conn.php');
$query = $conn->query("SELECT * FROM datas ORDER BY id DESC")->fetch(PDO::FETCH_ASSOC);
$limits = $conn->query("SELECT * FROM limits ORDER BY id DESC LIMIT 1")->fetch(PDO::FETCH_ASSOC);

if($query){
    $data["nabiz"]=$query["nabiz"];
    $data["ates"]=$query["ates"];
    $data["sicaklik"]=$query["sicaklik"];
    $data["roomTempLower"]=$limits["roomTempLower"];
    $data["roomTempUpper"]=$limits["roomTempUpper"];
    $data["patientTempUpper"]=$limits["patientTempUpper"];
    $data["patientTempLower"]=$limits["patientTempLower"];
    $data["pulseLower"]=$limits["pulseLower"];
    $data["pulseUpper"]=$limits["pulseUpper"];
    echo json_encode($data);
    
}
else{
    echo " veri yok ";
}

?>