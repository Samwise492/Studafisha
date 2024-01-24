<?php
  // Connect to database
  require dirname(__FILE__) . '/database.php';

  $sql = "SELECT * FROM Library WHERE Article_Id='".$_POST["Id"]."'"; 
  $result = $mysqli_conection->query($sql);

  if ($result->num_rows > 0) {
    // output data of each row
    while($row = $result->fetch_assoc()) {
      echo mb_convert_encoding($row["Article_text"], 'utf-8', 'windows-1251').'|'.mb_convert_encoding($row["Link1"], 'utf-8', 'windows-1251').'|'.
      mb_convert_encoding($row["Link2"], 'utf-8', 'windows-1251');
    }
  } else {
    echo " 0 results";
  }
  $mysqli_conection->close();
?>