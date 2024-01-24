<?php
  // test
  // Connect to database
  require dirname(__FILE__) . '/database.php';
  
  $sql = "SELECT Login FROM Users WHERE Name='Denis'"; // query
  $result = $mysqli_conection->query($sql);

  if ($result->num_rows > 0) {
    // output data of each row
    while($row = $result->fetch_assoc()) {
      echo $row["Login"];
    }
  } else {
    echo "0 results";
  }
  $mysqli_conection->close();
  ?>
