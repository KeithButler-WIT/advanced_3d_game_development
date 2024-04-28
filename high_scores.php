<?php
 
    $host = "localhost";
    $database = "id21963617_keith";
    $user = "id21963617_keith";
    $password = "Thelads4;";
    
    $error = "Can't Connect";
    $con = mysqli_connect($host,$user,$password);
    mysqli_select_db($con, $database) or die("Unable to connect");
    
    $query = "SELECT * FROM player";
    $result = mysqli_query($con, $query);

    while ($row = mysqli_fetch_assoc($result))
    {
        $name = $row["name"];
        $score = $row["score"];

        echo "Name: ".$name."\t";
        echo "Score: ".$score."\n";
    }
    
    ?>