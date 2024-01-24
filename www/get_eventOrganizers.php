<?php
  // Connect to database
  require dirname(__FILE__) . '/database.php';

  if (isset($_POST["Organizer1_SquadId"])) // get squads of organizers
  {
    $sql1 = "SELECT Type, Name FROM Squads WHERE Squad_Id='".$_POST["Organizer1_SquadId"]."'"; // query
    if (isset($_POST["Organizer2_SquadId"]))
    {
        $sql1 = "SELECT Type, Name FROM Squads WHERE Squad_Id='".$_POST["Organizer1_SquadId"]."'";
        $sql2 = "SELECT Type, Name FROM Squads WHERE Squad_Id='".$_POST["Organizer2_SquadId"]."'";
        if (isset($_POST["Organizer3_SquadId"]))
        {
            $sql1 = "SELECT Type, Name FROM Squads WHERE Squad_Id='".$_POST["Organizer1_SquadId"]."'";
            $sql2 = "SELECT Type, Name FROM Squads WHERE Squad_Id='".$_POST["Organizer2_SquadId"]."'";
            $sql3 = "SELECT Type, Name FROM Squads WHERE Squad_Id='".$_POST["Organizer3_SquadId"]."'";
            if (isset($_POST["Organizer4_SquadId"]))
            {
              $sql1 = "SELECT Type, Name FROM Squads WHERE Squad_Id='".$_POST["Organizer1_SquadId"]."'";
              $sql3 = "SELECT Type, Name FROM Squads WHERE Squad_Id='".$_POST["Organizer3_SquadId"]."'";
              $sql4 = "SELECT Type, Name FROM Squads WHERE Squad_Id='".$_POST["Organizer4_SquadId"]."'";
              $sql2 = "SELECT Type, Name FROM Squads WHERE Squad_Id='".$_POST["Organizer2_SquadId"]."'";
            }
        }
    }

    $result1 = $mysqli_conection->query($sql1);
    if (!is_null($sql2))
      $result2 = $mysqli_conection->query($sql2);
    if (!is_null($sql3))
      $result3 = $mysqli_conection->query($sql3);
    if (!is_null($sql4))
      $result4 = $mysqli_conection->query($sql4);

    if ($result1->num_rows > 0) {
      while($row1 = $result1->fetch_assoc()) { // element is 0492
        if ($result2->num_rows > 0) 
          while($row2 = $result2->fetch_assoc()) {
            if ($result3->num_rows > 0) 
              while($row3 = $result3->fetch_assoc()) {
                if ($result4->num_rows > 0) 
                  while($row4 = $result4->fetch_assoc()) {
                    $array = array(
                      mb_convert_encoding($row1["Type"], 'utf-8', 'windows-1251').' '.mb_convert_encoding($row1["Name"], 'utf-8', 'windows-1251')."|",
                      mb_convert_encoding($row2["Type"], 'utf-8', 'windows-1251').' '.mb_convert_encoding($row2["Name"], 'utf-8', 'windows-1251')."|",
                      mb_convert_encoding($row3["Type"], 'utf-8', 'windows-1251').' '.mb_convert_encoding($row3["Name"], 'utf-8', 'windows-1251')."|",
                      mb_convert_encoding($row4["Type"], 'utf-8', 'windows-1251').' '.mb_convert_encoding($row4["Name"], 'utf-8', 'windows-1251')."|"
                    );
                    foreach($array as $var)
                      $output .= $var." ";
                    echo ($output);
            }
            else {
              $array = array(
                mb_convert_encoding($row1["Type"], 'utf-8', 'windows-1251').' '.mb_convert_encoding($row1["Name"], 'utf-8', 'windows-1251')."|",
                mb_convert_encoding($row2["Type"], 'utf-8', 'windows-1251').' '.mb_convert_encoding($row2["Name"], 'utf-8', 'windows-1251')."|",
                mb_convert_encoding($row3["Type"], 'utf-8', 'windows-1251').' '.mb_convert_encoding($row3["Name"], 'utf-8', 'windows-1251')."|"
              );
              foreach($array as $var)
                $output .= $var." ";
              echo ($output);
            }
          }
          else {
            $array = array(
              mb_convert_encoding($row1["Type"], 'utf-8', 'windows-1251').' '.mb_convert_encoding($row1["Name"], 'utf-8', 'windows-1251')."|",
              mb_convert_encoding($row2["Type"], 'utf-8', 'windows-1251').' '.mb_convert_encoding($row2["Name"], 'utf-8', 'windows-1251')."|"
            );
            foreach($array as $var)
              $output .= $var." ";
            echo ($output);
          }
        }
        else {
          $array = array(
            mb_convert_encoding($row1["Type"], 'utf-8', 'windows-1251').' '.mb_convert_encoding($row1["Name"], 'utf-8', 'windows-1251')."|"
          );
          foreach($array as $var)
            $output .= $var." ";
          echo ($output);
        }
      }
    } else {
      echo "0 results";
    }
    $mysqli_conection->close();
  }
  else if (isset($_POST["Organizer1_Id"])) // get organizers
  {
      $sql = "SELECT LastName, Name, Squad_Id, Phone FROM Users WHERE User_Id='".$_POST["Organizer1_Id"]."'"; // query
      if (isset($_POST["Organizer2_Id"]))
      {
          $sql = "SELECT LastName, Name, Squad_Id, Phone  FROM Users WHERE User_Id='".$_POST["Organizer1_Id"]."' OR User_Id='".$_POST["Organizer2_Id"]."'"; // query
          if (isset($_POST["Organizer3_Id"]))
          {
              $sql = "SELECT LastName, Name, Squad_Id, Phone  FROM Users WHERE User_Id='".$_POST["Organizer1_Id"]."' OR User_Id='".$_POST["Organizer2_Id"].
              "' OR User_Id='".$_POST["Organizer3_Id"]."'"; // query
              if (isset($_POST["Organizer4_Id"]))
              {
                  $sql = "SELECT LastName, Name, Squad_Id, Phone  FROM Users WHERE User_Id='".$_POST["Organizer1_Id"]."' OR User_Id='".$_POST["Organizer2_Id"].
                  "' OR User_Id='".$_POST["Organizer3_Id"]."' OR User_Id='".$_POST["Organizer4_Id"]."'"; // query
              }
          }
      }
  
    $result = $mysqli_conection->query($sql);

    if ($result->num_rows > 0) {
      // output data of each row
      while($row = $result->fetch_assoc()) { // elemnts is 0492
        echo mb_convert_encoding($row["LastName"], 'utf-8', 'windows-1251').' '.mb_convert_encoding($row["Name"], 'utf-8', 'windows-1251')."ì".
        mb_convert_encoding($row["Squad_Id"], 'utf-8', 'windows-1251')."ì".$row["Phone"]."|";
      }
    } else {
      echo "0 results";
    }
    $mysqli_conection->close();
  }
  ?>