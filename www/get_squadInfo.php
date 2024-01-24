<?php
  // Connect to database
  require dirname(__FILE__) . '/database.php';

  $hq_sql = "SELECT Headquarter_Id FROM Headquarters WHERE University='".mb_convert_encoding($_POST["Uni"], 'windows-1251', 'utf-8')."'";
  $hq_id = $mysqli_conection->query($hq_sql);
  $hq_id = $hq_id->fetch_assoc();
  $hq_id = $hq_id["Headquarter_Id"];

  $sql = "SELECT VK, Commander, Commander_Vk, Commissioner, Commissioner_Vk, Press, Press_Vk, Master, Master_Vk, History, Mail 
  FROM Squads WHERE Type='"
  .mb_convert_encoding($_POST["Type"], 'windows-1251', 'utf-8')."' AND Name='"
  .mb_convert_encoding($_POST["Name"], 'windows-1251', 'utf-8')."' AND Headquarter_Id='".$hq_id."'";
  $result = $mysqli_conection->query($sql);

  $address_sql = "SELECT Uni_Address FROM Headquarters WHERE Headquarter_Id='".$hq_id."'";
  $address_result = $mysqli_conection->query($address_sql);
  $address_result = $address_result->fetch_assoc();
  $address_result = $address_result["Uni_Address"];

  if ($result->num_rows > 0) {
    // output data of each row
    while($row = $result->fetch_assoc()) {
      $output = $row["VK"].'|'.
      mb_convert_encoding($row["Commander"], 'utf-8', 'windows-1251').'|'.$row["Commander_Vk"].'|'.
      mb_convert_encoding($row["Commissioner"], 'utf-8', 'windows-1251').'|'.$row["Commissioner_Vk"].'|'.
      mb_convert_encoding($row["Press"], 'utf-8', 'windows-1251').'|'.$row["Press_Vk"].'|'.
      mb_convert_encoding($row["Master"], 'utf-8', 'windows-1251').'|'.$row["Master_Vk"].'|'.
      mb_convert_encoding($row["History"], 'utf-8', 'windows-1251').'|'.
      $row["Mail"];
    }
  } else { 
    echo "0 results";
  }
  $output .= '|'.mb_convert_encoding($address_result, 'utf-8', 'windows-1251');
  echo $output;

  $mysqli_conection->close();
  ?>
