<?php
  // Connect to database
  require dirname(__FILE__) . '/database.php';
  
  $sql = "SELECT Squad_Id FROM Squads WHERE Name='".mb_convert_encoding($_POST["Squad"], 'windows-1251', 'utf-8')."'"; // query
  $result = $mysqli_conection->query($sql);

  if ($result->num_rows > 0) {
    // output data of each row
    while($row = $result->fetch_assoc()) {
      echo $row["Squad_Id"];
    }
  } else {
    echo "0 results";
  }
  $mysqli_conection->close();
  ?>