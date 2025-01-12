<?php
$name = $_POST['name']; //Player name
$score =$_POST['score'];//Player score
//Connection database(host=server_address,dbname=database, user, password)
$db = new PDO('mysql:host=localhost; dbname=aquatics_squitten', 'aquatics_squitte', '691507');
$db->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_SILENT);
//Insert name and score in the table "highscore" of the database.
$sql= $db->exec("INSERT INTO highscore (name, score) VALUES ('$name', '$score')");
//Destroys all variables and disconnects the database.
unset($db,$sql,$name,$score,$_POST['name'],$_POST['score']);
exit;
?>


