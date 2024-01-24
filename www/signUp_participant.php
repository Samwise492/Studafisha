<?php
  // Connect to database
  require dirname(__FILE__) . '/database.php';
  
  $errors = array();
  $sql = "SELECT * FROM Registration_participant WHERE Event_Id='".$_POST["EventId"]."' AND User_Id='".$_POST["UserId"]."'";
  $result = $mysqli_conection->query($sql);

  if (isset($_POST["CovidVerification"]) && $_POST["PassportSeries"] && $_POST["PassportNumber"] && $_POST["Giver"])
  {
    if($result->num_rows == 0) { // if user's not participating in this event yet
      
      
      $sql = "INSERT INTO Registration_participant(Event_Id, User_Id, CovidVerification, IsVolunteer, Passport, PassportGiver) VALUES ('".
      $_POST["EventId"]."', '".
      $_POST["UserId"]."', '".
      translate_to_bool($_POST["CovidVerification"])."', '".
      translate_to_bool($_POST["IsVolunteer"])."', '".
      $_POST["PassportSeries"].$_POST["PassportNumber"]."', '".
      mb_convert_encoding($_POST["Giver"], 'windows-1251', 'utf-8')
      ."')";
      $_result = $mysqli_conection->query($sql);
      echo "Success";
    } else { // if user's participating in this event yet
      $errors[] = "Ты уже участвуешь в этом событии";
    }
  }
  else {
    $errors[] = "Не хватает данных";
  }

  if(count($errors) > 0){
    echo $errors[0];
  }
  $mysqli_conection->close();

  function translate_to_bool($var) {
		if ($var == "True")
      $var = 1;
    else if ($var == "False") 
      $var = 0;
    return $var;
	}
  ?>
