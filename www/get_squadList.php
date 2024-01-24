<?php
  // Connect to database
  require dirname(__FILE__) . '/database.php';
  
  if (isset($_POST["get_squads"]))
  {
    $sql = "SELECT Type, Name, City FROM Squads"; // query
    $result = $mysqli_conection->query($sql);

    if ($result->num_rows > 0) {
      // output data of each row
      while($row = $result->fetch_assoc()) {
        echo mb_convert_encoding($row["City"], 'utf-8', 'windows-1251').'|'.
        mb_convert_encoding($row["Type"], 'utf-8', 'windows-1251').' '.mb_convert_encoding($row["Name"], 'utf-8', 'windows-1251').'|';
      }
    } else {
      echo "0 results";
    }
  }
  if (isset($_POST["get_squads_without_hq"]))
  {
    $sql = "SELECT Type, Name FROM `Squads` WHERE Headquarter_Id=''"; // query
    $result = $mysqli_conection->query($sql);

    if ($result->num_rows > 0) {
      // output data of each row
      while($row = $result->fetch_assoc()) {
        echo mb_convert_encoding($row["Type"], 'utf-8', 'windows-1251').' '.mb_convert_encoding($row["Name"], 'utf-8', 'windows-1251').'|';
      }
    } else {
      echo "0 results";
    }
  }

  if (isset($_POST["get_hqs"]))
  {
    // get hq ids
    $sql = "SELECT Headquarter_Id FROM Squads"; // query
    $result = $mysqli_conection->query($sql);
    $hq_ids = array();

    if ($result->num_rows > 0) {
        while($row = $result->fetch_assoc()) {
          $hq_ids[ ] = $row["Headquarter_Id"];
        }
    } else {
        echo "0 results";
    }

    foreach($hq_ids as $hq_id)
    {
      $sql = "SELECT University FROM Headquarters WHERE Headquarter_Id='".$hq_id."'"; // query
      $result = $mysqli_conection->query($sql);

      if ($result->num_rows > 0) {
          while($row = $result->fetch_assoc()) {
          $output .= mb_convert_encoding($row["University"], 'utf-8', 'windows-1251').'|';
          }
      } else {
          echo "0 results";
      }
    }
    
    echo $output;
  }

  $mysqli_conection->close();
  ?>