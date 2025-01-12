<?php
//CLEAR CACHE AND COOKIES
header('Access-Control-Allow-Origin: *');
header("Expires: Mon, 26 Jul 1997 05:00:00 GMT");
header("Last-Modified: " . gmdate("D, d M Y H:i:s") . " GMT");
header("Cache-Control: no-store, no-cache, must-revalidate");// HTTP/1.1
header("Pragma: no-cache");// HTTP/1.0
//Connection database(host=server_address,dbname=database, user, password)
$db = new PDO('mysql:host=localhost; dbname=aquatics_squitten', 'aquatics_squitte', '691507');
$db->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_SILENT);
//Gets the name and score of the top 10 players registered in the database.
$sql= $db->query('SELECT * FROM highscore ORDER BY score DESC LIMIT 10');
//Stores the names and scores obtained from the database in the variable "$row".
foreach ($sql as $row) {echo $row['name'] . '|' . $row['score'] .'|';}
//Destroys all variables and disconnects the database.
unset($db,$sql,$row,$_GET,$_POST);
exit;
?>
