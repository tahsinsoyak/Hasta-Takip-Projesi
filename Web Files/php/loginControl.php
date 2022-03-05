<?php
include('../conn.php');
session_start();

if(isset($_POST["username"]) && isset($_POST["password"]))
{
    $password = $_POST['password']; 
    $username = $_POST['username']; 
    $query = $conn->query("SELECT * FROM user WHERE username = '{$username}' AND password = '{$password}'")->fetch(PDO::FETCH_ASSOC);
    if ( $query ){
        $_SESSION["username"]=$query["username"];
        $_SESSION["role"]=$query["role"];
        $data["status"]="success";        
        $data["title"]="success";        
        $data["message"]="Giriş başarılı";
        $data["role"]=$query["role"];
        echo json_encode($data);  
    }
    else{
        $data["status"]="error";        
        $data["title"]="error";        
        $data["message"]="Kişi üye değil".$username." ".$password;
        echo json_encode($data);  
    }
}
else{
        $data["status"]="error";        
        $data["title"]="error";        
        $data["message"]="post olmadı";
        echo json_encode($data);  
    
}

?>