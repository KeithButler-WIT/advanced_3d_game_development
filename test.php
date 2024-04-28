<?php

    // echo "Hello World From PHP";
    // $name = $_GET['name'];
    // $score = $_GET['score'];
    // echo "Name = ".$name.", Score = ".$score;
    
    $host = "localhost";
    $database = "id21963617_keith";
    $user = "id21963617_keith";
    $password = "Thelads4;";
    
    $error = "Can't Connect";
    $con = mysqli_connect($host,$user,$password);
    mysqli_select_db($con, $database) or die("Unable to connect");

    $name = $_GET['name'];
    echo "Name: ".$name;
    $score = $_GET['score'];
    echo "Score: ".$score."\n";

    $query = "SELECT * FROM player WHERE name = '$name'";
    $result = mysqli_query($con,$query);
    $n = mysqli_num_rows($result);
    if ($n > 0)
    {
        echo "Found the player";
        $query = "UPDATE player SET score = '$score' WHERE name = '$name'";
    }
    else
    {
        echo("Sorry, this player is not registered");
        $query = "INSERT INTO player VALUES ('$name','$score')";
    }
    $result = mysqli_query($con, $query);

?>