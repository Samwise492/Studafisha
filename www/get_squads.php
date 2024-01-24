<?php
  // Connect to database
  require dirname(__FILE__) . '/database.php';
  
  $sql = "SELECT Type, Name FROM Squads WHERE University='".mb_convert_encoding($_POST["University"], 'windows-1251', 'utf-8')."'"; // query
  $result = $mysqli_conection->query($sql);

  if ($result->num_rows > 0) {
    // output data of each row
    while($row = $result->fetch_assoc()) {
      echo mb_convert_encoding($row["Type"], 'utf-8', 'windows-1251').' '.mb_convert_encoding($row["Name"], 'utf-8', 'windows-1251').'|';
    }
  } else {
    echo "0 results";
  }
  $mysqli_conection->close();
  ?>