<?php
  // Connect to database
  require dirname(__FILE__) . '/database.php';
  
  $sql = "SELECT University FROM Headquarters"; // query
  $result = $mysqli_conection->query($sql);

  if ($result->num_rows > 0) {
    // output data of each row
    while($row = $result->fetch_assoc()) {
      echo mb_convert_encoding($row["University"], 'utf-8', 'windows-1251').'|';
    }
  } else {
    echo "0 results";
  }

  $mysqli_conection->close();
  ?>