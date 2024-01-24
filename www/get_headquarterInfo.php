<?php
  // Connect to database
  require dirname(__FILE__) . '/database.php';

  $sql = "SELECT Uni_Vk, Uni_Email, Uni_Address, Commander1, Commander1_Vk, Commander2, Commander2_Vk, 
  Commissioner, Commissioner_Vk, Press, Press_Vk, Master1, Master1_Vk, Master2, Master2_Vk, Master3, Master3_Vk, History FROM Headquarters 
  WHERE University='".mb_convert_encoding($_POST["Name"], 'windows-1251', 'utf-8')."'";
  $result = $mysqli_conection->query($sql);

  if ($result->num_rows > 0) {
    // output data of each row
    while($row = $result->fetch_assoc()) {
      echo $row["Uni_Vk"].'|'.$row["Uni_Email"].'|'.mb_convert_encoding($row["Uni_Address"], 'utf-8', 'windows-1251').'|'.
      mb_convert_encoding($row["Commander1"], 'utf-8', 'windows-1251').'|'.$row["Commander1_Vk"].'|'.
      mb_convert_encoding($row["Commander2"], 'utf-8', 'windows-1251').'|'.$row["Commander2_Vk"].'|'.
      mb_convert_encoding($row["Commissioner"], 'utf-8', 'windows-1251').'|'.$row["Commissioner_Vk"].'|'.
      mb_convert_encoding($row["Press"], 'utf-8', 'windows-1251').'|'.$row["Press_Vk"].'|'.
      mb_convert_encoding($row["Master1"], 'utf-8', 'windows-1251').'|'.$row["Master1_Vk"].'|'.
      mb_convert_encoding($row["Master2"], 'utf-8', 'windows-1251').'|'.$row["Master2_Vk"].'|'.
      mb_convert_encoding($row["Master3"], 'utf-8', 'windows-1251').'|'.$row["Master3_Vk"].'|'.
      mb_convert_encoding($row["History"], 'utf-8', 'windows-1251');
    }
  } else { 
    echo "0 results";
  }
  $mysqli_conection->close();
  ?>
