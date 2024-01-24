<?php
  // Connect to database
  require dirname(__FILE__) . '/database.php';
  
  if (isset($_POST["Get_Dates"]))
  {
    $sql = "SELECT Event_Id FROM Registration_participant WHERE User_Id='".$_POST["User_Id"]."'"; // query
    $result = $mysqli_conection->query($sql);
    $event_ids = array();
    $dates = array();

    while($row = $result->fetch_assoc()) {
      array_push($event_ids, $row["Event_Id"]);
    }

    $i = 0;
    do
    {
      $sql = "SELECT Date FROM Events WHERE Event_Id='".$event_ids[$i]."'";
      $result = $mysqli_conection->query($sql);
      while($row = $result->fetch_assoc()) {
        array_push($dates, $row["Date"]);
      }
      $i++;
    }
    while($i < count($event_ids));

    $i = 0;
    do
    {
      echo $dates[$i]."|";
      $i++;
    }
    while($i < count($dates));
  }
  else
  {
    $sql = "SELECT User_Id FROM User_scores WHERE User_Id='".$_POST["User_Id"]."'";
    $result = $mysqli_conection->query($sql);
    $rowNumbers = ($mysqli_conection->query($sql))->num_rows;
    
    if ($rowNumbers == 0) // score was not initialised before
    {
      $sql = "SELECT Event_Id FROM Events WHERE Organizer1_Id='".$_POST["User_Id"]."'";
      $result1 = $mysqli_conection->query($sql);
      $sql = "SELECT Event_Id FROM Events WHERE Organizer2_Id='".$_POST["User_Id"]."'";
      $result2 = $mysqli_conection->query($sql);
      $sql = "SELECT Event_Id FROM Events WHERE Organizer3_Id='".$_POST["User_Id"]."'";
      $result3 = $mysqli_conection->query($sql);
      $sql = "SELECT Event_Id FROM Events WHERE Organizer4_Id='".$_POST["User_Id"]."'";
      $result4 = $mysqli_conection->query($sql);
      $organisation_score = ($result1->num_rows) + ($result2->num_rows) + ($result3->num_rows) + ($result4->num_rows);

      $sql = "SELECT Event_Id FROM Registration_participant WHERE User_Id='".$_POST["User_Id"]."'";
      $participant_score = ($mysqli_conection->query($sql))->num_rows;

      $sql = "SELECT Event_Id FROM Registration_participant WHERE User_Id='".$_POST["User_Id"]."' AND IsVolunteer='1'";
      $volunteer_score = ($mysqli_conection->query($sql))->num_rows;

      $total_score = $participant_score*$_POST["P_Multiplier"]+$volunteer_score*$_POST["V_Multiplier"]+
      +$organisation_score*$_POST["O_Multiplier"];

      $sql = "INSERT INTO User_scores(User_Id, Participant_Score, Volunteer_Score, Organizer_Score, Total_Score) VALUES("
      .$_POST["User_Id"].", ".$participant_score.", ".$volunteer_score.", ".$organisation_score.", ".$total_score.")";
      $mysqli_conection->query($sql);

      echo $participant_score*$_POST["P_Multiplier"]."|"
      .$volunteer_score*$_POST["V_Multiplier"]."|"
      .$organisation_score*$_POST["O_Multiplier"];
    }
    else // score was initialised before
    {
      $sql = "SELECT * FROM User_scores WHERE User_Id='".$_POST["User_Id"]."'"; // query
      $result = $mysqli_conection->query($sql);

      while($row = $result->fetch_assoc()) {
          $scores = $row["Participant_Score"]*$_POST["P_Multiplier"]." ".
          $row["Volunteer_Score"]*$_POST["V_Multiplier"]." ".
          $row["Organizer_Score"]*$_POST["O_Multiplier"]." ".
          $row["Total_Score"];

          echo "hasBeenInitialised"."|".$scores;
        }
    }
  }

  $mysqli_conection->close();
  ?>