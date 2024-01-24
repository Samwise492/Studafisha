<?php
  // Connect to database
  require dirname(__FILE__) . '/database.php';

  $sql = "SELECT Item1, Item2, Item3, Item4 FROM Events WHERE Name='".mb_convert_encoding($_POST["EventName"], 'windows-1251', 'utf-8')."'"; 
  $result = $mysqli_conection->query($sql);

  if ($result->num_rows > 0) {
    // output data of each row
    while($row = $result->fetch_assoc()) { // elemnts is 0492
      echo mb_convert_encoding($row["Item1"], 'utf-8', 'windows-1251').' '.mb_convert_encoding($row["Item2"], 'utf-8', 'windows-1251').
      mb_convert_encoding($row["Item3"], 'utf-8', 'windows-1251').' '.mb_convert_encoding($row["Item4"], 'utf-8', 'windows-1251');
    }
  } else {
    echo "0 results";
  }
  $mysqli_conection->close();
?>