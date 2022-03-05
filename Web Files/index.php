<?php
session_start();
?>
<!DOCTYPE html>
<html lang="en">

<head>
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Patient Monitoring</title>
    <link rel="stylesheet" href="./main.css">
    <link href="https://fonts.googleapis.com/css?family=Asap" rel="stylesheet">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script src="//cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <link rel="icon" type="image/x-icon" href="./assets/favicon.ico">
</head>

<body class="body">

    <form id="loginForm" class="login">
        <input id="username" name="username" type="text" placeholder="Username">
        <input id="password" name="password" type="password" placeholder="Password">
        <button class="getLogin" type="button">Login</button>
    </form>


    <!-- JAVASCRİPT -->
    <script>
    var form = $('#loginForm');
    $('.getLogin').click(function() {
        $.ajax({
            type: "POST",
            url: "php/loginControl.php",
            data: form.serialize(),
            success: function(data) {
                veri = JSON.parse(data);
                if (veri["status"] == "success") {
                    Swal.fire(
                        veri["title"],
                        veri["message"],
                        veri["status"]
                    )
                    if(veri["role"]==0){
                        setTimeout(() => {
                        window.location.href = "doctor.php";
                    }, 200)
                    }else{
                        setTimeout(() => {
                        window.location.href = "nurse.php";
                    }, 200)
                    }
                    

                } 
                else {
                    Swal.fire(
                        veri["title"],
                        veri["message"],
                        veri["status"]
                    )
                }
            }
        });
    })
    </script>
        <!-- SCROLL REVEAL -->
    <script src="https://unpkg.com/scrollreveal"></script>

    <!-- JAVASCRİPT -->
    <script src="./main.js"></script>
</body>

</html>
