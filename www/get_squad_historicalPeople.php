<?php
  // Connect to database
  require dirname(__FILE__) . '/database.php';

  $sql = "SELECT HistoricalPerson_1, HistoricalPerson_1_About, 
  HistoricalPerson_2, HistoricalPerson_2_About, 
  HistoricalPerson_3, HistoricalPerson_3_About, 
  HistoricalPerson_4, HistoricalPerson_4_About 
  FROM Squads WHERE Type='"
  .mb_convert_encoding($_POST["Type"], 'windows-1251', 'utf-8')."' AND Name='"
  .mb_convert_encoding($_POST["Name"], 'windows-1251', 'utf-8')."'";
  $result = $mysqli_conection->query($sql);

  if ($result->num_rows > 0) {
    // output data of each row
    while($row = $result->fetch_assoc()) {
      echo
      mb_convert_encoding($row["HistoricalPerson_1"], 'utf-8', 'windows-1251').'|'.
      mb_convert_encoding($row["HistoricalPerson_1_About"], 'utf-8', 'windows-1251').'|'.
      mb_convert_encoding($row["HistoricalPerson_2"], 'utf-8', 'windows-1251').'|'.
      mb_convert_encoding($row["HistoricalPerson_2_About"], 'utf-8', 'windows-1251').'|'.
      mb_convert_encoding($row["HistoricalPerson_3"], 'utf-8', 'windows-1251').'|'.
      mb_convert_encoding($row["HistoricalPerson_3_About"], 'utf-8', 'windows-1251').'|'.
      mb_convert_encoding($row["HistoricalPerson_4"], 'utf-8', 'windows-1251').'|'.
      mb_convert_encoding($row["HistoricalPerson_4_About"], 'utf-8', 'windows-1251');
    }
  } else { 
    echo "0 results";
  }

  $mysqli_conection->close();
  ?>
