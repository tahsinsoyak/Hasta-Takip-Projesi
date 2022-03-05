<?php

$mysqlsunucu = "localhost";
$mysqlkullanici = "root";
$mysqlsifre = "";
$mysqldb="patient";

// $mysqlsunucu = "213.128.75.146"; 
// $mysqlkullanici = "emregurb_admin";
// $mysqldb="emregurb_hospital";
// $mysqlsifre = "o*]ObI_Z81_s";

try {
    $conn = new PDO("mysql:host=$mysqlsunucu;dbname=$mysqldb;charset=utf8", $mysqlkullanici, $mysqlsifre);
    $conn->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
    }
catch(PDOException $e)
    {
    echo "Bağlantı hatası: " . $e->getMessage();
    }
?>