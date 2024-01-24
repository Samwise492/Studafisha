<?php
    // Connect to database
    require dirname(__FILE__) . '/database.php';
    
    $sql = "SELECT User_Id, LastName, Name, Position FROM Users WHERE Login='".$_POST["Login"]."'"; // query
    $result = $mysqli_conection->query($sql);

    if ($result->num_rows > 0) {
        // output data of each row
        while($row = $result->fetch_assoc()) {
            $array = array(
                $row["User_Id"].';'.mb_convert_encoding($row["LastName"], 'utf-8', 'windows-1251').';'.mb_convert_encoding($row["Name"], 'utf-8', 'windows-1251').
                ';'.mb_convert_encoding($row["Position"], 'utf-8', 'windows-1251').';'
            );
            foreach($array as $var)
                $output .= $var;
        }
    } else {
        echo "0 results";
    }

    // get squad
    $sql = "SELECT Squad_Id FROM Users WHERE Login='".$_POST["Login"]."'"; // query
    $result = $mysqli_conection->query($sql);

    if ($result->num_rows > 0) {
        while($row = $result->fetch_assoc()) {
            $squad_id = $row["Squad_Id"];
        }
    } else {
        echo "0 results";
    }

    $sql = "SELECT Type, Name FROM Squads WHERE Squad_Id='".$squad_id."'"; // query
    $result = $mysqli_conection->query($sql);

    if ($result->num_rows > 0) {
        while($row = $result->fetch_assoc()) {
        $output .= mb_convert_encoding($row["Type"], 'utf-8', 'windows-1251').'|'.mb_convert_encoding($row["Name"], 'utf-8', 'windows-1251').';';
        }
    } else {
        echo "0 results";
    }

    // get hq
    $sql = "SELECT Headquarter_Id FROM Squads WHERE Squad_Id='".$squad_id."'"; // query
    $result = $mysqli_conection->query($sql);

    if ($result->num_rows > 0) {
        while($row = $result->fetch_assoc()) {
            $hq_id = $row["Headquarter_Id"];
        }
    } else {
        echo "0 results";
    }

    $sql = "SELECT University FROM Headquarters WHERE Headquarter_Id='".$hq_id."'"; // query
    $result = $mysqli_conection->query($sql);

    if ($result->num_rows > 0) {
        while($row = $result->fetch_assoc()) {
        $output .= mb_convert_encoding($row["University"], 'utf-8', 'windows-1251');
        }
    } else {
        echo "0 results";
    }

    echo $output;
    $mysqli_conection->close();
  ?>
