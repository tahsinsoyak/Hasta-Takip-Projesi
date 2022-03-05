<?php
include('../conn.php');
session_start();

if(isset($_POST["patientTemp"]) && isset($_POST["pulse"])&& isset($_POST["roomTemp"]))
{
    $patientTemp = $_POST['patientTemp']; 
    $roomTemp = $_POST['roomTemp']; 
    $pulse = $_POST['pulse']; 
    $patientTempLimit = explode(",", $patientTemp);
    $roomTempLimit = explode(",", $roomTemp);
    $pulseTempLimit = explode(",", $pulse);


    $query = $conn->prepare("INSERT INTO limits SET
        roomTempLower = ?,
        roomTempUpper = ?,
        pulseLower = ?,
        pulseUpper = ?,
        patientTempLower = ?,
        patientTempUpper = ?");
        $insert = $query->execute(array(
           $roomTempLimit[0],$roomTempLimit[1], $pulseTempLimit[0],$pulseTempLimit[1],$patientTempLimit[0],$patientTempLimit[1],));
        if ( $insert ){
            $data["status"]="success";        
            $data["title"]="success";        
            $data["message"]="Kayıt oluşturuldu.";
            echo json_encode($data);  
            $myfile = fopen("log.txt", "a") or die("Unable to open file!");
            $txt = "Dosya ".$_SESSION['username']." tarafından değiştirildi (".date("Y/m/d")." / ".date("h:i:sa").") Hasta ateş limit: ($patientTempLimit[0],$patientTempLimit[1]) , Nabız limit: ($pulseTempLimit[0],$pulseTempLimit[1]) // Oda sıcaklığı limit ($roomTempLimit[0],$roomTempLimit[1])   \n";
            fwrite($myfile, $txt);
            fclose($myfile);
        }
        else{
        $data["status"]="error";        
        $data["title"]="error";        
        $data["message"]="Ekleme yapılırken sorun oluştu.";
        echo json_encode($data);  
        }
    
}
else{
        $data["status"]="error";        
        $data["title"]="error";        
        $data["message"]="Ekleme başarısız 2";
        echo json_encode($data);  
    
}

?>