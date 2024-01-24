<?php
  // Connect to database
  require dirname(__FILE__) . '/database.php';
  
  $sql = "SELECT * FROM Events"; // query
  $result = $mysqli_conection->query($sql);

  if ($result->num_rows > 0) {
    // output data of each row
    while($row = $result->fetch_assoc()) { // symbol is 0492
      echo mb_convert_encoding($row["Name"], 'utf-8', 'windows-1251').'ì'.mb_convert_encoding($row["Type"], 'utf-8', 'windows-1251').'ì'.
      mb_convert_encoding($row["Date"], 'utf-8', 'windows-1251').'ì'.mb_convert_encoding($row["Description"], 'utf-8', 'windows-1251').'ì'.
      $row["Organizer1_Id"].'ì'.$row["Organizer2_Id"].'ì'.$row["Organizer3_Id"].'ì'.$row["Organizer4_Id"].'ì'.
      $row["VK"].'ì'.
      mb_convert_encoding($row["Address"], 'utf-8', 'windows-1251').'ì'.
      mb_convert_encoding($row["Item1"], 'utf-8', 'windows-1251').'ì'.mb_convert_encoding($row["Item2"], 'utf-8', 'windows-1251').'ì'.
      mb_convert_encoding($row["Item3"], 'utf-8', 'windows-1251').'ì'.mb_convert_encoding($row["Item4"], 'utf-8', 'windows-1251').'ì'.
      mb_convert_encoding($row["Event_plan"], 'utf-8', 'windows-1251').'ì'.mb_convert_encoding($row["VK photo"], 'utf-8', 'windows-1251').'ì'.
      $row["Event_Id"].'|';
    }
  } else {
    echo "0 results";
  }
  $mysqli_conection->close();
  ?>